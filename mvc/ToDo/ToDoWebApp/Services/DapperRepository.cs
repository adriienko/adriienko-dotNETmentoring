using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using ToDo.Models;

namespace ToDo.Services;

public class DapperRepository
{
    private readonly string _connectionString;

    public DapperRepository(string connectionString)
    {
        _connectionString = connectionString;
        EnsureGetUnfinishedTodosStoredProcedureExists();
        EnsureGetPagedToDoItemsExists();
    }

    public void EnsureGetPagedToDoItemsExists()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // Check if the stored procedure exists
            var procedureExists = connection.ExecuteScalar<int>(
                @"IF EXISTS (SELECT 1 
                            FROM sys.objects 
                            WHERE object_id = OBJECT_ID(N'[dbo].[GetPagedToDoItems]') 
                            AND type in (N'P', N'PC'))
                  SELECT 1 ELSE SELECT 0");

            if (procedureExists == 0)
            {
                // Create the stored procedure if it doesn't exist
                var createProcedureSql = @"
                CREATE PROCEDURE GetPagedToDoItems
                    @PageNumber INT,
                    @PageSize INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DECLARE @Offset INT
                    SET @Offset = (@PageNumber - 1) * @PageSize

                    SELECT 
                        Id,
                        Title,
                        IsCompleted,
                        DateCreated
                    FROM 
                        ToDoItems
                    ORDER BY 
                        DateCreated DESC
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;
                END";

                connection.Execute(createProcedureSql);
            }
        }
    }

    public IEnumerable<ToDoItem> GetPagedToDoItems(int pageSize, int pageNumber)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // Define parameters
            var parameters = new DynamicParameters();
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@PageSize", pageSize);

            // Call the stored procedure
            var toDoItems = connection.Query<ToDoItem>("GetPagedToDoItems", parameters, commandType: CommandType.StoredProcedure);

            return toDoItems;
        }
    }

    public void EnsureGetUnfinishedTodosStoredProcedureExists()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string sql = @"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUnfinishedTodos]') AND type in (N'P', N'PC'))
                BEGIN
                    EXEC sp_executesql N'
                    CREATE PROCEDURE [dbo].[GetUnfinishedTodos]
                    AS
                    BEGIN
                        SELECT * FROM TodoItems WHERE IsCompleted = 0
                    END'
                END";

            db.Execute(sql);
        }
    }

    public IEnumerable<ToDoItem> GetUnfinishedTodos()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            return db.Query<ToDoItem>("GetUnfinishedTodos", commandType: CommandType.StoredProcedure);
        }
    }
}

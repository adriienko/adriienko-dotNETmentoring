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

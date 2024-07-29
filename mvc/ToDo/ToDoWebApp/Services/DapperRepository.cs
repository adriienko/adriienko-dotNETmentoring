using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using ToDo.Models;

namespace ToDo.Services;

public class DapperRepository : IToDoListService
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

    public List<ToDoItem> GetItemList()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM TodoItems";
            return db.Query<ToDoItem>(sql).AsList();
        }
    }

    public Guid Create(ToDoItem item)
    {
        item.Id = Guid.NewGuid();
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string sql = "INSERT INTO TodoItems (Id, Title, IsCompleted) VALUES (@Id, @Title, @IsCompleted)";
            db.Execute(sql, item);
            return item.Id;
        }
    }

    public ToDoItem? Read(Guid id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM TodoItems WHERE Id = @Id";
            return db.QueryFirstOrDefault<ToDoItem>(sql, new { Id = id });
        }
    }

    public void Delete(Guid id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string sql = "DELETE FROM TodoItems WHERE Id = @Id";
            db.Execute(sql, new { Id = id });
        }
    }

    public void Update(ToDoItem item)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string sql = "UPDATE TodoItems SET Title = @Title, IsCompleted = @IsCompleted WHERE Id = @Id";
            db.Execute(sql, item);
        }
    }
}

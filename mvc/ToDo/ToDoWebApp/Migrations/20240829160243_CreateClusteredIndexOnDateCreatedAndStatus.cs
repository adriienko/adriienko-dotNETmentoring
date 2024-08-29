using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class CreateClusteredIndexOnDateCreatedAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing primary key (which is clustered)
            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems");

            // Create a new primary key that is non-clustered
            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems",
                column: "Id")
                .Annotation("SqlServer:Clustered", false); // non-clustered primary key

            // Create a clustered index on DateCreated and IsCompleted
            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_DateCreated_IsCompleted",
                table: "ToDoItems",
                columns: new[] { "DateCreated", "IsCompleted" })
                .Annotation("SqlServer:Clustered", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the clustered index on DateCreated and IsCompleted
            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_DateCreated_IsCompleted",
                table: "ToDoItems");

            // Drop the non-clustered primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems");

            // Recreate the original clustered primary key on Id
            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems",
                column: "Id");
        }
    }
}

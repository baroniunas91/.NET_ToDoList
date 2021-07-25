using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.RestfulAPI.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "IsDone", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, false, "Wash dishes", 2 },
                    { 2, false, "Clean table", 2 },
                    { 3, false, "Wash car", 2 },
                    { 4, false, "Do homework", 3 },
                    { 5, false, "Go to the gym", 3 },
                    { 6, false, "Write a book", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}

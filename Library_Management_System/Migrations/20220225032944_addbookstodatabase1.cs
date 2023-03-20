using Microsoft.EntityFrameworkCore.Migrations;

namespace Library_Management_System.Migrations
{
    public partial class addbookstodatabase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Published",
                table: "Books",
                type: "int",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Library_Management_System.Migrations
{
    public partial class addpublishertodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Published",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookPublishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PubAdd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PubContact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPublishers", x => x.PublisherId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookPublishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "BookPublishers",
                principalColumn: "PublisherId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookPublishers_PublisherId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookPublishers");

            migrationBuilder.DropIndex(
                name: "IX_Books_PublisherId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Books");
        }
    }
}

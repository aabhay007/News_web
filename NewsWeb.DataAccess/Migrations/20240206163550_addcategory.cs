using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsWeb.DataAccess.Migrations
{
    public partial class addcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Newses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Newses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "External_Url",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Newses_CategoryId",
                table: "Newses",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Newses_Categories_CategoryId",
                table: "Newses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Newses_Categories_CategoryId",
                table: "Newses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Newses_CategoryId",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "External_Url",
                table: "Newses");
        }
    }
}

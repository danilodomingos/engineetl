using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineETL.Infrastructure.Data.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ExpectedFormat",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    Login = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    LastAccess = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedFormat_UserId",
                table: "ExpectedFormat",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpectedFormat_User_UserId",
                table: "ExpectedFormat",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpectedFormat_User_UserId",
                table: "ExpectedFormat");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_ExpectedFormat_UserId",
                table: "ExpectedFormat");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExpectedFormat");
        }
    }
}

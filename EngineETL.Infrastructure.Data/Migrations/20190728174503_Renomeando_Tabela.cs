using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineETL.Infrastructure.Data.Migrations
{
    public partial class Renomeando_Tabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpectedFormat");

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    PropertyCity = table.Column<string>(maxLength: 200, nullable: true),
                    CityPropertyName = table.Column<string>(maxLength: 100, nullable: true),
                    CityPropertyHabitants = table.Column<string>(maxLength: 100, nullable: true),
                    PropertyNeighborhood = table.Column<string>(maxLength: 100, nullable: true),
                    NeighborhoodPropertyName = table.Column<string>(maxLength: 100, nullable: true),
                    NeighborhoodPropertyHabitants = table.Column<string>(maxLength: 100, nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Template_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Template_UserId",
                table: "Template",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.CreateTable(
                name: "ExpectedFormat",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    CityPropertyHabitants = table.Column<string>(maxLength: 100, nullable: true),
                    CityPropertyName = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    NeighborhoodPropertyHabitants = table.Column<string>(maxLength: 100, nullable: true),
                    NeighborhoodPropertyName = table.Column<string>(maxLength: 100, nullable: true),
                    PropertyCity = table.Column<string>(maxLength: 200, nullable: true),
                    PropertyNeighborhood = table.Column<string>(maxLength: 100, nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpectedFormat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpectedFormat_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedFormat_UserId",
                table: "ExpectedFormat",
                column: "UserId");
        }
    }
}

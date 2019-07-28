using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineETL.Infrastructure.Data.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpectedFormat",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    PropertyCity = table.Column<string>(maxLength: 200, nullable: true),
                    CityPropertyName = table.Column<string>(maxLength: 100, nullable: true),
                    CityPropertyHabitants = table.Column<string>(maxLength: 100, nullable: true),
                    PropertyNeighborhood = table.Column<string>(maxLength: 100, nullable: true),
                    NeighborhoodPropertyName = table.Column<string>(maxLength: 100, nullable: true),
                    NeighborhoodPropertyHabitants = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpectedFormat", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpectedFormat");
        }
    }
}

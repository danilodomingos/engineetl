using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineETL.Infrastructure.Data.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "LastAccess", "Login", "Password" },
                values: new object[] { new Guid("04cc9f3e-7014-4bda-9af6-08d713670934"), new DateTime(2019, 7, 28, 18, 36, 31, 430, DateTimeKind.Local).AddTicks(9002), "usrteste@gmail.com", "1234" });

            migrationBuilder.InsertData(
                table: "Template",
                columns: new[] { "Id", "CityPropertyHabitants", "CityPropertyName", "Name", "NeighborhoodPropertyHabitants", "NeighborhoodPropertyName", "PropertyCity", "PropertyNeighborhood", "UserId" },
                values: new object[] { new Guid("f342789f-8376-45c0-34ad-08d71388325b"), "cidade.populacao", "cidade.nome", "# Exemplo 1 - Template Estado Rio de Janeiro", "bairro.populacao", "bairro.nome", "corpo.cidade", "cidades.bairros.bairro", new Guid("04cc9f3e-7014-4bda-9af6-08d713670934") });

            migrationBuilder.InsertData(
                table: "Template",
                columns: new[] { "Id", "CityPropertyHabitants", "CityPropertyName", "Name", "NeighborhoodPropertyHabitants", "NeighborhoodPropertyName", "PropertyCity", "PropertyNeighborhood", "UserId" },
                values: new object[] { new Guid("5019b3d0-aa57-440c-b9cc-08d7138a0915"), "city.population", "city.name", "# Exemplo 2 - Template Estado Minas Gerais", "neighborhood.population", "neighborhood.name", "body.region.cities.city", "city.neighborhoods.neighborhood", new Guid("04cc9f3e-7014-4bda-9af6-08d713670934") });

            migrationBuilder.InsertData(
                table: "Template",
                columns: new[] { "Id", "CityPropertyHabitants", "CityPropertyName", "Name", "NeighborhoodPropertyHabitants", "NeighborhoodPropertyName", "PropertyCity", "PropertyNeighborhood", "UserId" },
                values: new object[] { new Guid("73896430-392a-4d76-b9cd-08d7138a0915"), "cities.population", "cities.name", "# Exemplo 3 - Template Estado do Acre", "neighborhoods.population", "neighborhoods.name", "cities", "cities.neighborhoods", new Guid("04cc9f3e-7014-4bda-9af6-08d713670934") });

            migrationBuilder.CreateIndex(
                name: "IX_Template_UserId",
                table: "Template",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

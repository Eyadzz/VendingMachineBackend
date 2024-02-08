using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "VendingMachine");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Balance = table.Column<float>(type: "real", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Users",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "VendingMachine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SellerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "Users",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Users",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Buyer" },
                    { 2, "Seller" }
                });

            migrationBuilder.InsertData(
                schema: "Users",
                table: "Users",
                columns: new[] { "Id", "Balance", "CreatedAt", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, 100f, new DateTime(2024, 2, 8, 22, 34, 16, 355, DateTimeKind.Utc).AddTicks(8883), "$2a$11$Pjl9qxNSCv9rSUp1W5/Tb..q/fHcEAkiG5tWK2sosV8onQ91kyivq", 1, "Buyer" },
                    { 2, 0f, new DateTime(2024, 2, 8, 22, 34, 16, 355, DateTimeKind.Utc).AddTicks(8888), "$2a$11$Pjl9qxNSCv9rSUp1W5/Tb..q/fHcEAkiG5tWK2sosV8onQ91kyivq", 2, "Seller" }
                });

            migrationBuilder.InsertData(
                schema: "VendingMachine",
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Name", "Price", "Quantity", "SellerId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 8, 22, 34, 16, 355, DateTimeKind.Utc).AddTicks(8903), "Chips", 5f, 100, 2, null },
                    { 2, new DateTime(2024, 2, 8, 22, 34, 16, 355, DateTimeKind.Utc).AddTicks(8907), "Coke", 11f, 30, 2, null },
                    { 3, new DateTime(2024, 2, 8, 22, 34, 16, 355, DateTimeKind.Utc).AddTicks(8908), "Biscuits", 2.5f, 56, 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                schema: "VendingMachine",
                table: "Products",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "Users",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "VendingMachine");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Users");
        }
    }
}

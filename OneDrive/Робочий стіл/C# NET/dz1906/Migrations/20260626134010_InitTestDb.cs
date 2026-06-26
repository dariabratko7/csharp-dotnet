using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dz1906.Migrations
{
    /// <inheritdoc />
    public partial class InitTestDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 120.50m, 1 },
                    { 2, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 250.00m, 2 },
                    { 3, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.30m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Dell XPS 13 Laptop", 1200.99m },
                    { 2, "Logitech MX Master 3 Mouse", 99.99m },
                    { 3, "LG UltraGear 27\" Monitor", 399.99m },
                    { 4, "Razer BlackWidow Keyboard", 149.50m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "IsActive", "Name", "RegistrationDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john@example.com", true, "John Doe", new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1995, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice@example.com", true, "Alice Smith", new DateTime(2022, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(1985, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael@example.com", false, "Michael Johnson", new DateTime(2021, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

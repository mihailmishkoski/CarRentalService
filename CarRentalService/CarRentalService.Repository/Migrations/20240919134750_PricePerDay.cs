using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalService.Repository.Migrations
{
    /// <inheritdoc />
    public partial class PricePerDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RentParam",
                keyColumn: "Id",
                keyValue: new Guid("ec550000-160a-4ebd-b174-64ca36f0df62"));

            migrationBuilder.AddColumn<int>(
                name: "PricePerDay",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "RentParam",
                columns: new[] { "Id", "AdditionalFees", "DiscountPercentage", "MinimumDaysForRent" },
                values: new object[] { new Guid("f23efcb2-4a0a-4659-a211-5cc9a783929c"), 10.0m, 5.0m, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RentParam",
                keyColumn: "Id",
                keyValue: new Guid("f23efcb2-4a0a-4659-a211-5cc9a783929c"));

            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "RentParam",
                columns: new[] { "Id", "AdditionalFees", "DiscountPercentage", "MinimumDaysForRent" },
                values: new object[] { new Guid("ec550000-160a-4ebd-b174-64ca36f0df62"), 10.0m, 5.0m, 1 });
        }
    }
}

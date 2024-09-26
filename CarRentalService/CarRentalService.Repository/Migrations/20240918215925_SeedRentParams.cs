using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalService.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SeedRentParams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RentParam",
                columns: new[] { "Id", "AdditionalFees", "DiscountPercentage", "MinimumDaysForRent" },
                values: new object[] { new Guid("ec550000-160a-4ebd-b174-64ca36f0df62"), 10.0m, 5.0m, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RentParam",
                keyColumn: "Id",
                keyValue: new Guid("ec550000-160a-4ebd-b174-64ca36f0df62"));
        }
    }
}

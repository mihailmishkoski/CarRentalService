using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalService.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AmountToPayNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RentParam",
                keyColumn: "Id",
                keyValue: new Guid("f23efcb2-4a0a-4659-a211-5cc9a783929c"));

            migrationBuilder.AlterColumn<int>(
                name: "RentAmount",
                table: "Rents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "RentParam",
                columns: new[] { "Id", "AdditionalFees", "DiscountPercentage", "MinimumDaysForRent" },
                values: new object[] { new Guid("c80e42a3-2da4-41d3-a89e-a35c1ec02ae6"), 10.0m, 5.0m, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RentParam",
                keyColumn: "Id",
                keyValue: new Guid("c80e42a3-2da4-41d3-a89e-a35c1ec02ae6"));

            migrationBuilder.AlterColumn<int>(
                name: "RentAmount",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "RentParam",
                columns: new[] { "Id", "AdditionalFees", "DiscountPercentage", "MinimumDaysForRent" },
                values: new object[] { new Guid("f23efcb2-4a0a-4659-a211-5cc9a783929c"), 10.0m, 5.0m, 1 });
        }
    }
}

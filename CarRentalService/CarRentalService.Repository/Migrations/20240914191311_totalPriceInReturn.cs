using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalService.Repository.Migrations
{
    /// <inheritdoc />
    public partial class totalPriceInReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Returns",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Returns");
        }
    }
}

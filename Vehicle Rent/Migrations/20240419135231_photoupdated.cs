using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicle_Rent.Migrations
{
    /// <inheritdoc />
    public partial class photoupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "RentalItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "RentalItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

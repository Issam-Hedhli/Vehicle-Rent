using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicle_Rent.Migrations
{
    /// <inheritdoc />
    public partial class changestruct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_RentalItems_RentalId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RentalId",
                table: "Ratings");

            migrationBuilder.AddColumn<string>(
                name: "RatingId",
                table: "RentalItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RentalId",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalItems_RatingId",
                table: "RentalItems",
                column: "RatingId",
                unique: true,
                filter: "[RatingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalItems_Ratings_RatingId",
                table: "RentalItems",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalItems_Ratings_RatingId",
                table: "RentalItems");

            migrationBuilder.DropIndex(
                name: "IX_RentalItems_RatingId",
                table: "RentalItems");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "RentalItems");

            migrationBuilder.AlterColumn<string>(
                name: "RentalId",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RentalId",
                table: "Ratings",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_RentalItems_RentalId",
                table: "Ratings",
                column: "RentalId",
                principalTable: "RentalItems",
                principalColumn: "Id");
        }
    }
}

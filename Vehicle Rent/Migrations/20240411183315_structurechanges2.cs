using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicle_Rent.Migrations
{
    /// <inheritdoc />
    public partial class structurechanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleCopies_Vehicles_IdVehicle",
                table: "VehicleCopies");

            migrationBuilder.AlterColumn<string>(
                name: "IdVehicle",
                table: "VehicleCopies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleCopies_Vehicles_IdVehicle",
                table: "VehicleCopies",
                column: "IdVehicle",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleCopies_Vehicles_IdVehicle",
                table: "VehicleCopies");

            migrationBuilder.AlterColumn<string>(
                name: "IdVehicle",
                table: "VehicleCopies",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleCopies_Vehicles_IdVehicle",
                table: "VehicleCopies",
                column: "IdVehicle",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}

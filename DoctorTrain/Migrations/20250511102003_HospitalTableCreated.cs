using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorTrain.Migrations
{
    /// <inheritdoc />
    public partial class HospitalTableCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Hospitals",
                newName: "District");

            migrationBuilder.AddColumn<string>(
                name: "ContactLocation",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactLocation",
                table: "Hospitals");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Hospitals",
                newName: "Location");
        }
    }
}

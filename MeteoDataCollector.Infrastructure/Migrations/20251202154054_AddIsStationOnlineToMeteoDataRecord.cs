using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeteoDataCollector.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsStationOnlineToMeteoDataRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStationOnline",
                table: "meteo_data_record",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStationOnline",
                table: "meteo_data_record");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsigniApi.Migrations
{
    /// <inheritdoc />
    public partial class AskedMeToMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Insignias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Insignias",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

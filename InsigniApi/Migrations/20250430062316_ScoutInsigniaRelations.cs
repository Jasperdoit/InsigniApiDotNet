using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsigniApi.Migrations
{
    /// <inheritdoc />
    public partial class ScoutInsigniaRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoutAssignments",
                columns: table => new
                {
                    CompletedAssignmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScoutsWithAssignmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutAssignments", x => new { x.CompletedAssignmentsId, x.ScoutsWithAssignmentId });
                    table.ForeignKey(
                        name: "FK_ScoutAssignments_Assignments_CompletedAssignmentsId",
                        column: x => x.CompletedAssignmentsId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoutAssignments_Scouts_ScoutsWithAssignmentId",
                        column: x => x.ScoutsWithAssignmentId,
                        principalTable: "Scouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoutInsignias",
                columns: table => new
                {
                    CompletedInsigniasId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScoutsWithInsigniaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutInsignias", x => new { x.CompletedInsigniasId, x.ScoutsWithInsigniaId });
                    table.ForeignKey(
                        name: "FK_ScoutInsignias_Insignias_CompletedInsigniasId",
                        column: x => x.CompletedInsigniasId,
                        principalTable: "Insignias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoutInsignias_Scouts_ScoutsWithInsigniaId",
                        column: x => x.ScoutsWithInsigniaId,
                        principalTable: "Scouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoutAssignments_ScoutsWithAssignmentId",
                table: "ScoutAssignments",
                column: "ScoutsWithAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoutInsignias_ScoutsWithInsigniaId",
                table: "ScoutInsignias",
                column: "ScoutsWithInsigniaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoutAssignments");

            migrationBuilder.DropTable(
                name: "ScoutInsignias");
        }
    }
}

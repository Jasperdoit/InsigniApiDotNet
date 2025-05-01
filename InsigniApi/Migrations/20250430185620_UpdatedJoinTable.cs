using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsigniApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoutAssignments");

            migrationBuilder.DropTable(
                name: "ScoutInsignias");

            migrationBuilder.CreateTable(
                name: "ScoutAssignment",
                columns: table => new
                {
                    ScoutId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LeaderSignature = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutAssignment", x => new { x.ScoutId, x.AssignmentId });
                    table.ForeignKey(
                        name: "FK_ScoutAssignment_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoutAssignment_Scouts_ScoutId",
                        column: x => x.ScoutId,
                        principalTable: "Scouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoutInsignia",
                columns: table => new
                {
                    ScoutId = table.Column<Guid>(type: "uuid", nullable: false),
                    InsigniaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateAwarded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutInsignia", x => new { x.ScoutId, x.InsigniaId });
                    table.ForeignKey(
                        name: "FK_ScoutInsignia_Insignias_InsigniaId",
                        column: x => x.InsigniaId,
                        principalTable: "Insignias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoutInsignia_Scouts_ScoutId",
                        column: x => x.ScoutId,
                        principalTable: "Scouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoutAssignment_AssignmentId",
                table: "ScoutAssignment",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoutInsignia_InsigniaId",
                table: "ScoutInsignia",
                column: "InsigniaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoutAssignment");

            migrationBuilder.DropTable(
                name: "ScoutInsignia");

            migrationBuilder.CreateTable(
                name: "ScoutAssignments",
                columns: table => new
                {
                    CompletedAssignmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScoutsWithAssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LeaderSignature = table.Column<string>(type: "text", nullable: true)
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
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsigniApi.Migrations
{
    /// <inheritdoc />
    public partial class ForgotTablesForAssignmentsAndInsigniaJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoutAssignment_Assignments_AssignmentId",
                table: "ScoutAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoutAssignment_Scouts_ScoutId",
                table: "ScoutAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoutInsignia_Insignias_InsigniaId",
                table: "ScoutInsignia");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoutInsignia_Scouts_ScoutId",
                table: "ScoutInsignia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoutInsignia",
                table: "ScoutInsignia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoutAssignment",
                table: "ScoutAssignment");

            migrationBuilder.RenameTable(
                name: "ScoutInsignia",
                newName: "ScoutInsignias");

            migrationBuilder.RenameTable(
                name: "ScoutAssignment",
                newName: "ScoutAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_ScoutInsignia_InsigniaId",
                table: "ScoutInsignias",
                newName: "IX_ScoutInsignias_InsigniaId");

            migrationBuilder.RenameIndex(
                name: "IX_ScoutAssignment_AssignmentId",
                table: "ScoutAssignments",
                newName: "IX_ScoutAssignments_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoutInsignias",
                table: "ScoutInsignias",
                columns: new[] { "ScoutId", "InsigniaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoutAssignments",
                table: "ScoutAssignments",
                columns: new[] { "ScoutId", "AssignmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutAssignments_Assignments_AssignmentId",
                table: "ScoutAssignments",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutAssignments_Scouts_ScoutId",
                table: "ScoutAssignments",
                column: "ScoutId",
                principalTable: "Scouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutInsignias_Insignias_InsigniaId",
                table: "ScoutInsignias",
                column: "InsigniaId",
                principalTable: "Insignias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutInsignias_Scouts_ScoutId",
                table: "ScoutInsignias",
                column: "ScoutId",
                principalTable: "Scouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoutAssignments_Assignments_AssignmentId",
                table: "ScoutAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoutAssignments_Scouts_ScoutId",
                table: "ScoutAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoutInsignias_Insignias_InsigniaId",
                table: "ScoutInsignias");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoutInsignias_Scouts_ScoutId",
                table: "ScoutInsignias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoutInsignias",
                table: "ScoutInsignias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoutAssignments",
                table: "ScoutAssignments");

            migrationBuilder.RenameTable(
                name: "ScoutInsignias",
                newName: "ScoutInsignia");

            migrationBuilder.RenameTable(
                name: "ScoutAssignments",
                newName: "ScoutAssignment");

            migrationBuilder.RenameIndex(
                name: "IX_ScoutInsignias_InsigniaId",
                table: "ScoutInsignia",
                newName: "IX_ScoutInsignia_InsigniaId");

            migrationBuilder.RenameIndex(
                name: "IX_ScoutAssignments_AssignmentId",
                table: "ScoutAssignment",
                newName: "IX_ScoutAssignment_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoutInsignia",
                table: "ScoutInsignia",
                columns: new[] { "ScoutId", "InsigniaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoutAssignment",
                table: "ScoutAssignment",
                columns: new[] { "ScoutId", "AssignmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutAssignment_Assignments_AssignmentId",
                table: "ScoutAssignment",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutAssignment_Scouts_ScoutId",
                table: "ScoutAssignment",
                column: "ScoutId",
                principalTable: "Scouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutInsignia_Insignias_InsigniaId",
                table: "ScoutInsignia",
                column: "InsigniaId",
                principalTable: "Insignias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutInsignia_Scouts_ScoutId",
                table: "ScoutInsignia",
                column: "ScoutId",
                principalTable: "Scouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

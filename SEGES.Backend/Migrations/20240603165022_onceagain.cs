using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEGES.Backend.Migrations
{
    /// <inheritdoc />
    public partial class onceagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_Goal_GoalId",
                table: "KPIs");

            migrationBuilder.DropForeignKey(
                name: "FK_Rel_IssueGoals_Goal_GoalId",
                table: "Rel_IssueGoals");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Goal_GoalId",
                table: "Requirements");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Goal_Goal_ID",
                table: "Requirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goal",
                table: "Goal");

            migrationBuilder.RenameTable(
                name: "Goal",
                newName: "Goals");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Goals",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "Goals",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Project_ID",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goals",
                table: "Goals",
                column: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Projects_GoalId",
                table: "Goals",
                column: "GoalId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_Goals_GoalId",
                table: "KPIs",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rel_IssueGoals_Goals_GoalId",
                table: "Rel_IssueGoals",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Goals_GoalId",
                table: "Requirements",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Goals_Goal_ID",
                table: "Requirements",
                column: "Goal_ID",
                principalTable: "Goals",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Projects_GoalId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_Goals_GoalId",
                table: "KPIs");

            migrationBuilder.DropForeignKey(
                name: "FK_Rel_IssueGoals_Goals_GoalId",
                table: "Rel_IssueGoals");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Goals_GoalId",
                table: "Requirements");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Goals_Goal_ID",
                table: "Requirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goals",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Project_ID",
                table: "Goals");

            migrationBuilder.RenameTable(
                name: "Goals",
                newName: "Goal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Goal",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "Goal",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goal",
                table: "Goal",
                column: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_Goal_GoalId",
                table: "KPIs",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rel_IssueGoals_Goal_GoalId",
                table: "Rel_IssueGoals",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Goal_GoalId",
                table: "Requirements",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Goal_Goal_ID",
                table: "Requirements",
                column: "Goal_ID",
                principalTable: "Goal",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEGES.Backend.Migrations
{
    /// <inheritdoc />
    public partial class requirementGoalrel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "Requirements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_Goal_ID",
                table: "Requirements",
                column: "Goal_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Goal_Goal_ID",
                table: "Requirements",
                column: "Goal_ID",
                principalTable: "Goal",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Goal_Goal_ID",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_Goal_ID",
                table: "Requirements");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "Requirements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

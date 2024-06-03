using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEGES.Backend.Migrations
{
    /// <inheritdoc />
    public partial class projectRequirementsrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Project_ID",
                table: "Requirements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_Project_ID",
                table: "Requirements",
                column: "Project_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Projects_Project_ID",
                table: "Requirements",
                column: "Project_ID",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Projects_Project_ID",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_Project_ID",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "Project_ID",
                table: "Requirements");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEGES.Backend.Migrations
{
    /// <inheritdoc />
    public partial class LearnMoreComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Projects_GoalId",
                table: "Goals");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "Goals",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "LearnMoreComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnMoreComments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_Project_ID",
                table: "Goals",
                column: "Project_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Projects_Project_ID",
                table: "Goals",
                column: "Project_ID",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Projects_Project_ID",
                table: "Goals");

            migrationBuilder.DropTable(
                name: "LearnMoreComments");

            migrationBuilder.DropIndex(
                name: "IX_Goals_Project_ID",
                table: "Goals");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "Goals",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Projects_GoalId",
                table: "Goals",
                column: "GoalId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

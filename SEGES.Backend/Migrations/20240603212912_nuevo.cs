using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEGES.Backend.Migrations
{
    /// <inheritdoc />
    public partial class nuevo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Goals_Goal_ID",
                table: "Requirements");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Projects_Project_ID",
                table: "Requirements");

            migrationBuilder.DropTable(
                name: "Rel_IssueGoals");

            migrationBuilder.DropTable(
                name: "Rel_RolPermissions");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_Goal_ID",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_Project_ID",
                table: "Requirements");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Requirements",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Goal_Id",
                table: "KPIs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "KPIs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_ProjectId",
                table: "Requirements",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Projects_ProjectId",
                table: "Requirements",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Projects_ProjectId",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_ProjectId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Requirements");

            migrationBuilder.AlterColumn<int>(
                name: "Goal_Id",
                table: "KPIs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "KPIs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Rel_IssueGoals",
                columns: table => new
                {
                    Issue_ID = table.Column<int>(type: "int", nullable: false),
                    Goal_ID = table.Column<int>(type: "int", nullable: false),
                    GoalId = table.Column<int>(type: "int", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rel_IssueGoalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_IssueGoals", x => new { x.Issue_ID, x.Goal_ID });
                    table.ForeignKey(
                        name: "FK_Rel_IssueGoals_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "GoalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rel_IssueGoals_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "IssueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rel_RolPermissions",
                columns: table => new
                {
                    Role_ID = table.Column<int>(type: "int", nullable: false),
                    Permission_ID = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RolePermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_RolPermissions", x => new { x.Role_ID, x.Permission_ID });
                    table.ForeignKey(
                        name: "FK_Rel_RolPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rel_RolPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_Goal_ID",
                table: "Requirements",
                column: "Goal_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_Project_ID",
                table: "Requirements",
                column: "Project_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_IssueGoals_GoalId",
                table: "Rel_IssueGoals",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_IssueGoals_IssueId",
                table: "Rel_IssueGoals",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_RolPermissions_PermissionId",
                table: "Rel_RolPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_RolPermissions_RoleId",
                table: "Rel_RolPermissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Goals_Goal_ID",
                table: "Requirements",
                column: "Goal_ID",
                principalTable: "Goals",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Projects_Project_ID",
                table: "Requirements",
                column: "Project_ID",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

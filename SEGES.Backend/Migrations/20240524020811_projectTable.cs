using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEGES.Backend.Migrations
{
    /// <inheritdoc />
    public partial class projectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectManagerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_RequirementsEngineerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_StakeHolderId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectStatuses_ProjectStatusId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectManagerId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectStatusId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Projects_RequirementsEngineerId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StakeHolderId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectStatusId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "RequirementsEngineerId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StakeHolderId",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "States",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "StakeHolder_ID",
                table: "Project",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RequirementsEngineer_ID",
                table: "Project",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectManager_ID",
                table: "Project",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Project",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectManager_ID",
                table: "Project",
                column: "ProjectManager_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectStatus_ID",
                table: "Project",
                column: "ProjectStatus_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_RequirementsEngineer_ID",
                table: "Project",
                column: "RequirementsEngineer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StakeHolder_ID",
                table: "Project",
                column: "StakeHolder_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectManager_ID",
                table: "Project",
                column: "ProjectManager_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_RequirementsEngineer_ID",
                table: "Project",
                column: "RequirementsEngineer_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_StakeHolder_ID",
                table: "Project",
                column: "StakeHolder_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectStatuses_ProjectStatus_ID",
                table: "Project",
                column: "ProjectStatus_ID",
                principalTable: "ProjectStatuses",
                principalColumn: "ProjectStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectManager_ID",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_RequirementsEngineer_ID",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_StakeHolder_ID",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectStatuses_ProjectStatus_ID",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectManager_ID",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectStatus_ID",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Projects_RequirementsEngineer_ID",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StakeHolder_ID",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "States",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "StakeHolder_ID",
                table: "Project",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "RequirementsEngineer_ID",
                table: "Project",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectManager_ID",
                table: "Project",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Project",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "ProjectManagerId",
                table: "Project",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectStatusId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RequirementsEngineerId",
                table: "Project",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StakeHolderId",
                table: "Project",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectManagerId",
                table: "Project",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectStatusId",
                table: "Project",
                column: "ProjectStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_RequirementsEngineerId",
                table: "Project",
                column: "RequirementsEngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StakeHolderId",
                table: "Project",
                column: "StakeHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectManagerId",
                table: "Project",
                column: "ProjectManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_RequirementsEngineerId",
                table: "Project",
                column: "RequirementsEngineerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_StakeHolderId",
                table: "Project",
                column: "StakeHolderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectStatuses_ProjectStatusId",
                table: "Project",
                column: "ProjectStatusId",
                principalTable: "ProjectStatuses",
                principalColumn: "ProjectStatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

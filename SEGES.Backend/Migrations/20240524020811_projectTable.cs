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
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_RequirementsEngineerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_StakeHolderId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectStatuses_ProjectStatusId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectManagerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectStatusId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_RequirementsEngineerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StakeHolderId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectStatusId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "RequirementsEngineerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StakeHolderId",
                table: "Projects");

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
                table: "Projects",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RequirementsEngineer_ID",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectManager_ID",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectManager_ID",
                table: "Projects",
                column: "ProjectManager_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectStatus_ID",
                table: "Projects",
                column: "ProjectStatus_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_RequirementsEngineer_ID",
                table: "Projects",
                column: "RequirementsEngineer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StakeHolder_ID",
                table: "Projects",
                column: "StakeHolder_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectManager_ID",
                table: "Projects",
                column: "ProjectManager_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_RequirementsEngineer_ID",
                table: "Projects",
                column: "RequirementsEngineer_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_StakeHolder_ID",
                table: "Projects",
                column: "StakeHolder_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectStatuses_ProjectStatus_ID",
                table: "Projects",
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
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_RequirementsEngineer_ID",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_StakeHolder_ID",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectStatuses_ProjectStatus_ID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectManager_ID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectStatus_ID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_RequirementsEngineer_ID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StakeHolder_ID",
                table: "Projects");

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
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "RequirementsEngineer_ID",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectManager_ID",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "ProjectManagerId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectStatusId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RequirementsEngineerId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StakeHolderId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectStatusId",
                table: "Projects",
                column: "ProjectStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_RequirementsEngineerId",
                table: "Projects",
                column: "RequirementsEngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StakeHolderId",
                table: "Projects",
                column: "StakeHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_RequirementsEngineerId",
                table: "Projects",
                column: "RequirementsEngineerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_StakeHolderId",
                table: "Projects",
                column: "StakeHolderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectStatuses_ProjectStatusId",
                table: "Projects",
                column: "ProjectStatusId",
                principalTable: "ProjectStatuses",
                principalColumn: "ProjectStatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

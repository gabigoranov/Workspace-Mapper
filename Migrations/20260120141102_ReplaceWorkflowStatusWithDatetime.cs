using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowManager.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceWorkflowStatusWithDatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Workflows");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStartup",
                table: "Workflows",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastStartup",
                table: "Workflows");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Workflows",
                type: "INTEGER",
                maxLength: 30,
                nullable: false,
                defaultValue: 0);
        }
    }
}

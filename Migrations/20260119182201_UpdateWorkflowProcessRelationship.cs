using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkflowProcessRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Workflows_Process",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Workflows_WorkflowId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_Process",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "Process",
                table: "Processes");

            migrationBuilder.AlterColumn<int>(
                name: "WorkflowId",
                table: "Processes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Workflows_WorkflowId",
                table: "Processes",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Workflows_WorkflowId",
                table: "Processes");

            migrationBuilder.AlterColumn<int>(
                name: "WorkflowId",
                table: "Processes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Process",
                table: "Processes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Processes_Process",
                table: "Processes",
                column: "Process");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Workflows_Process",
                table: "Processes",
                column: "Process",
                principalTable: "Workflows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Workflows_WorkflowId",
                table: "Processes",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id");
        }
    }
}

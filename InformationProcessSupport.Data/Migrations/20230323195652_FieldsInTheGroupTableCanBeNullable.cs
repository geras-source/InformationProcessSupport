using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationProcessSupport.Data.Migrations
{
    /// <inheritdoc />
    public partial class FieldsInTheGroupTableCanBeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEntities_GroupEntities_GroupId",
                table: "ScheduleEntities");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleEntities_GroupId",
                table: "ScheduleEntities");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "ScheduleEntities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "GuildId",
                table: "GroupEntities",
                type: "decimal(20,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AlternateKey",
                table: "GroupEntities",
                type: "decimal(20,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntities_GroupId",
                table: "ScheduleEntities",
                column: "GroupId",
                unique: true,
                filter: "[GroupId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEntities_GroupEntities_GroupId",
                table: "ScheduleEntities",
                column: "GroupId",
                principalTable: "GroupEntities",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEntities_GroupEntities_GroupId",
                table: "ScheduleEntities");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleEntities_GroupId",
                table: "ScheduleEntities");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "ScheduleEntities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GuildId",
                table: "GroupEntities",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AlternateKey",
                table: "GroupEntities",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntities_GroupId",
                table: "ScheduleEntities",
                column: "GroupId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEntities_GroupEntities_GroupId",
                table: "ScheduleEntities",
                column: "GroupId",
                principalTable: "GroupEntities",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

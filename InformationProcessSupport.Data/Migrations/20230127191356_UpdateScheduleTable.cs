using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationProcessSupport.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEntities_ChannelsEntity_ChannelId",
                table: "ScheduleEntities");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleEntities_ChannelId",
                table: "ScheduleEntities");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "ScheduleEntities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "ScheduleEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubjectName",
                table: "ScheduleEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntities_ChannelId",
                table: "ScheduleEntities",
                column: "ChannelId",
                unique: true,
                filter: "[ChannelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntities_GroupId",
                table: "ScheduleEntities",
                column: "GroupId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEntities_ChannelsEntity_ChannelId",
                table: "ScheduleEntities",
                column: "ChannelId",
                principalTable: "ChannelsEntity",
                principalColumn: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEntities_GroupEntities_GroupId",
                table: "ScheduleEntities",
                column: "GroupId",
                principalTable: "GroupEntities",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEntities_ChannelsEntity_ChannelId",
                table: "ScheduleEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEntities_GroupEntities_GroupId",
                table: "ScheduleEntities");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleEntities_ChannelId",
                table: "ScheduleEntities");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleEntities_GroupId",
                table: "ScheduleEntities");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ScheduleEntities");

            migrationBuilder.DropColumn(
                name: "SubjectName",
                table: "ScheduleEntities");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "ScheduleEntities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntities_ChannelId",
                table: "ScheduleEntities",
                column: "ChannelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEntities_ChannelsEntity_ChannelId",
                table: "ScheduleEntities",
                column: "ChannelId",
                principalTable: "ChannelsEntity",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

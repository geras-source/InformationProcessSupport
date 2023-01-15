using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationProcessSupport.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTimesActionsEntityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CameraOperationTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "CameraTurnOffTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "CameraTurnOnTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "MicrophoneOperatingTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "MicrophoneTurnOffTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "MicrophoneTurnOnTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "StreamOperationTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "StreamTurnOffTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "StreamTurnOnTime",
                table: "Statistics");

            migrationBuilder.CreateTable(
                name: "TimesEntities",
                columns: table => new
                {
                    TimeEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MicrophoneOperatingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CameraOperationTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    StreamOperationTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CameraTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CameraTurnOffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MicrophoneTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MicrophoneTurnOffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StreamTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StreamTurnOffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatistisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesEntities", x => x.TimeEntityId);
                    table.ForeignKey(
                        name: "FK_TimesEntities_Statistics_StatistisId",
                        column: x => x.StatistisId,
                        principalTable: "Statistics",
                        principalColumn: "StatisticId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimesEntities_StatistisId",
                table: "TimesEntities",
                column: "StatistisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimesEntities");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CameraOperationTime",
                table: "Statistics",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "CameraTurnOffTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CameraTurnOnTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MicrophoneOperatingTime",
                table: "Statistics",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "MicrophoneTurnOffTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MicrophoneTurnOnTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StreamOperationTime",
                table: "Statistics",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "StreamTurnOffTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StreamTurnOnTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

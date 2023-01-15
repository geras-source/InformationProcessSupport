using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationProcessSupport.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReWriteStatisticModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "MicrophoneOperatingTime",
                table: "Statistics",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ConnectionTime",
                table: "Statistics",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CameraOperationTime",
                table: "Statistics",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExitTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CameraTurnOffTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "CameraTurnOnTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "EntryTime",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "ExitTime",
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "MicrophoneOperatingTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConnectionTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CameraOperationTime",
                table: "Statistics",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }
    }
}

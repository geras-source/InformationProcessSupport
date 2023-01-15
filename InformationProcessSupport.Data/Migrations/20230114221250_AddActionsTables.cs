using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationProcessSupport.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddActionsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimesEntities");

            migrationBuilder.CreateTable(
                name: "CameraActionsEntity",
                columns: table => new
                {
                    CameraActionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CameraOperationTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CameraTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CameraTurnOffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatistisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraActionsEntity", x => x.CameraActionsId);
                    table.ForeignKey(
                        name: "FK_CameraActionsEntity_Statistics_StatistisId",
                        column: x => x.StatistisId,
                        principalTable: "Statistics",
                        principalColumn: "StatisticId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MicrophoneActionsEntity",
                columns: table => new
                {
                    MicrophoneTimeEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MicrophoneOperatingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    MicrophoneTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MicrophoneTurnOffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatistisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicrophoneActionsEntity", x => x.MicrophoneTimeEntityId);
                    table.ForeignKey(
                        name: "FK_MicrophoneActionsEntity_Statistics_StatistisId",
                        column: x => x.StatistisId,
                        principalTable: "Statistics",
                        principalColumn: "StatisticId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelfDeafenedActionsEntities",
                columns: table => new
                {
                    SelfDeafenedActionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelfDeafenedOperationTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    SelfDeafenedTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SelfDeafenedTurnOffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatistisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfDeafenedActionsEntities", x => x.SelfDeafenedActionsId);
                    table.ForeignKey(
                        name: "FK_SelfDeafenedActionsEntities_Statistics_StatistisId",
                        column: x => x.StatistisId,
                        principalTable: "Statistics",
                        principalColumn: "StatisticId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreamActionsEntity",
                columns: table => new
                {
                    StreamActionsEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreamOperationTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    StreamTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StreamTurnOffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatistisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamActionsEntity", x => x.StreamActionsEntityId);
                    table.ForeignKey(
                        name: "FK_StreamActionsEntity_Statistics_StatistisId",
                        column: x => x.StatistisId,
                        principalTable: "Statistics",
                        principalColumn: "StatisticId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CameraActionsEntity_StatistisId",
                table: "CameraActionsEntity",
                column: "StatistisId");

            migrationBuilder.CreateIndex(
                name: "IX_MicrophoneActionsEntity_StatistisId",
                table: "MicrophoneActionsEntity",
                column: "StatistisId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeafenedActionsEntities_StatistisId",
                table: "SelfDeafenedActionsEntities",
                column: "StatistisId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamActionsEntity_StatistisId",
                table: "StreamActionsEntity",
                column: "StatistisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CameraActionsEntity");

            migrationBuilder.DropTable(
                name: "MicrophoneActionsEntity");

            migrationBuilder.DropTable(
                name: "SelfDeafenedActionsEntities");

            migrationBuilder.DropTable(
                name: "StreamActionsEntity");

            migrationBuilder.CreateTable(
                name: "TimesEntities",
                columns: table => new
                {
                    TimeEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatistisId = table.Column<int>(type: "int", nullable: false),
                    CameraOperationTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CameraTurnOffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CameraTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MicrophoneOperatingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    MicrophoneTurnOffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MicrophoneTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StreamOperationTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    StreamTurnOffTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StreamTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
    }
}

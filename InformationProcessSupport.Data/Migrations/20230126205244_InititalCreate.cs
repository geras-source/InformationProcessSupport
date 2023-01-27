using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationProcessSupport.Data.Migrations
{
    /// <inheritdoc />
    public partial class InititalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelsEntity",
                columns: table => new
                {
                    ChannelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlternateKey = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuildName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelsEntity", x => x.ChannelId);
                    table.UniqueConstraint("AK_ChannelsEntity_AlternateKey", x => x.AlternateKey);
                });

            migrationBuilder.CreateTable(
                name: "GroupEntities",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlternateKey = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEntities", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleEntities",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTimeTheSubject = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTimeTheSubject = table.Column<TimeSpan>(type: "time", nullable: false),
                    DayOfTheWeek = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleEntities", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_ScheduleEntities_ChannelsEntity_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ChannelsEntity",
                        principalColumn: "ChannelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersEntity",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlternateKey = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuildName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersEntity", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UsersEntity_GroupEntities_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupEntities",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    StatisticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Attendance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.StatisticId);
                    table.ForeignKey(
                        name: "FK_Statistics_ChannelsEntity_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ChannelsEntity",
                        principalColumn: "ChannelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistics_UsersEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersEntity",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    MicrophoneTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    SelfDeafenedTurnOnTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "IX_ScheduleEntities_ChannelId",
                table: "ScheduleEntities",
                column: "ChannelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeafenedActionsEntities_StatistisId",
                table: "SelfDeafenedActionsEntities",
                column: "StatistisId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_ChannelId",
                table: "Statistics",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamActionsEntity_StatistisId",
                table: "StreamActionsEntity",
                column: "StatistisId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersEntity_GroupId",
                table: "UsersEntity",
                column: "GroupId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CameraActionsEntity");

            migrationBuilder.DropTable(
                name: "MicrophoneActionsEntity");

            migrationBuilder.DropTable(
                name: "ScheduleEntities");

            migrationBuilder.DropTable(
                name: "SelfDeafenedActionsEntities");

            migrationBuilder.DropTable(
                name: "StreamActionsEntity");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "ChannelsEntity");

            migrationBuilder.DropTable(
                name: "UsersEntity");

            migrationBuilder.DropTable(
                name: "GroupEntities");
        }
    }
}

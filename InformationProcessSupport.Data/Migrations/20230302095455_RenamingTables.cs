using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationProcessSupport.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CameraActionsEntity_Statistics_StatistisId",
                table: "CameraActionsEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_MicrophoneActionsEntity_Statistics_StatistisId",
                table: "MicrophoneActionsEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEntities_ChannelsEntity_ChannelId",
                table: "ScheduleEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_ChannelsEntity_ChannelId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_UsersEntity_UserId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_StreamActionsEntity_Statistics_StatistisId",
                table: "StreamActionsEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersEntity_GroupEntities_GroupId",
                table: "UsersEntity");

            migrationBuilder.DropTable(
                name: "ChannelsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersEntity",
                table: "UsersEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StreamActionsEntity",
                table: "StreamActionsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MicrophoneActionsEntity",
                table: "MicrophoneActionsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CameraActionsEntity",
                table: "CameraActionsEntity");

            migrationBuilder.RenameTable(
                name: "UsersEntity",
                newName: "UserEntities");

            migrationBuilder.RenameTable(
                name: "StreamActionsEntity",
                newName: "StreamActionEntities");

            migrationBuilder.RenameTable(
                name: "MicrophoneActionsEntity",
                newName: "MicrophoneActionEntities");

            migrationBuilder.RenameTable(
                name: "CameraActionsEntity",
                newName: "CameraActionEntities");

            migrationBuilder.RenameIndex(
                name: "IX_UsersEntity_GroupId",
                table: "UserEntities",
                newName: "IX_UserEntities_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_StreamActionsEntity_StatistisId",
                table: "StreamActionEntities",
                newName: "IX_StreamActionEntities_StatistisId");

            migrationBuilder.RenameIndex(
                name: "IX_MicrophoneActionsEntity_StatistisId",
                table: "MicrophoneActionEntities",
                newName: "IX_MicrophoneActionEntities_StatistisId");

            migrationBuilder.RenameIndex(
                name: "IX_CameraActionsEntity_StatistisId",
                table: "CameraActionEntities",
                newName: "IX_CameraActionEntities_StatistisId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEntities",
                table: "UserEntities",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StreamActionEntities",
                table: "StreamActionEntities",
                column: "StreamActionsEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MicrophoneActionEntities",
                table: "MicrophoneActionEntities",
                column: "MicrophoneTimeEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CameraActionEntities",
                table: "CameraActionEntities",
                column: "CameraActionsId");

            migrationBuilder.CreateTable(
                name: "ChannelEntities",
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
                    table.PrimaryKey("PK_ChannelEntities", x => x.ChannelId);
                    table.UniqueConstraint("AK_ChannelEntities_AlternateKey", x => x.AlternateKey);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CameraActionEntities_Statistics_StatistisId",
                table: "CameraActionEntities",
                column: "StatistisId",
                principalTable: "Statistics",
                principalColumn: "StatisticId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MicrophoneActionEntities_Statistics_StatistisId",
                table: "MicrophoneActionEntities",
                column: "StatistisId",
                principalTable: "Statistics",
                principalColumn: "StatisticId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEntities_ChannelEntities_ChannelId",
                table: "ScheduleEntities",
                column: "ChannelId",
                principalTable: "ChannelEntities",
                principalColumn: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_ChannelEntities_ChannelId",
                table: "Statistics",
                column: "ChannelId",
                principalTable: "ChannelEntities",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_UserEntities_UserId",
                table: "Statistics",
                column: "UserId",
                principalTable: "UserEntities",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StreamActionEntities_Statistics_StatistisId",
                table: "StreamActionEntities",
                column: "StatistisId",
                principalTable: "Statistics",
                principalColumn: "StatisticId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEntities_GroupEntities_GroupId",
                table: "UserEntities",
                column: "GroupId",
                principalTable: "GroupEntities",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CameraActionEntities_Statistics_StatistisId",
                table: "CameraActionEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_MicrophoneActionEntities_Statistics_StatistisId",
                table: "MicrophoneActionEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEntities_ChannelEntities_ChannelId",
                table: "ScheduleEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_ChannelEntities_ChannelId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_UserEntities_UserId",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_StreamActionEntities_Statistics_StatistisId",
                table: "StreamActionEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEntities_GroupEntities_GroupId",
                table: "UserEntities");

            migrationBuilder.DropTable(
                name: "ChannelEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEntities",
                table: "UserEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StreamActionEntities",
                table: "StreamActionEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MicrophoneActionEntities",
                table: "MicrophoneActionEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CameraActionEntities",
                table: "CameraActionEntities");

            migrationBuilder.RenameTable(
                name: "UserEntities",
                newName: "UsersEntity");

            migrationBuilder.RenameTable(
                name: "StreamActionEntities",
                newName: "StreamActionsEntity");

            migrationBuilder.RenameTable(
                name: "MicrophoneActionEntities",
                newName: "MicrophoneActionsEntity");

            migrationBuilder.RenameTable(
                name: "CameraActionEntities",
                newName: "CameraActionsEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserEntities_GroupId",
                table: "UsersEntity",
                newName: "IX_UsersEntity_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_StreamActionEntities_StatistisId",
                table: "StreamActionsEntity",
                newName: "IX_StreamActionsEntity_StatistisId");

            migrationBuilder.RenameIndex(
                name: "IX_MicrophoneActionEntities_StatistisId",
                table: "MicrophoneActionsEntity",
                newName: "IX_MicrophoneActionsEntity_StatistisId");

            migrationBuilder.RenameIndex(
                name: "IX_CameraActionEntities_StatistisId",
                table: "CameraActionsEntity",
                newName: "IX_CameraActionsEntity_StatistisId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersEntity",
                table: "UsersEntity",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StreamActionsEntity",
                table: "StreamActionsEntity",
                column: "StreamActionsEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MicrophoneActionsEntity",
                table: "MicrophoneActionsEntity",
                column: "MicrophoneTimeEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CameraActionsEntity",
                table: "CameraActionsEntity",
                column: "CameraActionsId");

            migrationBuilder.CreateTable(
                name: "ChannelsEntity",
                columns: table => new
                {
                    ChannelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlternateKey = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CategoryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    GuildName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelsEntity", x => x.ChannelId);
                    table.UniqueConstraint("AK_ChannelsEntity_AlternateKey", x => x.AlternateKey);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CameraActionsEntity_Statistics_StatistisId",
                table: "CameraActionsEntity",
                column: "StatistisId",
                principalTable: "Statistics",
                principalColumn: "StatisticId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MicrophoneActionsEntity_Statistics_StatistisId",
                table: "MicrophoneActionsEntity",
                column: "StatistisId",
                principalTable: "Statistics",
                principalColumn: "StatisticId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEntities_ChannelsEntity_ChannelId",
                table: "ScheduleEntities",
                column: "ChannelId",
                principalTable: "ChannelsEntity",
                principalColumn: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_ChannelsEntity_ChannelId",
                table: "Statistics",
                column: "ChannelId",
                principalTable: "ChannelsEntity",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_UsersEntity_UserId",
                table: "Statistics",
                column: "UserId",
                principalTable: "UsersEntity",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StreamActionsEntity_Statistics_StatistisId",
                table: "StreamActionsEntity",
                column: "StatistisId",
                principalTable: "Statistics",
                principalColumn: "StatisticId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersEntity_GroupEntities_GroupId",
                table: "UsersEntity",
                column: "GroupId",
                principalTable: "GroupEntities",
                principalColumn: "GroupId");
        }
    }
}

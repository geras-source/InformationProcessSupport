using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationProcessSupport.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTheRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersEntity_GroupId",
                table: "UsersEntity");

            migrationBuilder.AddColumn<decimal>(
                name: "GuildId",
                table: "GroupEntities",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "GuildName",
                table: "GroupEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersEntity_GroupId",
                table: "UsersEntity",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersEntity_GroupId",
                table: "UsersEntity");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "GroupEntities");

            migrationBuilder.DropColumn(
                name: "GuildName",
                table: "GroupEntities");

            migrationBuilder.CreateIndex(
                name: "IX_UsersEntity_GroupId",
                table: "UsersEntity",
                column: "GroupId",
                unique: true,
                filter: "[GroupId] IS NOT NULL");
        }
    }
}

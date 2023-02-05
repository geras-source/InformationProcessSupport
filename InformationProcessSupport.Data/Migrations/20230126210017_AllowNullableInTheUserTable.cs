using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformationProcessSupport.Data.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullableInTheUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersEntity_GroupEntities_GroupId",
                table: "UsersEntity");

            migrationBuilder.DropIndex(
                name: "IX_UsersEntity_GroupId",
                table: "UsersEntity");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "UsersEntity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UsersEntity_GroupId",
                table: "UsersEntity",
                column: "GroupId",
                unique: true,
                filter: "[GroupId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersEntity_GroupEntities_GroupId",
                table: "UsersEntity",
                column: "GroupId",
                principalTable: "GroupEntities",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersEntity_GroupEntities_GroupId",
                table: "UsersEntity");

            migrationBuilder.DropIndex(
                name: "IX_UsersEntity_GroupId",
                table: "UsersEntity");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "UsersEntity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersEntity_GroupId",
                table: "UsersEntity",
                column: "GroupId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersEntity_GroupEntities_GroupId",
                table: "UsersEntity",
                column: "GroupId",
                principalTable: "GroupEntities",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

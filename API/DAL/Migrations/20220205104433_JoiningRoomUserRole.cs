using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class JoiningRoomUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomRoleId",
                table: "RoomUserJoint",
                newName: "CustomRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomUserJoint_CustomRoleId",
                table: "RoomUserJoint",
                column: "CustomRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUserJoint_CustomRoles_CustomRoleId",
                table: "RoomUserJoint",
                column: "CustomRoleId",
                principalTable: "CustomRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomUserJoint_CustomRoles_CustomRoleId",
                table: "RoomUserJoint");

            migrationBuilder.DropIndex(
                name: "IX_RoomUserJoint_CustomRoleId",
                table: "RoomUserJoint");

            migrationBuilder.RenameColumn(
                name: "CustomRoleId",
                table: "RoomUserJoint",
                newName: "RoomRoleId");
        }
    }
}

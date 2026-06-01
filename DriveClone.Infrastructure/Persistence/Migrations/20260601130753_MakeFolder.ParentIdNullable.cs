using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveClone.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeFolderParentIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFolder_AspNetUsers_UserId",
                table: "UserFolder");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFolder_Folders_FolderId",
                table: "UserFolder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFolder",
                table: "UserFolder");

            migrationBuilder.RenameTable(
                name: "UserFolder",
                newName: "UserFolders");

            migrationBuilder.RenameIndex(
                name: "IX_UserFolder_UserId_FolderId",
                table: "UserFolders",
                newName: "IX_UserFolders_UserId_FolderId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFolder_FolderId",
                table: "UserFolders",
                newName: "IX_UserFolders_FolderId");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Folders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFolders",
                table: "UserFolders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFolders_AspNetUsers_UserId",
                table: "UserFolders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFolders_Folders_FolderId",
                table: "UserFolders",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFolders_AspNetUsers_UserId",
                table: "UserFolders");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFolders_Folders_FolderId",
                table: "UserFolders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFolders",
                table: "UserFolders");

            migrationBuilder.RenameTable(
                name: "UserFolders",
                newName: "UserFolder");

            migrationBuilder.RenameIndex(
                name: "IX_UserFolders_UserId_FolderId",
                table: "UserFolder",
                newName: "IX_UserFolder_UserId_FolderId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFolders_FolderId",
                table: "UserFolder",
                newName: "IX_UserFolder_FolderId");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Folders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFolder",
                table: "UserFolder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFolder_AspNetUsers_UserId",
                table: "UserFolder",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFolder_Folders_FolderId",
                table: "UserFolder",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

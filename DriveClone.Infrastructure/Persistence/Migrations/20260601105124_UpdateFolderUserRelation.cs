using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveClone.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFolderUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Folders_AspNetUsers_OwnerId",
            //     table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_OwnerId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Folders");

            migrationBuilder.AddColumn<int>(
                name: "FolderId",
                table: "Folders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileType",
                table: "FilesMetaData",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FolderId",
                table: "FilesMetaData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserFolder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsOwner = table.Column<bool>(type: "bit", nullable: false),
                    AccessPermission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PubId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFolder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFolder_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFolder_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FolderId",
                table: "Folders",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesMetaData_FolderId",
                table: "FilesMetaData",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFolder_FolderId",
                table: "UserFolder",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFolder_UserId_FolderId",
                table: "UserFolder",
                columns: new[] { "UserId", "FolderId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FilesMetaData_Folders_FolderId",
                table: "FilesMetaData",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_FolderId",
                table: "Folders",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilesMetaData_Folders_FolderId",
                table: "FilesMetaData");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_FolderId",
                table: "Folders");

            migrationBuilder.DropTable(
                name: "UserFolder");

            migrationBuilder.DropIndex(
                name: "IX_Folders_FolderId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_FilesMetaData_FolderId",
                table: "FilesMetaData");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "FilesMetaData");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Folders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "FileType",
                table: "FilesMetaData",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_OwnerId",
                table: "Folders",
                column: "OwnerId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Folders_AspNetUsers_OwnerId",
            //     table: "Folders",
            //     column: "OwnerId",
            //     principalTable: "AspNetUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }
    }
}

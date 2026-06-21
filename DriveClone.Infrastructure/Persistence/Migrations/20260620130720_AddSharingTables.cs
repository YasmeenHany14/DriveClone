using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DriveClone.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSharingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_FolderId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_ParentId",
                table: "Folders");

            migrationBuilder.DropTable(
                name: "UserFiles");

            migrationBuilder.DropTable(
                name: "UserFolders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_FolderId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_ParentId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Folders");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Folders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "FilesMetaData",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "AccessPermissionRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Create = table.Column<bool>(type: "bit", nullable: false),
                    Update = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_AccessPermissionRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileShareLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_FileShareLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileShareLinks_AccessPermissionRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccessPermissionRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileShareLinks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileShareLinks_FilesMetaData_FileId",
                        column: x => x.FileId,
                        principalTable: "FilesMetaData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FolderShareLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FolderId = table.Column<int>(type: "int", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_FolderShareLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolderShareLinks_AccessPermissionRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccessPermissionRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderShareLinks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderShareLinks_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFileSharingPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_UserFileSharingPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFileSharingPermissions_AccessPermissionRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccessPermissionRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFileSharingPermissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFileSharingPermissions_FilesMetaData_FileId",
                        column: x => x.FileId,
                        principalTable: "FilesMetaData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserFolderSharingPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FolderId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_UserFolderSharingPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFolderSharingPermissions_AccessPermissionRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccessPermissionRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFolderSharingPermissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFolderSharingPermissions_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccessPermissionRoles",
                columns: new[] { "Id", "Create", "CreatedBy", "CreatedDate", "Delete", "DeletedBy", "DeletedDate", "ModifiedBy", "ModifiedDate", "Name", "PubId", "Read", "Update" },
                values: new object[,]
                {
                    { 1, false, "00000000-0000-0000-0000-000000000000", new DateTime(2026, 6, 20, 16, 7, 19, 636, DateTimeKind.Local).AddTicks(9902), false, null, null, null, null, "ReadOnly", "7760c632-0666-4802-88f8-e2ffa07014b6", true, false },
                    { 2, true, "00000000-0000-0000-0000-000000000000", new DateTime(2026, 6, 20, 16, 7, 19, 642, DateTimeKind.Local).AddTicks(4833), true, null, null, null, null, "ReadWrite", "908eb749-d8d5-4114-bd85-3c53b7f1157b", true, true },
                    { 3, false, "00000000-0000-0000-0000-000000000000", new DateTime(2026, 6, 20, 16, 7, 19, 642, DateTimeKind.Local).AddTicks(5288), false, null, null, null, null, "ReadWriteFileOnly", "3221576c-6496-41b6-a02f-8094523b0c85", true, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Folders_OwnerId",
                table: "Folders",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FileShareLinks_FileId",
                table: "FileShareLinks",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileShareLinks_OwnerId",
                table: "FileShareLinks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FileShareLinks_RoleId",
                table: "FileShareLinks",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderShareLinks_FolderId",
                table: "FolderShareLinks",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderShareLinks_OwnerId",
                table: "FolderShareLinks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderShareLinks_RoleId",
                table: "FolderShareLinks",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFileSharingPermissions_FileId",
                table: "UserFileSharingPermissions",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFileSharingPermissions_RoleId",
                table: "UserFileSharingPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFileSharingPermissions_UserId_FileId",
                table: "UserFileSharingPermissions",
                columns: new[] { "UserId", "FileId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFolderSharingPermissions_FolderId",
                table: "UserFolderSharingPermissions",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFolderSharingPermissions_RoleId",
                table: "UserFolderSharingPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFolderSharingPermissions_UserId_FolderId",
                table: "UserFolderSharingPermissions",
                columns: new[] { "UserId", "FolderId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_AspNetUsers_OwnerId",
                table: "Folders",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_AspNetUsers_OwnerId",
                table: "Folders");

            migrationBuilder.DropTable(
                name: "FileShareLinks");

            migrationBuilder.DropTable(
                name: "FolderShareLinks");

            migrationBuilder.DropTable(
                name: "UserFileSharingPermissions");

            migrationBuilder.DropTable(
                name: "UserFolderSharingPermissions");

            migrationBuilder.DropTable(
                name: "AccessPermissionRoles");

            migrationBuilder.DropIndex(
                name: "IX_Folders_OwnerId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Folders");

            migrationBuilder.AddColumn<int>(
                name: "FolderId",
                table: "Folders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Folders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "FilesMetaData",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.CreateTable(
                name: "UserFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileMetaDataId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessPermission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PubId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFiles_FilesMetaData_FileMetaDataId",
                        column: x => x.FileMetaDataId,
                        principalTable: "FilesMetaData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessPermission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOwner = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PubId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFolders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFolders_Folders_FolderId",
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
                name: "IX_Folders_ParentId",
                table: "Folders",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_FileMetaDataId",
                table: "UserFiles",
                column: "FileMetaDataId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_UserId_FileMetaDataId",
                table: "UserFiles",
                columns: new[] { "UserId", "FileMetaDataId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFolders_FolderId",
                table: "UserFolders",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFolders_UserId_FolderId",
                table: "UserFolders",
                columns: new[] { "UserId", "FolderId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_FolderId",
                table: "Folders",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_ParentId",
                table: "Folders",
                column: "ParentId",
                principalTable: "Folders",
                principalColumn: "Id");
        }
    }
}

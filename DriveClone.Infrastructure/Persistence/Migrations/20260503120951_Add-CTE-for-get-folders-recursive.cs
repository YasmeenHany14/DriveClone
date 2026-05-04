using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveClone.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCTEforgetfoldersrecursive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE PROC GetFoldersRecursive AS" +
                                 "BEGIN" +
                                 "WITH RECURSIVE parent AS (" +
                                 "SELECT " +
                                 "FROM" +
                                 "WHERE" +
                                 "UNION" +
                                 "SELECT" +
                                 "FROM" +
                                 "WHERE" +
                                 ")" +
                                 "SELECT * FROM parent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC DoStuff");
        }
    }
}

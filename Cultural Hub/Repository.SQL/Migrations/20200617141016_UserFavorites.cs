using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.SQL.Migrations
{
    public partial class UserFavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteEvents",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    EventListJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteEvents", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_FavoriteEvents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteEvents");
        }
    }
}

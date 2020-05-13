using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.SQL.Migrations
{
    public partial class AddedClientIdToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Events");
        }
    }
}

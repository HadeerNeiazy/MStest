using Microsoft.EntityFrameworkCore.Migrations;

namespace MStest.Migrations
{
    public partial class addMessageToChatMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "ChatMessages");
        }
    }
}

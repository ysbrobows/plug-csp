using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlugApi.Migrations
{
    public partial class add_projectkey_customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectKey",
                table: "Customers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectKey",
                table: "Customers");
        }
    }
}

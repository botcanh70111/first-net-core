using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogNetCore.Migrations
{
    public partial class AddUsersRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupervisorId",
                table: "AspNetUsers",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "AspNetUsers",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Menus",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Categories",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Tags",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "SiteConfigs",
                maxLength: 450,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "UserType", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "SupervisorId", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "OwnerId", table: "SiteConfigs");
            migrationBuilder.DropColumn(name: "OwnerId", table: "Tags");
            migrationBuilder.DropColumn(name: "OwnerId", table: "Categories");
            migrationBuilder.DropColumn(name: "OwnerId", table: "Menus");
        }
    }
}

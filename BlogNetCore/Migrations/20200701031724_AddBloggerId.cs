using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogNetCore.Migrations
{
    public partial class AddBloggerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloggerId",
                table: "Blogs",
                maxLength: 450,
                nullable: true);
            migrationBuilder.AddForeignKey(name: "FK_Blogs_Users_BloggerId", 
                table: "Blogs", 
                column: "BloggerId", 
                principalTable: "AspNetUsers", 
                principalColumn: "Id", 
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloggerId",
                table: "Blogs");
        }
    }
}

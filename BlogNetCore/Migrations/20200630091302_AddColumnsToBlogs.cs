using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BlogNetCore.Migrations
{
    public partial class AddColumnsToBlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Blogs",
                nullable: true,
                maxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "PostScript",
                table: "Blogs",
                nullable: true,
                maxLength: 1000);

            migrationBuilder.AddColumn<DateTime>(
                name: "Edited",
                table: "Blogs",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Views",
                table: "Blogs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Blogs");
            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "Blogs");
            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Blogs");
            migrationBuilder.DropColumn(
                name: "PostScript",
                table: "Blogs");
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Blogs");
        }
    }
}

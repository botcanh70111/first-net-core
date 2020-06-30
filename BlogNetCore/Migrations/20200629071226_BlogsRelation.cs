using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BlogNetCore.Migrations
{
    public partial class BlogsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogsTags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(nullable: false),
                    BlogId = table.Column<Guid>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogsTags", x => new { x.TagId, x.BlogId });
                    table.ForeignKey(
                        name: "FK_BlogsTags_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BlogsTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate: ReferentialAction.NoAction);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("BlogsTags");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class addnotnullconstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thoughts_AspNetUsers_AuthorId",
                table: "Thoughts");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Thoughts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Thoughts_AspNetUsers_AuthorId",
                table: "Thoughts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thoughts_AspNetUsers_AuthorId",
                table: "Thoughts");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Thoughts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Thoughts_AspNetUsers_AuthorId",
                table: "Thoughts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

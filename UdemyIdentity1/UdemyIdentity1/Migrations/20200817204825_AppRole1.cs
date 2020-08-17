using Microsoft.EntityFrameworkCore.Migrations;

namespace UdemyIdentity1.Migrations
{
    public partial class AppRole1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "AspNetUsers",
                newName: "Picture");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleType",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleType",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "AspNetUsers",
                newName: "MyProperty");
        }
    }
}

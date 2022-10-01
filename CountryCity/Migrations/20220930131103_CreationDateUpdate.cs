using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountryCity.Migrations
{
    public partial class CreationDateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationData",
                table: "AspNetRoles",
                newName: "CreationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "AspNetRoles",
                newName: "CreationData");
        }
    }
}

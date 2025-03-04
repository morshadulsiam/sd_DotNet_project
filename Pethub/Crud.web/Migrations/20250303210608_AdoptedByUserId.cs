using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crud.web.Migrations
{
    /// <inheritdoc />
    public partial class AdoptedByUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdoptedByUserId",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdoptedByUserId",
                table: "Pets");
        }
    }
}

﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crud.web.Migrations
{
    /// <inheritdoc />
    public partial class IsAdopted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdopted",
                table: "Pets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdopted",
                table: "Pets");
        }
    }
}

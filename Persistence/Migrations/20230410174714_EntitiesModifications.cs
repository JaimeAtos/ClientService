using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesModifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "ClientsPositions",
                newName: "DateLastModify");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "ClientsPositions",
                newName: "UserModifierId");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Clients",
                newName: "DateLastModify");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Clients",
                newName: "UserModifierId");

            migrationBuilder.AddColumn<string>(
                name: "RomaId",
                table: "ClientsPositions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RomaId",
                table: "ClientsPositions");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "UserModifierId",
                table: "ClientsPositions",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "DateLastModify",
                table: "ClientsPositions",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UserModifierId",
                table: "Clients",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "DateLastModify",
                table: "Clients",
                newName: "ModifiedDate");

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_ClientId",
                table: "Location",
                column: "ClientId");
        }
    }
}

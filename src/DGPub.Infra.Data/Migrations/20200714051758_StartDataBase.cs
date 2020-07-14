using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DGPub.Infra.Data.Migrations
{
    public partial class StartDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tab",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(nullable: false),
                    Open = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemTab",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    TabId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTab", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTab_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTab_Tab_TabId",
                        column: x => x.TabId,
                        principalTable: "Tab",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("e53890e8-3c5b-4343-987d-ff9ebb63edce"), "Cerveja", 5m },
                    { new Guid("09229e4c-5d04-410c-a0b1-eb064b3aed1b"), "Conhaque", 20m },
                    { new Guid("fd1a1612-50b2-44e8-8563-b4e51b27ff73"), "Suco", 50m },
                    { new Guid("f07ee6ea-2b64-4d41-b6e7-bb5a50d87329"), "Água", 70m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemTab_ItemId",
                table: "ItemTab",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTab_TabId",
                table: "ItemTab",
                column: "TabId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemTab");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Tab");
        }
    }
}

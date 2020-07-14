using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DGPub.Infra.Data.Migrations
{
    public partial class StartSqlDatabase : Migration
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
                    { new Guid("61e2d6a1-f060-4293-978b-28ffc05842e1"), "Cerveja", 5m },
                    { new Guid("02e617c3-6675-447c-96d1-a41bfefd426d"), "Conhaque", 20m },
                    { new Guid("38184383-6e55-4464-9010-4242e3777a2c"), "Suco", 50m },
                    { new Guid("5db8dfb6-baaa-460f-8c02-5249a47abd1f"), "Água", 70m }
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

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DGPub.Infra.Data.Migrations
{
    public partial class StartDatabase : Migration
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
                    CustomerName = table.Column<string>(nullable: false)
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
                values: new object[] { new Guid("208be985-f429-48e5-8ea9-ec3966c87430"), "Cerveja", 5m });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("5ae2d0db-1ef3-40dc-ab9a-8b9ff9130565"), "Conhaque", 20m });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("8dbf9f09-f772-449f-83c6-a869517e9c79"), "Suco", 50m });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("7709e4e6-1c49-4e62-a266-383c5f1a74ff"), "Água", 70m });

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

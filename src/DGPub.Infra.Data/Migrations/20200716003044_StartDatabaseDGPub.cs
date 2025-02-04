﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DGPub.Infra.Data.Migrations
{
    public partial class StartDatabaseDGPub : Migration
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
                    Discount = table.Column<decimal>(nullable: false),
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
                    { new Guid("67ff90d7-c765-46a2-8511-fe49872c2bf4"), "Cerveja", 5m },
                    { new Guid("ea18353a-b01e-49a9-bac7-57434555b13f"), "Conhaque", 20m },
                    { new Guid("f8e898dc-bb78-46f5-b9d3-ea2bd4f3291f"), "Suco", 50m },
                    { new Guid("1aa699c2-9235-46c8-8f5d-eb463338849d"), "Água", 70m }
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

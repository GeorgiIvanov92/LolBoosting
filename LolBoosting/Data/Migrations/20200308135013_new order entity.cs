using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LolBoosting.Data.Migrations
{
    public partial class neworderentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "AspNetUsers",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Winrate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountUsername = table.Column<string>(nullable: true),
                    AccountPassword = table.Column<string>(nullable: true),
                    OrderType = table.Column<int>(nullable: false),
                    BoosterEarningsPerWin = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    BoosterEarningsPerGame = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    BoosterEarningsPer20LP = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    StartingTier = table.Column<int>(nullable: false),
                    StartingDivision = table.Column<int>(nullable: false),
                    StartingLP = table.Column<int>(nullable: false),
                    CurrentTier = table.Column<int>(nullable: false),
                    CurrentDivision = table.Column<int>(nullable: false),
                    CurrentLP = table.Column<int>(nullable: false),
                    DesiredTier = table.Column<int>(nullable: false),
                    DesiredDivision = table.Column<int>(nullable: false),
                    DesiredLP = table.Column<int>(nullable: false),
                    PurchasedGames = table.Column<int>(nullable: false),
                    RemainingGames = table.Column<int>(nullable: false),
                    BoosterId = table.Column<string>(nullable: false),
                    ClientId = table.Column<string>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_BoosterId",
                        column: x => x.BoosterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_BoosterId",
                table: "Order",
                column: "BoosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Winrate",
                table: "AspNetUsers");
        }
    }
}

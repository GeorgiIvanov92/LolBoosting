using Microsoft.EntityFrameworkCore.Migrations;

namespace LoLBoosting.Data.Migrations
{
    public partial class AddedTierRateentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TierRates",
                columns: table => new
                {
                    TierRateId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    TierName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TierRates", x => x.TierRateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TierRates");
        }
    }
}

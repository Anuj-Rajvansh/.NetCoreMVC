using Microsoft.EntityFrameworkCore.Migrations;

namespace CSV_75WAY.Migrations
{
    public partial class CSV_75WAYModelsDbContexts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    epl_identifier = table.Column<string>(nullable: false),
                    site_name = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    province = table.Column<string>(nullable: true),
                    building_type = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.epl_identifier);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}

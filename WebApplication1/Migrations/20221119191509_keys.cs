using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTeam",
                columns: table => new
                {
                    team_Id = table.Column<int>(type: "int", nullable: false)
                      ,
                    team_abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team_conference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team_division = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team_full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTeam", x => x.team_Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                       ,
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    height_feet = table.Column<int>(type: "int", nullable: true),
                    height_inches = table.Column<int>(type: "int", nullable: false),
                    weight_pounds = table.Column<int>(type: "int", nullable: true),
                    AppTeam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.id);
                    table.ForeignKey(
                        name: "FK_AppUsers_AppTeam_AppTeam",
                        column: x => x.AppTeam,
                        principalTable: "AppTeam",
                        principalColumn: "team_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AppTeam",
                table: "AppUsers",
                column: "AppTeam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "AppTeam");
        }
    }
}

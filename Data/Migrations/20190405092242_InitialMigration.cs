using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HuntedList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuntedList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HuntingSpot",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuntingSpot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TibiaCharacter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    GuildName = table.Column<string>(nullable: true),
                    World = table.Column<string>(nullable: true),
                    Vocation = table.Column<string>(nullable: true),
                    Residence = table.Column<string>(nullable: true),
                    LatestDeathBy = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    IsOnline = table.Column<bool>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: false),
                    LatestDeath = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TibiaCharacter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HuntedListCharacter",
                columns: table => new
                {
                    HuntedListId = table.Column<int>(nullable: false),
                    TibiaCharacterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuntedListCharacter", x => new { x.HuntedListId, x.TibiaCharacterId });
                    table.ForeignKey(
                        name: "FK_HuntedListCharacter_HuntedList_HuntedListId",
                        column: x => x.HuntedListId,
                        principalTable: "HuntedList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HuntedListCharacter_TibiaCharacter_TibiaCharacterId",
                        column: x => x.TibiaCharacterId,
                        principalTable: "TibiaCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HuntingSpotCharacter",
                columns: table => new
                {
                    HuntingSpotId = table.Column<int>(nullable: false),
                    TibiaCharacterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuntingSpotCharacter", x => new { x.HuntingSpotId, x.TibiaCharacterId });
                    table.ForeignKey(
                        name: "FK_HuntingSpotCharacter_HuntingSpot_HuntingSpotId",
                        column: x => x.HuntingSpotId,
                        principalTable: "HuntingSpot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HuntingSpotCharacter_TibiaCharacter_TibiaCharacterId",
                        column: x => x.TibiaCharacterId,
                        principalTable: "TibiaCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HuntedListCharacter_TibiaCharacterId",
                table: "HuntedListCharacter",
                column: "TibiaCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingSpotCharacter_TibiaCharacterId",
                table: "HuntingSpotCharacter",
                column: "TibiaCharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HuntedListCharacter");

            migrationBuilder.DropTable(
                name: "HuntingSpotCharacter");

            migrationBuilder.DropTable(
                name: "HuntedList");

            migrationBuilder.DropTable(
                name: "HuntingSpot");

            migrationBuilder.DropTable(
                name: "TibiaCharacter");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synword.Infrastructure.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Picture = table.Column<string>(type: "TEXT", nullable: true),
                    Locale = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsageData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlagiarismCheckCount = table.Column<int>(type: "INTEGER", nullable: false),
                    RephraseCount = table.Column<int>(type: "INTEGER", nullable: false),
                    LastVisitDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ExternalSignIn_Type = table.Column<string>(type: "TEXT", nullable: true),
                    ExternalSignIn_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Ip = table.Column<string>(type: "TEXT", nullable: false),
                    Roles = table.Column<string>(type: "TEXT", nullable: false),
                    Coins = table.Column<int>(type: "INTEGER", nullable: false),
                    UsageDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    MetadataId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "Metadata",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_UsageData_UsageDataId",
                        column: x => x.UsageDataId,
                        principalTable: "UsageData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    PurchaseToken = table.Column<string>(type: "TEXT", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlagiarismCheckHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Error = table.Column<string>(type: "TEXT", nullable: true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Percent = table.Column<float>(type: "REAL", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlagiarismCheckHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlagiarismCheckHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RephraseHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceText = table.Column<string>(type: "TEXT", nullable: false),
                    RephrasedText = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RephraseHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RephraseHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchedUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Percent = table.Column<float>(type: "REAL", nullable: false),
                    PlagiarismCheckHistoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchedUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchedUrls_PlagiarismCheckHistories_PlagiarismCheckHistoryId",
                        column: x => x.PlagiarismCheckHistoryId,
                        principalTable: "PlagiarismCheckHistories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Synonyms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SynonymWordStartIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    SynonymWordEndIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    Synonyms = table.Column<string>(type: "TEXT", nullable: false),
                    RephraseHistoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synonyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Synonyms_RephraseHistories_RephraseHistoryId",
                        column: x => x.RephraseHistoryId,
                        principalTable: "RephraseHistories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HighlightRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    EndIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    MatchedUrlId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlagiarismCheckHistoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighlightRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HighlightRanges_MatchedUrls_MatchedUrlId",
                        column: x => x.MatchedUrlId,
                        principalTable: "MatchedUrls",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HighlightRanges_PlagiarismCheckHistories_PlagiarismCheckHistoryId",
                        column: x => x.PlagiarismCheckHistoryId,
                        principalTable: "PlagiarismCheckHistories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HighlightRanges_MatchedUrlId",
                table: "HighlightRanges",
                column: "MatchedUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_HighlightRanges_PlagiarismCheckHistoryId",
                table: "HighlightRanges",
                column: "PlagiarismCheckHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchedUrls_PlagiarismCheckHistoryId",
                table: "MatchedUrls",
                column: "PlagiarismCheckHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlagiarismCheckHistories_UserId",
                table: "PlagiarismCheckHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RephraseHistories_UserId",
                table: "RephraseHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Synonyms_RephraseHistoryId",
                table: "Synonyms",
                column: "RephraseHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExternalSignIn_Id",
                table: "Users",
                column: "ExternalSignIn_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MetadataId",
                table: "Users",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UsageDataId",
                table: "Users",
                column: "UsageDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HighlightRanges");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Synonyms");

            migrationBuilder.DropTable(
                name: "MatchedUrls");

            migrationBuilder.DropTable(
                name: "RephraseHistories");

            migrationBuilder.DropTable(
                name: "PlagiarismCheckHistories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Metadata");

            migrationBuilder.DropTable(
                name: "UsageData");
        }
    }
}

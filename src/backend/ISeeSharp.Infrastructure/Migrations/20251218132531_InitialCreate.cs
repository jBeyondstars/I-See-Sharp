using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISeeSharp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Instructions = table.Column<string>(type: "text", nullable: false),
                    ObjectivesJson = table.Column<string>(type: "jsonb", nullable: true),
                    HintsJson = table.Column<string>(type: "jsonb", nullable: true),
                    Difficulty = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    EstimatedMinutes = table.Column<int>(type: "integer", nullable: false),
                    TargetWpm = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsPremium = table.Column<bool>(type: "boolean", nullable: false),
                    TotalAttempts = table.Column<int>(type: "integer", nullable: false),
                    TotalCompletions = table.Column<int>(type: "integer", nullable: false),
                    AverageAccuracy = table.Column<double>(type: "double precision", nullable: false),
                    AverageWpm = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    TotalScore = table.Column<int>(type: "integer", nullable: false),
                    TotalXp = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    SessionsCompleted = table.Column<int>(type: "integer", nullable: false),
                    TotalLinesWritten = table.Column<long>(type: "bigint", nullable: false),
                    TotalCharactersTyped = table.Column<long>(type: "bigint", nullable: false),
                    TotalTimeSeconds = table.Column<long>(type: "bigint", nullable: false),
                    AverageWpm = table.Column<double>(type: "double precision", nullable: false),
                    AverageAccuracy = table.Column<double>(type: "double precision", nullable: false),
                    CurrentStreak = table.Column<int>(type: "integer", nullable: false),
                    LongestStreak = table.Column<int>(type: "integer", nullable: false),
                    LastActivityDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FreezeTokens = table.Column<int>(type: "integer", nullable: false),
                    PreferencesJson = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Language = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TargetContent = table.Column<string>(type: "text", nullable: false),
                    EditableRegionsJson = table.Column<string>(type: "jsonb", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    TotalLines = table.Column<int>(type: "integer", nullable: false),
                    TotalCharacters = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionFiles_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSessionResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    Accuracy = table.Column<double>(type: "double precision", nullable: false),
                    Wpm = table.Column<int>(type: "integer", nullable: false),
                    Cpm = table.Column<int>(type: "integer", nullable: false),
                    ErrorCount = table.Column<int>(type: "integer", nullable: false),
                    CompletionTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    LinesTyped = table.Column<int>(type: "integer", nullable: false),
                    CharactersTyped = table.Column<int>(type: "integer", nullable: false),
                    FilesCompleted = table.Column<int>(type: "integer", nullable: false),
                    TotalFiles = table.Column<int>(type: "integer", nullable: false),
                    ScoreEarned = table.Column<int>(type: "integer", nullable: false),
                    XpEarned = table.Column<int>(type: "integer", nullable: false),
                    AttemptNumber = table.Column<int>(type: "integer", nullable: false),
                    FileResultsJson = table.Column<string>(type: "jsonb", nullable: true),
                    ErrorPositionsJson = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessionResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSessionResults_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSessionResults_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionFiles_SessionId",
                table: "SessionFiles",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionFiles_SessionId_SortOrder",
                table: "SessionFiles",
                columns: new[] { "SessionId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Category",
                table: "Sessions",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Difficulty",
                table: "Sessions",
                column: "Difficulty");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_IsActive",
                table: "Sessions",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_IsPremium",
                table: "Sessions",
                column: "IsPremium");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Slug",
                table: "Sessions",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Level",
                table: "Users",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TotalLinesWritten",
                table: "Users",
                column: "TotalLinesWritten");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TotalScore",
                table: "Users",
                column: "TotalScore");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TotalXp",
                table: "Users",
                column: "TotalXp");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionResults_SessionId",
                table: "UserSessionResults",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionResults_UserId_CreatedAt",
                table: "UserSessionResults",
                columns: new[] { "UserId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionResults_UserId_SessionId",
                table: "UserSessionResults",
                columns: new[] { "UserId", "SessionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionFiles");

            migrationBuilder.DropTable(
                name: "UserSessionResults");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

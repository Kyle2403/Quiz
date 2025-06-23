using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class flat4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionText = table.Column<string>(type: "TEXT", nullable: false),
                    AnswerText = table.Column<string>(type: "TEXT", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizEntries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizEntries");
        }
    }
}

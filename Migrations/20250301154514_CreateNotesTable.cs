using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProject.Migrations
{
    /// <inheritdoc />
    public partial class CreateNotesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                schema: "Dbo",
                columns: table => new
                {
                    noteId = table.Column<Guid>(nullable: false), // Primary Key
                    id = table.Column<Guid>(nullable: false),     // NOT NULL
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    done = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.noteId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Notes", schema: "Dbo");
        }
    }
}
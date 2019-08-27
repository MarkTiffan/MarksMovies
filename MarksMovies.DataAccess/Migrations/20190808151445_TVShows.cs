using Microsoft.EntityFrameworkCore.Migrations;

namespace MarksMovies.Migrations
{
    public partial class TVShows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieOrTVShow",
                table: "Movie",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Season",
                table: "Movie",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieOrTVShow",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Movie");
        }
    }
}

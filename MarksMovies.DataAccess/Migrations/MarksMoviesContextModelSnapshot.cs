﻿// <auto-generated />
using MarksMovies.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MarksMovies.Migrations
{
    [DbContext(typeof(MarksMoviesContext))]
    partial class MarksMoviesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MarksMovies.Models.Genre", b =>
                {
                    b.Property<int>("GenreID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieID");

                    b.Property<int>("genre");

                    b.HasKey("GenreID");

                    b.HasIndex("MovieID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("MarksMovies.Models.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasMaxLength(500);

                    b.Property<string>("IMDB_ID")
                        .HasMaxLength(9);

                    b.Property<int>("MediaType");

                    b.Property<int>("MovieOrTVShow");

                    b.Property<int>("Rank");

                    b.Property<int>("Rating");

                    b.Property<int>("Season");

                    b.Property<int>("TMDB_ID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Year");

                    b.HasKey("ID");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("MarksMovies.Models.Genre", b =>
                {
                    b.HasOne("MarksMovies.Models.Movie", "Movie")
                        .WithMany("Genres")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

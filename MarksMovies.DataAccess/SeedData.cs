using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarksMovies.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MarksMoviesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MarksMoviesContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }
                try
                {

                    //string[] lines = System.IO.File.ReadAllLines(@"./Assets/MovieList.txt");

                    //foreach (string line in lines)
                    //{
                    //    context.Movie.Add(new Movie {
                    //        Title = line
                    //    });
                    //}

                    string json = System.IO.File.ReadAllText(@"./Assets/movies.json");
                    IList<Movie> Movies = JsonConvert.DeserializeObject<IList<Movie>>(json);

                    foreach (var movie in Movies)
                    {
                        context.Movie.Add(movie);
                    }

                    context.SaveChanges();
                    
                }
                catch(Exception e)
                {
                    Console.WriteLine("An Error Occurred: " + e);
                }
                finally
                {
                    context.Database.CloseConnection();
                }
            }
        }
    }
}
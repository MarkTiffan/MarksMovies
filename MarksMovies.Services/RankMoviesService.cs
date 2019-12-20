using MarksMovies.DataAccess;
using MarksMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarksMovies.Services
{
    public class RankMoviesService
    {
        private readonly IMovieDBAccess _dbAccess;

        public RankMoviesService(IMovieDBAccess DbAccess)
        {
            _dbAccess = DbAccess;
        }


        public IList<Movie> GetRankedMovies()
        {
            return _dbAccess.GetRankedMovies();
        }


        public async Task<int> UpdateRanksAsync(string itemIds)
        {
            if (!string.IsNullOrEmpty(itemIds))
            {
                int count = 1;
                List<int> itemIdList = new List<int>();
                itemIdList = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                foreach (var itemId in itemIdList)
                {
                    try
                    {
                        Movie movie = await _dbAccess.GetMovieAsync(itemId);
                        if (movie != null)
                        {
                            movie.Rank = count;
                            _dbAccess.Update(movie);
                        }
                        else
                            throw new Exception($"Movie: {itemId} came back null.");
                        count++;
                    }
                    catch(Exception e) {
                        throw new Exception(e.Message);
                    }
                }
                await _dbAccess.SaveChangesAsync();
            }
            return 0;
        }
    }
}

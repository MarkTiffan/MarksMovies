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

        public RankMoviesService(IMovieDBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }


        public IList<Movie> OnGet()
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
                    Movie movie = await _dbAccess.GetMovieAsync(itemId);
                    movie.Rank = count;
                    _dbAccess.Update(movie);
                    await _dbAccess.SaveChangesAsync();
                    count++;
                }
            }
            return 0;
        }
    }
}

using MarksMovies.DataAccess;
using MarksMovies.Models;
using System.Threading.Tasks;

namespace MarksMovies.Services
{
    public class DeleteService
    {
        private readonly IMovieDBAccess _dbAccess;

        public DeleteService(IMovieDBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public const int DELETE_FAIL = 0;
        public const int DELETE_OK = 1;

        public async Task<Movie> GetMovieAsync(int? id)
        {
            return await _dbAccess.GetMovieAsync(id);
        }


        public async Task<int> DeleteMovieAsync(int? id)
        {
            return await _dbAccess.DeleteMovieAsync(id);
        }
    }
}

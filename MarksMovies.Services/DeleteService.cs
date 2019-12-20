using MarksMovies.DataAccess;
using System.Threading.Tasks;

namespace MarksMovies.Services
{
    public class DeleteService
    {
        private readonly IMovieDBAccess _dbAccess;
        public const int DELETE_FAIL = 0;
        public const int DELETE_SUCCESS = 1;
        public DeleteService(IMovieDBAccess DbAccess)
        {
            _dbAccess = DbAccess;
        }


        public async Task<int> DeleteMovieAsync(int? ID)
        {
            return await _dbAccess.DeleteMovieAsync(ID);
        }
    }
}

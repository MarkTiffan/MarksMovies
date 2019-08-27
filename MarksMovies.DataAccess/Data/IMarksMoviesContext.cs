using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace MarksMovies.Models
{
    public interface IMarksMoviesContext
    {
        DbSet<Movie> Movie { get; set; }
        EntityEntry Entry(object entity);

        EntityEntry Remove(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry Update(object entity);

    }
}

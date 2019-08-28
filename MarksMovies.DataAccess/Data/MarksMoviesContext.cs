using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MarksMovies.Models;

namespace MarksMovies.DataAccess
{
    public class MarksMoviesContext : DbContext, IMarksMoviesContext
    {
        public MarksMoviesContext (DbContextOptions<MarksMoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movie { get; set; }

        public override EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public override EntityEntry Remove(object entity)
        {
            return base.Remove(entity);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public override EntityEntry Update(object entity)
        {
            return base.Update(entity);
        }

    }
}

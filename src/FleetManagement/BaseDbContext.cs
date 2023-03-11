using Microsoft.EntityFrameworkCore;

namespace TheSeam.DataAccess
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext() : base()
        {

        }

        public BaseDbContext(DbContextOptions options) : base(options)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken, bool logChanges)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}

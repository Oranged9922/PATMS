using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class PatmsDbContext : DbContext
    {
        public string DbPath { get; }

        public PatmsDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "PatmsDb.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
        public DbSet<User> Users { get; set; } = null!;
    }
}

using Microsoft.EntityFrameworkCore;
using MvcWebsite.Models;

namespace MvcWebsite.Data
{
    public class MvcWebsiteContext : DbContext
    {
        public MvcWebsiteContext(DbContextOptions<MvcWebsiteContext> options)
            : base(options)
        {
        }

        public DbSet<Board> Board { get; set; }
        public DbSet<Stiky> Stiky { get; set; }
    }
}
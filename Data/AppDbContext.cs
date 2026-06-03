using Microsoft.EntityFrameworkCore;
using TravelPlanner.Models;

namespace TravelPlanner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<SuggestedRoute> SuggestedRoutes { get; set; }
        public DbSet<VisaFreeCountry> VisaFreeCountries { get; set; }
        public DbSet<TravelTip> TravelTips { get; set; }
    }
}
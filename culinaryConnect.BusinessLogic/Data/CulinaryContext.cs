using System.Data.Entity;
using culinaryConnect.BusinessLogic.Models.User;
using culinaryConnect.BusinessLogic.Seed;

namespace culinaryConnect.BusinessLogic.Data
{
    public class CulinaryContext : DbContext
    {
        public CulinaryContext() : base("name=CulinaryContext")
        {
            Database.SetInitializer(new DbInitializer()); // Register the initializer
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

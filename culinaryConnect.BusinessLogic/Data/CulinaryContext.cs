using System.Data.Entity;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.BusinessLogic.Models.AdminDB;

namespace culinaryConnect.BusinessLogic.Data
{
    public class CulinaryContext : DbContext
    {
        public CulinaryContext() : base("name=CulinaryContext")
        {
        }

        public DbSet<UserDB> Users { get; set; }
        public DbSet<AdminDB> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

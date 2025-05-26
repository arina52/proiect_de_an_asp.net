using System.Data.Entity;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.BusinessLogic.Models;
namespace culinaryConnect.BusinessLogic.Data
{
    public class CulinaryContext : DbContext
    {
        public CulinaryContext() : base("name=CulinaryContext")
        {
        }

        public DbSet<UserDB> Users { get; set; }
        public DbSet<RecipeDB> Recipes { get; set; }
        public DbSet<CategoryDB> Categories { get; set; }

        public DbSet<RecipeAboutDB> RecipesAboutDb { get; set; }
        public DbSet<FavoriteRecipeDB> FavoriteRecipes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

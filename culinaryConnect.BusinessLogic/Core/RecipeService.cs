using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.BusinessLogic.Core
{
    public class RecipeService : IRecipeService
    {
        private readonly CulinaryContext _context;

        public RecipeService(CulinaryContext context)
        {
            _context = context;
        }
        public List<RecipeDetails> GetAllRecipes()
        {
            var recipes = _context.Recipes.Include("AboutRecipe").ToList();

            return recipes.Select(r => new RecipeDetails
            {
                Id = r.Id,
                Title = r.Title,
                CategoryID = r.CategoryID,
                ImagePath = r.ImagePath,
                AboutRecipe = r.AboutRecipe != null ? new RecipeAbout
                {
                    Description = r.AboutRecipe.Description,
                    CookingTime = r.AboutRecipe.CookingTime,
                    Instructions = r.AboutRecipe.Instructions,
                    Ingredients = r.AboutRecipe.Ingredients
                } : null
            }).ToList();
        }

        public RecipeDetails GetRecipeById(int Id)
        {
            var recipeDB = _context.Recipes.Include("AboutRecipe").FirstOrDefault(r => r.Id == Id);

            if (recipeDB == null) return null;

            return new RecipeDetails
            {
                Id = recipeDB.Id,
                Title = recipeDB.Title,
                ImagePath = recipeDB.ImagePath,
                CategoryID = recipeDB.CategoryID,
                AboutRecipe = recipeDB.AboutRecipe != null ? new RecipeAbout
                {
                    Description = recipeDB.AboutRecipe.Description,
                    CookingTime = recipeDB.AboutRecipe.CookingTime,
                    Instructions = recipeDB.AboutRecipe.Instructions,
                    Ingredients = recipeDB.AboutRecipe.Ingredients
                } : null
            };
        }

    }
}

using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities;
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
            var authorIds = recipes.Select(r => r.AuthorID).Distinct().ToList();
            var authors = _context.Users
                .Where(u => authorIds.Contains(u.Id))
                .Select(u => new AuthorModel { ID = u.Id, Name = u.UserName })
                .ToList();
            return recipes.Select(r => new RecipeDetails
            {
                Id = r.Id,
                Title = r.Title,
                CategoryID = r.CategoryID,
                ImagePath = r.ImagePath,
                CreatedDate=r.CreatedDate.ToShortDateString(),
                Author = authors.FirstOrDefault(a => a.ID == r.AuthorID),
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
            var author = _context.Users.FirstOrDefault(u => u.Id == recipeDB.AuthorID);

            if (recipeDB == null) return null;

            return new RecipeDetails
            {
                Id = recipeDB.Id,
                Title = recipeDB.Title,
                ImagePath = recipeDB.ImagePath,
                CategoryID = recipeDB.CategoryID,
                CreatedDate = recipeDB.CreatedDate.ToShortDateString(),
                Author = new AuthorModel { ID=author.Id,
                Name = author.UserName},
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

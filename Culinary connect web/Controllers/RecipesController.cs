using Culinary_connect_web.DTO;
using culinaryConnect.BusinessLogic.Core;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace culinaryConnect.Web.Controllers
{
    public class RecipesController : Controller
    {

        private readonly IRecipeService _recipeService;
        private readonly CulinaryContext _context = new CulinaryContext();
        public RecipesController()
        {
            _recipeService = new RecipeService();
        }

        public ActionResult Index()
        {
            var recipes = _context.Recipes
            .Include(r => r.AboutRecipe)
            .ToList();
            var recipeList = recipes.Select(r => new RecipeDetails
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


            var recipesDTO = new RecipesListDTO
            {
                RecipeList = recipeList
            };

            return View(recipesDTO);
        }
    }
}
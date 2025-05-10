using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Culinary_connect_web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly CulinaryContext _context = new CulinaryContext();

        // GET: Recipe
        public ActionResult Index(int id)
        {
            var recipeDB = _context.Recipes.Include("AboutRecipe").FirstOrDefault(r => r.Id == id);


            if (recipeDB == null) {
                return HttpNotFound();
            }

            var recipePage = new RecipeDetails { 
                Id = recipeDB.Id,
                Title = recipeDB.Title,
                ImagePath = recipeDB.ImagePath,
                AboutRecipe = new RecipeAbout
                {
                    Description = recipeDB.AboutRecipe.Description,
                    CookingTime = recipeDB.AboutRecipe.CookingTime,
                    Instructions = recipeDB.AboutRecipe.Instructions,
                    Ingredients = recipeDB.AboutRecipe.Ingredients
                },
                CategoryID = recipeDB.CategoryID
            };

            return View(recipePage);
        }
    }
}
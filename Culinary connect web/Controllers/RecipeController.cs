using culinaryConnect.BusinessLogic.Data;
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
            var recipeDB = _context.Recipes.FirstOrDefault(x => x.Id == id);


            if (recipeDB == null) {
                return HttpNotFound();
            }

            var recipePage = new RecipeDetails { 
                Id = recipeDB.Id,
                Title = recipeDB.Title,
                Description = recipeDB.Description,
                CategoryID = recipeDB.CategoryID
            };

            return View(recipePage);
        }
    }
}
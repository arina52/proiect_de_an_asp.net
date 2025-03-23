using culinaryConnect.BusinessLogic.Core;
using culinaryConnect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace culinaryConnect.Web.Controllers
{
    public class RecipesController : Controller
    {

        private readonly IRecipeService _recipeService;
        public RecipesController()
        {
            _recipeService = new RecipeService(); 
        }

        public ActionResult Index()
        {
            var recipes = _recipeService.GetAllRecipes();
            return View(recipes);
        }
    }
}
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
            _recipeService = new RecipeService(_context);
        }

        public ActionResult Index()
        {
            var recipeList = _recipeService.GetAllRecipes();
            var recipesDTO = new RecipesListDTO
            {
                RecipeList = recipeList
            };

            return View(recipesDTO);
        }
    }
}
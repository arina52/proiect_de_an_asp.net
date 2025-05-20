using culinaryConnect.BusinessLogic.Core;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using culinaryConnect.BusinessLogic.Interfaces;
namespace Culinary_connect_web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        public RecipeController()
        {
            _recipeService = new RecipeService();
        }

        // GET: Recipe
        public ActionResult Index(int id)
        {
            var recipePage = _recipeService.GetRecipeById(id);
            if (recipePage == null)
            {
                return HttpNotFound();
            }
            return View(recipePage);
        }
    }
}
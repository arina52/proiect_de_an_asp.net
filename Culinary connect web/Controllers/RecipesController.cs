using Culinary_connect_web.DTO;
using culinaryConnect.BusinessLogic.Services;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using culinaryConnect.BusinessLogic;
using culinaryConnect.Domain.Entities;

namespace culinaryConnect.Web.Controllers
{
    public class RecipesController : Controller
    {

        private readonly IRecipeService _recipeService;
        private readonly IAcountService _accountService;

        public RecipesController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _recipeService = new RecipeServiceBL();
            _accountService = new AccountServiceBL();
        }

        public ActionResult Index()
        {
            var recipeList = _recipeService.GetAllActiveRecipes();
            var categoryList = _accountService.GetCategoryList().Select(c => new CategoryUser
            {
                Id = c.Id,
                Name = c.Title,
            }
            ).ToList();
            var recipesDTO = new RecipesListDTO
            {
                RecipeList = recipeList,
                Categories = categoryList
            };

            return View(recipesDTO);
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Culinary_connect_web.DTO;
using culinaryConnect.BusinessLogic.Services;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.Domain.Entities;

namespace culinaryConnect.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IAcountService _accountService;

        public HomeController()
        {
            _recipeService = new RecipeServiceBL();
            _accountService = new AccountServiceBL();
        }
        // GET: Home
        public ActionResult Index()
        {
            var recipeList = _recipeService.GetAllActiveRecipes();
            var categoryList = _accountService.GetCategoryList().Select(c => new CategoryUser
            {
                Id = c.Id,
                Name = c.Title,
                Recipes = recipeList.Where(r => r.CategoryID == c.Id).ToList()
            }
            ).ToList();
            var recipesDTO = new RecipesListDTO
            {
                RecipeList = recipeList,
                Categories = categoryList
            };
            return View(recipesDTO);
        }
        public ActionResult AboutMe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubscribeNews(string email)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("index", "login");

            _accountService.SubscribeUserToNewsletter(email);

            return View("index"); 
        }

    }
}
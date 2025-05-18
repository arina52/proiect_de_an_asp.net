using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models.UserDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Culinary_connect_web.DTO;
using culinaryConnect.BusinessLogic.Core;
using culinaryConnect.BusinessLogic.Interfaces;

namespace culinaryConnect.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CulinaryContext _context = new CulinaryContext();
        private readonly IRecipeService _recipeService;

        public HomeController()
        {
            _recipeService = new RecipeService(_context);
        }
        // GET: Home
        public ActionResult Index()
        {
            var recipeList = _recipeService.GetAllRecipes();
            var recipesDTO = new RecipesListDTO
            {
                RecipeList = recipeList
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
            {
                return RedirectToAction("index", "login");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email);

            if (user == null)
            {

                var newUser = new UserDB();
                newUser.UserEmail = email;
                newUser.SubscribedToNews = true;
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return View();
            }

            user.SubscribedToNews = true;
            _context.SaveChanges();

            return View("index");
        }
    }
}
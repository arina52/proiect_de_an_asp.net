using System.Web.Mvc;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.Domain.Entities.Recipe;
using culinaryConnect.BusinessLogic.Models;
using System.Linq;
using System.Web.Helpers;
using System.Web;
using System.IO;
using System;
using culinaryConnect.Domain.Entities;
using culinaryConnect.BusinessLogic.Services;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.BusinessLogic;

namespace Culinary_connect_web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly IAcountService _accountService;
        private readonly IRecipeService _recipeService;
        public AccountController()
        {
            var bl = new BusinessLogic();
            _accountService = bl.GetAccountService();
            _recipeService = bl.GetRecipeService();
        }
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("index", "login");

            int userId = (int)Session["UserID"];
            var user = _accountService.GetUserByID(userId);
            return View(user);

        }

        public ActionResult Favorites()
        {
            int userId = (int)Session["UserID"];
            var favorites = _recipeService.GetUserFavorites(userId);
            return View(favorites);
        }


        public ActionResult Recipes() {
            if (Session["UserID"] == null)
                return RedirectToAction("index", "login");
            IRecipeService _recipeService = new RecipeServiceBL();
            var model = new RecipesPageModel();
            model.RecipeList = _recipeService.GetAllRecipes();

            return View(model);
        }

        public ActionResult Recipe()
        {

            if (Session["UserID"] == null)
                return RedirectToAction("index", "login");
            var categories = _accountService.GetCategoryList();
            var model = new RecipesPageModel()
            {
                Categories = categories.Select(c => new CategoryUser
                {
                    Id = c.Id,
                    Name = c.Title
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddRecipe(RecipesPageModel model, HttpPostedFileBase RecipeImage)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("index", "login");

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "The model is incorect";
                RedirectToAction("Index", "Home");
            }

            var recipe = model.RecipeForm;

            if (string.IsNullOrWhiteSpace(recipe.Title) || string.IsNullOrWhiteSpace(recipe.AboutRecipe.Description) ){
                ViewBag.ErrorMessage = "Title and Description are required";
                return View("recipes", model);
            }

            string imageFileName = null;

            if (RecipeImage != null && RecipeImage.ContentLength > 0)
            {
                imageFileName = Path.GetFileName(RecipeImage.FileName);
                var path = Server.MapPath("~/Content/Images/recipe/" + imageFileName);
                RecipeImage.SaveAs(path);
            }

            int userId = (int)Session["UserID"];
            _accountService.AddRecipe(model, userId, imageFileName);

            TempData["Success"] = "Recipe created successfully!";
            return RedirectToAction("recipes", "account");
        }

        public ActionResult Delete(int id)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("index", "login");

            try
            {
                int userId = (int)Session["UserID"];
                _accountService.DeleteRecipe(id, userId);
                TempData["Success"] = "Recipe deleted successfully!";
            }
            catch (UnauthorizedAccessException)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Recipes");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var recipe = _recipeService.GetRecipeEntityById(id);
            if (recipe == null || recipe.AuthorID != (int)Session["UserID"])
                return HttpNotFound();

            var recipeDetails = new RecipeDetails
            {
                Id = recipe.Id,
                Title = recipe.Title,
                ImagePath = recipe.ImagePath,
                CategoryID = recipe.CategoryID,
                AboutRecipe = new RecipeAbout
                {
                    Description = recipe.AboutRecipe.Description,
                    Instructions = recipe.AboutRecipe.Instructions,
                    Ingredients = recipe.AboutRecipe.Ingredients,
                    CookingTime = recipe.AboutRecipe.CookingTime
                }
            };
            var categories = _accountService.GetCategoryList();
            var model = new RecipesPageModel
            {
                RecipeForm = recipeDetails,
                Categories = categories.Select(c => new CategoryUser
                {
                    Id = c.Id,
                    Name = c.Title
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RecipesPageModel model, HttpPostedFileBase RecipeImage)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("index", "login");

            if (!ModelState.IsValid)
                return View(model);

            string imageFileName = null;

            if (RecipeImage != null && RecipeImage.ContentLength > 0)
            {
                imageFileName = Path.GetFileName(RecipeImage.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/recipe/"), imageFileName);
                RecipeImage.SaveAs(path);
            }

            try
            {
                int userId = (int)Session["UserID"];
                _accountService.UpdateRecipe(model, userId, imageFileName);

                TempData["Success"] = "Recipe updated successfully!";
                return RedirectToAction("Recipes", "Account");
            }
            catch (UnauthorizedAccessException)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult ToggleFavorite(int recipeId)
        {
            if (Session["UserID"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            int userId = (int)Session["UserID"];
            if (_recipeService.IsFavorite(userId, recipeId))
                _recipeService.RemoveFromFavorites(userId, recipeId);
            else
                _recipeService.AddToFavorites(userId, recipeId);

            return RedirectToAction("Index", "Recipe", new { id = recipeId });
        }

    }
}
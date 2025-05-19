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
using culinaryConnect.BusinessLogic.Core;
using culinaryConnect.BusinessLogic.Interfaces;

namespace Culinary_connect_web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        private readonly CulinaryContext _context = new CulinaryContext();

        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            } else
            {
                return RedirectToAction("index", "login");
            }
        }

        public ActionResult Favorites()
        {
            return View();
        }

        public ActionResult Recipes() {
            if (Session["UserID"] == null)
                return RedirectToAction("index", "login");
            IRecipeService _recipeService = new RecipeService(_context);
            var model = new RecipesPageModel();
            model.RecipeList = _recipeService.GetAllRecipes();

            return View(model);
        }

        public ActionResult Recipe()
        {

            if (Session["UserID"] == null)
                return RedirectToAction("index", "login");
            var categories = _context.Categories.ToList();
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

            var categoryId = -1;
            if(recipe.CategoryID != 0)
            {
                categoryId = recipe.CategoryID;
            }
            var aboutRecipe = new RecipeAboutDB
            {
                Description = recipe.AboutRecipe.Description,
                CookingTime = recipe.AboutRecipe.CookingTime,
                Ingredients = recipe.AboutRecipe.Ingredients,
                Instructions = recipe.AboutRecipe.Instructions
            };
            var newRecipe = new RecipeDB
            {
                Title = recipe.Title,
                AboutRecipe = aboutRecipe,
                CategoryID = categoryId,
                CreatedDate = DateTime.Now,
                AuthorID = (int)Session["UserID"]
            };
            if (RecipeImage != null && RecipeImage.ContentLength > 0)
            {
                var fileName = Path.GetFileName(RecipeImage.FileName);
                var path = Server.MapPath("~/Content/Images/recipe/" + fileName);
                RecipeImage.SaveAs(path);

                newRecipe.ImagePath = fileName;
            }

            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();

            TempData["Success"] = "Recipe created successfully!";
            return RedirectToAction("recipes", "account");
        }

        public ActionResult Delete(int id)
        {
            var recipe = _context.Recipes.Include("AboutRecipe").FirstOrDefault(r => r.Id == id);
            if (recipe == null || recipe.AuthorID != (int)Session["UserID"])
                return HttpNotFound();

            if (recipe.AboutRecipe != null)
                _context.RecipesAboutDb.Remove(recipe.AboutRecipe);

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();

            return RedirectToAction("Recipes");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var recipe = _context.Recipes.Include("AboutRecipe").FirstOrDefault(r => r.Id == id);
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
            var categories = _context.Categories.ToList();
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
            if (!ModelState.IsValid)
                return View(model);

            var recipe = _context.Recipes.Include("AboutRecipe").FirstOrDefault(r => r.Id == model.RecipeForm.Id);
            if (recipe == null || recipe.AuthorID != (int)Session["UserID"])
                return HttpNotFound();

            recipe.Title = model.RecipeForm.Title;
            recipe.CategoryID = model.RecipeForm.CategoryID;
            recipe.AboutRecipe.Description = model.RecipeForm.AboutRecipe.Description;
            recipe.AboutRecipe.Instructions = model.RecipeForm.AboutRecipe.Instructions;
            recipe.AboutRecipe.Ingredients = model.RecipeForm.AboutRecipe.Ingredients;
            recipe.AboutRecipe.CookingTime = model.RecipeForm.AboutRecipe.CookingTime;

            if (RecipeImage != null && RecipeImage.ContentLength > 0)
            {
                var fileName = Path.GetFileName(RecipeImage.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/recipe/"), fileName);
                RecipeImage.SaveAs(path);

                recipe.ImagePath = fileName;
            }

            _context.SaveChanges();

            TempData["Success"] = "Recipe updated successfully!";
            return RedirectToAction("Recipes", "Account");
        }


    }
}
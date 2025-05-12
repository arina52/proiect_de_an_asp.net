using System.Web.Mvc;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.Domain.Entities.Recipe;
using culinaryConnect.BusinessLogic.Models;
using System.Linq;
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
            var model = new RecipesPageModel { 
                RecipeForm = new RecipeDetails(),
                RecipeList = _context.Recipes.Include("AboutRecipe").ToList().Select(r => new RecipeDetails
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImagePath = r.ImagePath,
                    AboutRecipe = new RecipeAbout
                    {
                        Description = r.AboutRecipe.Description,
                        CookingTime = r.AboutRecipe.CookingTime,
                        Instructions = r.AboutRecipe.Instructions,
                        Ingredients = r.AboutRecipe.Ingredients
                    },
                    CategoryID = r.CategoryID
                }).ToList()

            };

            return View(model);
        }

        public ActionResult Recipe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRecipe(RecipesPageModel model)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("index", "login");

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "The model is incorect";
                return View("recipes", model);
            }

            var recipe = model.RecipeForm;

            if (string.IsNullOrWhiteSpace(recipe.Title) || string.IsNullOrWhiteSpace(recipe.AboutRecipe.Description) ){
                ViewBag.ErrorMessage = "Title and Description are required";
                return View("recipes", model);
            }

            var categoryId = -1;
            if(recipe.CategoryID != null)
            {
                categoryId = recipe.CategoryID;
            }

            var newRecipe = new RecipeDB
            {
                Title = recipe.Title,
                AboutRecipe = new RecipeAboutDB
                {
                    Description = recipe.AboutRecipe.Description,
                    CookingTime = recipe.AboutRecipe.CookingTime,
                    Instructions = recipe.AboutRecipe.Instructions,
                    Ingredients = recipe.AboutRecipe.Ingredients
                },
                CategoryID = categoryId,
            };

            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();

            TempData["Success"] = "Recipe created successfully!";
            return RedirectToAction("recipes", "account");
        }
    }
}
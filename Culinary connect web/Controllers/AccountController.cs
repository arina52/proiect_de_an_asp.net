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
                RecipeList = _context.Recipes.ToList().Select(r => new RecipeDetails
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CategoryID = r.CategoryID ?? null
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

            if (string.IsNullOrWhiteSpace(recipe.Title) || string.IsNullOrWhiteSpace(recipe.Description) ){
                ViewBag.ErrorMessage = "Title and Description are required";
                return View("recipes", model);
            }

            var newRecipe = new RecipeDB
            {
                Title = recipe.Title,
                Description = recipe.Description,
                CategoryID = recipe.CategoryID ?? "-1",
            };

            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();

            TempData["Success"] = "Recipe created successfully!";
            return RedirectToAction("recipes", "account");
        }
    }
}
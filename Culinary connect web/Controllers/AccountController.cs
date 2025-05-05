using System.Web.Mvc;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.Domain.Entities.Recipe;
using culinaryConnect.BusinessLogic.Models;
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

        [HttpPost]
        public ActionResult AddRecipe(RecipeDetails model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "The model is incorect";
                return View();
            }

            var newRecipe = new RecipeDB
            {
                Title = model.Title,
                Description = model.Description,
                CategoryID = model.CategoryID,
            };

            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();

            TempData["Success"] = "Recipe created successfully!";
            return RedirectToAction("index", "account");
        }
    }
}
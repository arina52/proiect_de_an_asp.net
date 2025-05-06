using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.Domain.Entities.Admin;
using culinaryConnect.Web.Services.AdminService;
using culinaryConnect.Domain.Entities.Category;
using culinaryConnect.BusinessLogic.Models;

namespace Culinary_connect_web.Controllers
{
    public class AdminController : Controller
    {
        private readonly CulinaryContext _context = new CulinaryContext();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["AdminID"] != null) { 
                var usersListDB = _context.Users.ToList();

                var usersList = usersListDB.Select(u => new culinaryConnect.Domain.Entities.User.User
                {
                    Id = u.Id,
                    Email = u.UserEmail,
                    Name = u.UserName
                }).ToList();

                var adminWrapper = new AdminWraper
                { Users = usersList};


                return View(adminWrapper);
            }
            return RedirectToAction("login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginRequest(Admin model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "There is something wrong with model";
                return View();
            }


            var adminService = new AdminService();
            var admin = adminService.GetByCredentials(model.AdminEmail, model.AdminPassword);

            if (admin != null)
            {
                Session["AdminID"] = admin.Id;
                Session["AdminEmail"] = admin.AdminEmail;

                return RedirectToAction("index");
            }

            ViewBag.ErrorMessage = "Login failed";
            return View("login");
        }

        public ActionResult Category()
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var categories = _context.Categories.ToList().Select(c => new Category
            {
                Id = c.Id,
                Title = c.Title,
                RecipesID = c.Recipies
            }).ToList();

            var model = new CategoryPageModel
            {
                FormCategory = new CategoryForm(),
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryPageModel model)
        {
            if (Session["AdminID"]==null)
            {
                return RedirectToAction("login");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Model not valid";
                return View("category", model);
            }

            var category = model.FormCategory;

            if (string.IsNullOrEmpty(category.Title))
            {
                ViewBag.ErrorMessage = "Title is empty";
                return View("category", model);
            }
            var categoryDB = new CategoryDB{ Title = category.Title };

            _context.Categories.Add(categoryDB);
            _context.SaveChanges();

            model.FormCategory = new CategoryForm();

            TempData["Success"] = "Category created successfully!";
            return RedirectToAction("category", "admin", model);
        }
    }
}
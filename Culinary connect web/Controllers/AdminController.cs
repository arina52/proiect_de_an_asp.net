using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.Domain.Entities.Admin;
using culinaryConnect.Web.Services.AdminService;
using culinaryConnect.Domain.Entities.Category;
using culinaryConnect.BusinessLogic.Models;
using System.Data.Entity.Migrations;
using culinaryConnect.Domain.Entities.User;

namespace Culinary_connect_web.Controllers
{
    public class AdminController : Controller
    {
        private readonly CulinaryContext _context = new CulinaryContext();

        // Pages
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
            if (Session["AdminID"] != null)
            {
                return RedirectToAction("index");
            }
            return View();
        }

        public ActionResult Users(UsersPageModel model)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var users = _context.Users.ToList().Select(u => new User {
                Id = u.Id,
                Name = u.UserName,
                Email = u.UserEmail,
                SubscribedNews = u.SubscribedToNews,
            }).ToList();
            model.Users = users;
            return View(model);
        }

        public ActionResult Recipes()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
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


        // Functions
        [HttpPost]
        public ActionResult Logout() {
            Session["AdminID"] = null;
            Session["AdminEmail"] = null;

            return RedirectToAction("login");
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

        [HttpPost]
        public ActionResult CreateUser(UsersPageModel model)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var userModel = model.UserRegisterModel;
            var existingUser = _context.Users.FirstOrDefault(u => u.UserEmail == userModel.UserEmail);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Such a user already exists";
                model.UserRegisterModel = new UserRegisterModel();
                return View("Users", model);
            }

            var userPasswordHash = Crypto.SHA256(userModel.UserPassword + "tralalero");
            _context.Users.Add(new UserDB
            {
                UserEmail = userModel.UserEmail,
                UserName = userModel.UserName,
                PasswordHash = userPasswordHash,
            });

            _context.SaveChanges();
            model.UserRegisterModel = new UserRegisterModel();
            return RedirectToAction("Users",model);
        }

        [HttpPost]
        public ActionResult EditUser(UsersPageModel model) {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var userModel = model.UserEditModel;

            var user = _context.Users.FirstOrDefault(u => u.Id == userModel.Id);
            if(user == null)
            {
                ViewBag.ErrorMessage = "There is no such user" + userModel.Id;
                model.UserEditModel = new UserEditModel();
                model.Users = _context.Users.ToList().Select(u => new User
                {
                    Id = u.Id,
                    Email = u.UserEmail,
                    Name = u.UserName,
                    SubscribedNews = u.SubscribedToNews
                }).ToList();
                return View("users", model);
            }

            user.UserName = userModel.Name;
            user.UserEmail = userModel.Email;
            if(userModel.Email.Length != 0) {
                user.PasswordHash = Crypto.SHA256(userModel.Password + "tralalero");
            }
            _context.SaveChanges();


            return RedirectToAction("users");
        }

        [HttpPost]
        public ActionResult DeleteUser(UsersPageModel model) {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var userModel = model.UserDeleteModel;
            var existingUser = _context.Users.FirstOrDefault(u => u.Id ==  userModel.Id);
            if(existingUser == null)
            {
                ViewBag.ErrorMessage = "There is no such user";
                model.UserDeleteModel = new UserDeleteModel();
                return View("users",model);
            }

            _context.Users.Remove(existingUser);
            _context.SaveChanges();
            return RedirectToAction("users");
        }


        [HttpPost]
        public ActionResult AddCategory(CategoryPageModel model)
        {
            if (Session["AdminID"] == null)
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

        [HttpPost]
        public ActionResult DeleteCategory(int ID)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == ID);

            if(category == null)
            {
                ViewBag.ErrorMessage = "No category was found";
                return View("category");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            TempData["Success"] = "Category deleted successfully!";
            return RedirectToAction("category", "admin");
        }

        [HttpPost]
        public ActionResult UpdateCategory(int ID, string newTitle)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == ID);

            if (category == null)
            {
                ViewBag.ErrorMessage = "No category was found";
                return View("category");
            }

            category.Title = newTitle;
            _context.SaveChanges();

            TempData["Success"] = "Category updated successfully!";
            return RedirectToAction("category", "admin");
        }
    }
}
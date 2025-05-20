using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.Domain.Entities.Admin;
using culinaryConnect.BusinessLogic.Core;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities.User;
using culinaryConnect.Domain.Entities.Recipe;
using culinaryConnect.Domain.Entities.Recipe.AdminRecipes;
using culinaryConnect.Domain.Entities.Recipe.AdminRecipe;
using System.Web;
using System.IO;
using culinaryConnect.Domain.Entities.CategoryModels.AdminCategories;
using culinaryConnect.Domain.Entities.CategoryModels.AdminCategory;
using culinaryConnect.BusinessLogic.Interfaces;

namespace Culinary_connect_web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _service = new AdminService();

        // Pages
        public ActionResult Index()
        {
            if (Session["AdminID"] != null) {
                var usersListDB = _service.GetAllUsers();

                var usersList = _service.ConvertDbToViewUsers(usersListDB);

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

            var users = _service.GetAllUsers();
            var usersList = _service.ConvertDbToViewUsersAndNews(users);
            model.Users = usersList;
            return View(model);
        }

        public ActionResult Recipes(RecipesAdminPageModel model)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var recipes = _service.GetAllRecipes();
            var recipesList = _service.ConvertDbToViewsRecipesAdmin(recipes);

            model.Recipes = recipesList;
            return View(model);
        }


        public ActionResult Recipe(int Id)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var recipe = _service.GetRecipeAndAbout(Id);

            if(recipe == null)
            {
                ViewBag.ErrorMessage = "There is no such recipe";
                return View();
            }
            var recipeAdmin = new RecipeAdmin
            {
                Id = recipe.Id,
                Title = recipe.Title,
                AboutObject = new RecipeAbout
                {
                    CookingTime = recipe.AboutRecipe.CookingTime,
                    Description = recipe.AboutRecipe.Description,
                    Ingredients = recipe.AboutRecipe.Ingredients,
                    Instructions = recipe.AboutRecipe.Instructions,
                },
                CreatedDate = recipe.CreatedDate.ToShortDateString(),
                ImagePath = recipe.ImagePath,
                Status = recipe.Status.ToString(),
            };
            var recipeAuthorUser = _service.GetUser(recipe.AuthorID);
            if(recipeAuthorUser == null)
            {
                var recipeAuthorAdmin = _service.GetUser(recipe.AuthorID);
                if(recipeAuthorAdmin != null)
                {
                    recipeAdmin.Author = recipeAuthorAdmin.UserEmail;
                } else
                {
                    recipeAdmin.Author = null;
                }
            } else
            {
                recipeAdmin.Author = recipeAuthorUser.UserEmail;
            }

            if(recipe.CategoryID != 0)
            {
                var recipeCategory = _service.GetCategory(recipe.CategoryID);
                if (recipeCategory != null)
                {
                    recipeAdmin.Category = new CategoryRecipeDb { 
                        Id = recipeCategory.Id,
                        Name = recipeCategory.Title
                    };
                }
            } 
            // get the list of all the categories to choose from
            var categories = _service.GetAllCategories();
            var categoiresList = _service.ConvertDbToCategoryRecipeDb(categories);

            recipeAdmin.CategoryDbList = categoiresList;

            var model = new RecipeAdminPageModel
            {
                RecipeInfo = recipeAdmin,
                RecipeDelete = new RecipeDeleteAdminModel(),
                RecipeUpdate = new RecipeUpdateAdminModel
                {
                    Id = recipe.Id,
                    Status = recipe.Status.ToString(),
                    Title = recipe.Title
                }
            };
            return View(model);
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }


        public ActionResult Categories()
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }
            var categories = _service.GetAllCategories();
            var categoriesList = _service.ConvertDbToCategory(categories);

            var model = new CategoriesPageModel
            {
                FormCategory = new CategoriesForm(),
                Categories = categoriesList
            };

            return View(model);
        }

        public ActionResult Category(int id)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var category = _service.GetCategory(id);
            if(category == null)
            {
                return RedirectToAction("categories");
            }

            var model = new Category { 
                Id = category.Id,
                Title = category.Title
            };

            return View(model);
        }


        // Functions

        // Login
        [HttpPost]
        public ActionResult Logout() {
            Session["AdminID"] = null;
            Session["AdminEmail"] = null;
            Session["Role"] = null;
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


            var admin = _service.GetUserByCredentials(model.AdminEmail, model.AdminPassword);

            if (admin != null)
            {
                Session["AdminID"] = admin.Id;
                Session["AdminEmail"] = admin.UserEmail;
                Session["Role"]=admin.Role.ToString();
                return RedirectToAction("index");
            }

            ViewBag.ErrorMessage = "Login failed";
            return View("login");
        }

        //#####################################################
        //#####################################################

        // Recipe
        [HttpPost]
        public ActionResult CreateRecipe(RecipesAdminPageModel model, HttpPostedFileBase RecipeImage)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var recipeCreateModel = model.RecipeCreateAdminModel;

            var recipeAboutToCreate = new RecipeAboutDB
            {
                CookingTime = recipeCreateModel.RecipeAbout.CookingTime,
                Description = recipeCreateModel.RecipeAbout.Description,
                Ingredients = recipeCreateModel.RecipeAbout.Ingredients != null ? string.Join("###", recipeCreateModel.RecipeAbout.Ingredients) : null,
                Instructions = recipeCreateModel.RecipeAbout.Instructions != null ? string.Join("###", recipeCreateModel.RecipeAbout.Instructions) : null,
            };

            _service.AddRecipeAbout(recipeAboutToCreate);

            var recipeToCreate = new RecipeDB
            {
                Title = recipeCreateModel.Title,
                AboutRecipeID = recipeAboutToCreate.Id,
                CreatedDate = DateTime.Now,
                Status = Status.Pending,
                AuthorID = Session["AdminID"] != null ? Convert.ToInt32(Session["AdminID"]) : 0
            };

            if (RecipeImage != null && RecipeImage.ContentLength > 0)
            {
                var fileName = Path.GetFileName(RecipeImage.FileName);
                var path = Server.MapPath("~/Content/Images/recipe/" + fileName);
                RecipeImage.SaveAs(path);

                recipeToCreate.ImagePath = fileName;
            }

            _service.AddRecipe(recipeToCreate);

            return RedirectToAction("recipes");
        }

        public ActionResult EditRecipe(RecipeAdminPageModel model, HttpPostedFileBase RecipeImage)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var editModel = model.RecipeUpdate;
            if(editModel == null)
            {
                return RedirectToAction("recipes");
            }

            var recipe = _service.GetRecipe(editModel.Id);
            if(recipe == null)
            {
                ViewBag.ErrorMessage = "there is no such recipe";
                model.RecipeUpdate = new RecipeUpdateAdminModel
                {
                    Id = model.RecipeInfo.Id,
                    Status = model.RecipeInfo.Status,
                    Title = model.RecipeInfo.Title
                };
                return View("recipe", new { id = editModel.Id });
            }

            if(editModel.Status == "Active")
            {
                recipe.Status = Status.Active;
            } else
            {
                recipe.Status = Status.Pending;
            }

            recipe.Title = editModel.Title;
            if(editModel.CategoryDbId != 0)
            {
                recipe.CategoryID = editModel.CategoryDbId;
            }

            if(RecipeImage != null && RecipeImage.ContentLength > 0)
            {
                var fileName = Path.GetFileName(RecipeImage.FileName);
                var path = Server.MapPath("~/Content/Images/recipe/" + fileName);
                RecipeImage.SaveAs(path);

                _service.UpdateRecipeByImage(recipe, fileName);
            } else
            {
                _service.UpdateRecipeByImage(recipe, null);
            }

            return RedirectToAction("recipe", new {id = editModel.Id});
        }

        public ActionResult DeleteRecipe(RecipeAdminPageModel model)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var deleteModel = model.RecipeDelete;
            var recipeToDelete = _service.GetRecipe(deleteModel.Id);
            if(recipeToDelete == null)
            {
                return RedirectToAction("recipes");
            }

            _service.DeleteRecipe(recipeToDelete);

            return RedirectToAction("recipes");
        }


        public ActionResult UpdateStatusRecipe(int Id, string NewStatus)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var recipe = _service.GetRecipe(Id);
            if (recipe != null)
            {
                _service.UpdateRecipeStatus(recipe, NewStatus);
            }

            return RedirectToAction("recipes");
        }

        //#####################################################
        //#####################################################

        // Users
        [HttpPost]
        public ActionResult CreateUser(UsersPageModel model)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var userModel = model.UserRegisterModel;
            var existingUser = _service.GetUserByEmail(userModel.UserEmail);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Such a user already exists";
                model.UserRegisterModel = new UserRegisterModel();
                return View("Users", model);
            }

            var userPasswordHash = Crypto.SHA256(userModel.UserPassword + "tralalero");
            var userToAdd = new UserDB
            {
                UserEmail = userModel.UserEmail,
                UserName = userModel.UserName,
                PasswordHash = userPasswordHash
            };

            _service.AddUser(userToAdd);
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

            var user = _service.GetUser(userModel.Id);
            if(user == null)
            {
                ViewBag.ErrorMessage = "There is no such user" + userModel.Id;
                model.UserEditModel = new UserEditModel();
                var users = _service.GetAllUsers();
                var usersList = _service.ConvertDbToViewUsersAndNews(users);
                model.Users = usersList;
                return View("users", model);
            }

            _service.UpdateUser(user, userModel.Name, userModel.Email, userModel.Password);

            return RedirectToAction("users");
        }

        [HttpPost]
        public ActionResult DeleteUser(UsersPageModel model) {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var userModel = model.UserDeleteModel;
            var existingUser = _service.GetUser(userModel.Id);
            if(existingUser == null)
            {
                ViewBag.ErrorMessage = "There is no such user";
                model.UserDeleteModel = new UserDeleteModel();
                return View("users",model);
            }

            _service.DeleteUser(existingUser);

            return RedirectToAction("users");
        }

        //#####################################################
        //#####################################################

        // Category
        [HttpPost]
        public ActionResult AddCategory(CategoriesPageModel model)
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

            _service.AddCategory(categoryDB);

            model.FormCategory = new CategoriesForm();

            TempData["Success"] = "Category created successfully!";
            return RedirectToAction("categories");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int ID)
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }

            var category = _service.GetCategory(ID);

            if(category == null)
            {
                ViewBag.ErrorMessage = "No category was found";
                return View("category");
            }

            _service.DeleteCategory(category);

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

            var category = _service.GetCategory(ID);

            if (category == null)
            {
                ViewBag.ErrorMessage = "No category was found";
                return View("category");
            }

            _service.UpdateCategory(category, newTitle);

            TempData["Success"] = "Category updated successfully!";
            return RedirectToAction("category", new {id = category.Id});
        }
    }
}
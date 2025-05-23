using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.Domain.Entities.CategoryModels.AdminCategory;
using culinaryConnect.Domain.Entities.Recipe.AdminRecipe;
using culinaryConnect.Domain.Entities.Recipe;
using culinaryConnect.Domain.Entities.Recipe.AdminRecipes;
using culinaryConnect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace culinaryConnect.BusinessLogic
{
    public class AdminApi
    {
        private readonly CulinaryContext _context = new CulinaryContext();

        public List<UserDB> GetAllUsersFromDB()
        {
            var users = _context.Users.ToList();
            return users;
        }
        public UserDB GetUserByIdFromDB(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public UserDB GetUserByEmailFromDB(string email)
        {
            return _context.Users.FirstOrDefault(u => u.UserEmail == email);
        }
        public UserDB GetUserByCredentialsFromDB(string email, string password)
        {
            var salt = "123";
            var hashedPassword = Crypto.SHA256(password + salt);
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email && u.PasswordHash == hashedPassword);
            return user;
        }
        public void AddUserDB(UserDB user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void UpdateUserDB(UserDB user, string name, string email, string password)
        {
            user.UserName = name;
            user.UserEmail = email;
            if (email.Length != 0)
            {
                user.PasswordHash = Crypto.SHA256(password + "tralalero");
            }

            _context.SaveChanges();
        }
        public void DeleteUserDB(UserDB user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        public List<User> ConvertDbToViewUsersAction(List<UserDB> usersDB)
        {
            var usersList = usersDB.Select(u => new User
            {
                Id = u.Id,
                Email = u.UserEmail,
                Name = u.UserName
            }).ToList();

            return usersList;
        }
        public List<User> ConvertDbToViewUsersAndNewsAction(List<UserDB> usersDB)
        {
            var usersList = usersDB.Select(u => new User
            {
                Id = u.Id,
                Email = u.UserEmail,
                Name = u.UserName,
                SubscribedNews = u.SubscribedToNews,
            }).ToList();

            return usersList;
        }
        public List<RecipeDB> GetAllRecipesDB()
        {
            var recipes = _context.Recipes.ToList();
            return recipes;
        }
        public RecipeDB GetRecipeDB(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            return recipe;
        }

        public List<RecipesAdmin> ConvertDbToViewsRecipesAdminAction(List<RecipeDB> recipesDB)
        {
            var recipes = recipesDB.Select(r => new RecipesAdmin
            {
                Id = r.Id,
                Title = r.Title,
                Status = r.Status.ToString(),
                ImagePath = r.ImagePath,
                CreatedDate = r.CreatedDate.ToShortDateString(),
            }).ToList();

            return recipes;
        }
        public RecipeDB GetRecipeAndAboutAction(int id)
        {
            return _context.Recipes.Include("AboutRecipe").FirstOrDefault(r => r.Id == id);
        }
        public List<CategoryDB> GetAllCategoriesDB()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }
        public CategoryDB GetCategoryDB(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public void AddCategoryDB(CategoryDB category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategoryDB(CategoryDB category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public void UpdateCategoryDB(CategoryDB category, string title)
        {
            category.Title = title;
            _context.SaveChanges();
        }

        public List<CategoryRecipeDb> ConvertDbToCategoryRecipeDbAction(List<CategoryDB> categoriesDB)
        {
            var categories = categoriesDB.Select(c => new CategoryRecipeDb
            {
                Id = c.Id,
                Name = c.Title,
            }).ToList();

            return categories;
        }

        public List<Category> ConvertDbToCategoryAction(List<CategoryDB> categoriesDB)
        {
            var categories = categoriesDB.Select(c => new Category
            {
                Id = c.Id,
                Title = c.Title,
                RecipesID = c.Recipies
            }).ToList();

            return categories;
        }

        public void AddRecipeAboutDB(RecipeAboutDB recipeAboutDB)
        {
            _context.RecipesAboutDb.Add(recipeAboutDB);
            _context.SaveChanges();
        }

        public void AddRecipeDB(RecipeDB recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }

        public void DeleteRecipeDB(RecipeDB recipe)
        {
            _context.Recipes.Remove(recipe);
            _context.RecipesAboutDb.Remove(recipe.AboutRecipe);
            _context.SaveChanges();
        }

        public void UpdateRecipeStatusAction(RecipeDB recipe, string status)
        {
            if (Enum.TryParse(status, out Status statusEnum))
            {
                recipe.Status = statusEnum;
                _context.SaveChanges();
            }
        }

        public void UpdateRecipeByImageAction(RecipeDB recipe, string fileName)
        {
            recipe.ImagePath = fileName;
            _context.SaveChanges();
        }
    }
}

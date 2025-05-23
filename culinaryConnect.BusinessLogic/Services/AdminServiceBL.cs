using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.Domain.Entities.Admin;
using System.Web.Helpers;
using culinaryConnect.Domain.Entities.User;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities.Recipe.AdminRecipes;
using culinaryConnect.Domain.Entities.Recipe.AdminRecipe;
using culinaryConnect.Domain.Entities.CategoryModels.AdminCategory;
using culinaryConnect.Domain.Entities.Recipe;


namespace culinaryConnect.BusinessLogic.Services
{
    public class AdminServiceBL:AdminApi, IAdminService
    {
        
        public List<UserDB> GetAllUsers() 
        {
            return GetAllUsersFromDB();
        }

        public UserDB GetUser(int id)
        {
            return GetUserByIdFromDB(id);
        }

        public UserDB GetUserByEmail(string email)
        {
            return GetUserByEmailFromDB(email);
        }

        public UserDB GetUserByCredentials(string email, string password)
        {
            return GetUserByCredentialsFromDB(email, password);
        }

        public void AddUser(UserDB user)
        {
            AddUserDB(user);
        }

        public void UpdateUser(UserDB user, string name, string email, string password)
        {
            UpdateUserDB(user, name, email, password);
        }

        public void DeleteUser(UserDB user)
        {
            DeleteUserDB(user);
        }

        public List<User> ConvertDbToViewUsers(List<UserDB> usersDB) 
        {
            return ConvertDbToViewUsersAction(usersDB);
        }

        public List<User> ConvertDbToViewUsersAndNews(List<UserDB> usersDB)
        {
            return ConvertDbToViewUsersAndNewsAction(usersDB);
        }

        public List<RecipeDB> GetAllRecipes() 
        {
            return GetAllRecipesDB();
        }

        public RecipeDB GetRecipe(int id)
        {
            return GetRecipeDB(id);
        }

        public List<RecipesAdmin> ConvertDbToViewsRecipesAdmin(List<RecipeDB> recipesDB)
        {
            return ConvertDbToViewsRecipesAdminAction(recipesDB);
        }

        public RecipeDB GetRecipeAndAbout(int id)
        {
            return GetRecipeAndAboutAction(id);
        }

        public List<CategoryDB> GetAllCategories()
        {
            return GetAllCategoriesDB();
        }

        public CategoryDB GetCategory(int id)
        {
            return GetCategoryDB(id);
        }

        public void AddCategory(CategoryDB category)
        {
            AddCategoryDB(category);
        }

        public void DeleteCategory(CategoryDB category)
        {
           DeleteCategoryDB(category);
        }

        public void UpdateCategory(CategoryDB category, string title)
        {
            UpdateCategoryDB(category, title);
        }

        public List<CategoryRecipeDb> ConvertDbToCategoryRecipeDb(List<CategoryDB> categoriesDB)
        {
            return ConvertDbToCategoryRecipeDbAction(categoriesDB);
        }

        public List<Category> ConvertDbToCategory(List<CategoryDB> categoriesDB)
        {
            return ConvertDbToCategoryAction(categoriesDB);
        }

        public void AddRecipeAbout(RecipeAboutDB recipeAboutDB)
        {
            AddRecipeAboutDB(recipeAboutDB);
        }

        public void AddRecipe(RecipeDB recipe)
        {
            AddRecipeDB(recipe);
        }

        public void DeleteRecipe(RecipeDB recipe)
        {
            DeleteRecipeDB(recipe);
        }

        public void UpdateRecipeStatus(RecipeDB recipe, string status)
        {
            UpdateRecipeStatusAction(recipe, status);
        }

        public void UpdateRecipeByImage(RecipeDB recipe, string fileName)
        {
            UpdateRecipeByImageAction(recipe, fileName);
        }
    }
}

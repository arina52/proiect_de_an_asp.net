using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.Domain.Entities.CategoryModels.AdminCategory;
using culinaryConnect.Domain.Entities.Recipe.AdminRecipe;
using culinaryConnect.Domain.Entities.Recipe.AdminRecipes;
using culinaryConnect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        List<UserDB> GetAllUsers();
        UserDB GetUser(int id);
        UserDB GetUserByEmail(string email);
        UserDB GetUserByCredentials(string email, string password);

        void AddUser(UserDB user);

        void UpdateUser(UserDB user, string name, string email, string password);

        void DeleteUser(UserDB user);

        List<User> ConvertDbToViewUsers(List<UserDB> usersDB);
        List<User> ConvertDbToViewUsersAndNews(List<UserDB> usersDB);

        List<RecipeDB> GetAllRecipes();

        RecipeDB GetRecipe(int id);

        List<RecipesAdmin> ConvertDbToViewsRecipesAdmin(List<RecipeDB> recipesDB);

        RecipeDB GetRecipeAndAbout(int id);

        List<CategoryDB> GetAllCategories();

        CategoryDB GetCategory(int id);

        void AddCategory(CategoryDB category);

        void DeleteCategory(CategoryDB category);

        void UpdateCategory(CategoryDB category, string title);

        List<CategoryRecipeDb> ConvertDbToCategoryRecipeDb(List<CategoryDB> categoriesDB);

        List<Category> ConvertDbToCategory(List<CategoryDB> categoriesDB);

        void AddRecipeAbout(RecipeAboutDB recipeAboutDB);

        void AddRecipe(RecipeDB recipe);

        void DeleteRecipe(RecipeDB recipe);

        void UpdateRecipeStatus(RecipeDB recipe, string status);

        void UpdateRecipeByImage(RecipeDB recipe, string fileName);
    }
}

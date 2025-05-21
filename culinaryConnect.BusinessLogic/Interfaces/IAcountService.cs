using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using culinaryConnect.Domain.Entities.User;

namespace culinaryConnect.BusinessLogic.Interfaces
{
    public interface IAcountService
    {
        List<CategoryDB> GetCategoryList();
        User GetUserByID(int Id);
        void AddRecipe(RecipesPageModel recipeModel, int userId, string image);
        void DeleteRecipe(int id, int userId);
        void SubscribeUserToNewsletter(string email);
        void UpdateRecipe(RecipesPageModel recipeModel, int userId, string image);

    }
}

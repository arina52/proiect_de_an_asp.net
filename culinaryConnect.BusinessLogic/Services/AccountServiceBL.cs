using culinaryConnect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.BusinessLogic.Data;
using System.IO;
using culinaryConnect.Domain.Entities.Recipe;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.Domain.Entities.User;

namespace culinaryConnect.BusinessLogic.Services
{
    public class AccountServiceBL: UserApi, IAcountService
    {

        public List<CategoryDB> GetCategoryList()
        {
            return GetCategoryListFromDB();
        }

        public User GetUserByID(int Id)
        {
            return GetUserByIdFromDB(Id);
        }
        public void AddRecipe(RecipesPageModel recipeModel, int userId, string image)
        {
            AddRecipeDB(recipeModel, userId, image);
        }

        public void DeleteRecipe(int id, int userId)
        {
            DeleteRecipeDB(id, userId);
        }


        public void SubscribeUserToNewsletter(string email)
        {
            SubscribeUserToNewsletterAction(email);
        }
        public void UpdateRecipe(RecipesPageModel model, int userId, string imageFileName = null)
        {
            UpdateRecipeAction(model, userId, imageFileName);
        }


    }
}

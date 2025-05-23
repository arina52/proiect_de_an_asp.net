using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities;
using culinaryConnect.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.BusinessLogic.Services
{
    public class RecipeServiceBL : UserApi, IRecipeService
    {
       
        public List<RecipeDetails> GetAllRecipes()
        {
            return GetAllRecipesFromDB();
        }

        public List<RecipeDetails> GetAllActiveRecipes()
        {
            return GetAllActiveRecipesFromDB();
        }
        public RecipeDetails GetRecipeById(int Id)
        {
            return GetRecipeByIdFromDB(Id);
        }

        public RecipeDB GetRecipeEntityById(int Id)
        {
            return GetRecipeEntityByIdFromDB(Id);
        }
    }
}

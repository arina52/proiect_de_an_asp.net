using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities.Recipe;

namespace culinaryConnect.BusinessLogic.Interfaces
{
    public interface IRecipeService
    {
        List<RecipeDetails> GetAllRecipes();
        RecipeDetails GetRecipeById(int id);
        RecipeDB GetRecipeEntityById(int Id);

    }
}

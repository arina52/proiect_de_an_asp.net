using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using culinaryConnect.Domain.Entities.Recipe;

namespace culinaryConnect.BusinessLogic.Interfaces
{
    public interface IRecipeService
    {
        IEnumerable<RecipeDetails> GetAllRecipes();
        RecipeDetails GetRecipeById(int id);
    }
}

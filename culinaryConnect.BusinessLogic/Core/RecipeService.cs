using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.BusinessLogic.Core
{
    public class RecipeService : IRecipeService
    {
        private List<RecipeDetails> _recipes = new List<RecipeDetails>
            {
                new RecipeDetails { Id = 1, Title = "Supa crema de morcovi", CategoryID = "1", 
                    Description = "O supă cremoasă și fină din morcovi dulci, asezonată cu condimente aromate și servită cu crutoane crocante – o alegere sănătoasă și reconfortantă."},
               
            };
        public IEnumerable<RecipeDetails> GetAllRecipes()
        {
            return _recipes;
        }

        public RecipeDetails GetRecipeById(int id)
        {
            return _recipes.FirstOrDefault(r => r.Id == id);
        }

    }
}

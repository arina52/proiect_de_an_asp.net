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
                new RecipeDetails { Id = 1, Title = "Supa crema de morcovi", Category = "Soup", Image = "recipe-1.jpg", Description = "O supă cremoasă și fină din morcovi dulci, asezonată cu condimente aromate și servită cu crutoane crocante – o alegere sănătoasă și reconfortantă."},
                new RecipeDetails { Id = 2, Title = "Cheesecake cu afine", Category = "Dessert", Image = "recipe-2.jpg", Description = "Un desert fin și răcoritor, cu blat crocant, cremă catifelată de brânză și un topping delicios de afine proaspete – combinația perfectă între dulce și fructat."},
                new RecipeDetails { Id = 3, Title = "Somon cu susan", Category = "Main Course", Image = "recipe-3.jpg", Description = "File de somon fraged, gătit la cuptor și presărat cu semințe de susan crocante, servit cu un strop de sos asiatic pentru un gust rafinat și echilibrat."},
                new RecipeDetails { Id = 4, Title = "Burger cu cartofi", Category = "Main Course", Image = "big-2.jpg", Description = "O supă cremoasă și aromată, pregătită din ciuperci proaspete, smântână și mirodenii delicate – perfectă pentru un prânz reconfortant."},
                new RecipeDetails { Id = 5, Title = "Inghetata cu fructe", Category="Dessert", Image = "big-3.jpg", Description = "O înghețată răcoritoare, preparată din fructe proaspete și cremoasă cât trebuie – desertul ideal pentru zilele calde."},
                new RecipeDetails { Id = 6, Title = "Caracatita cu suc de lamaie", Category = "Main Course", Image = "big-4.jpg", Description = "O combinație rafinată de caracatiță fragedă, gătită lent și asezonată cu suc proaspăt de lămâie pentru un gust mediteranean autentic."}
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

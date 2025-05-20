using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Recipe
{
    public class RecipesPageModel
    {
        public RecipeDetails RecipeForm { get; set; }
        public List<RecipeDetails> RecipeList { get; set; }
        public List<CategoryUser> Categories { get; set; }
    }
}

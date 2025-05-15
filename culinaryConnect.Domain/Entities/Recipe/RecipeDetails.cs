using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Recipe
{
    public class RecipeDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public RecipeAbout AboutRecipe { get; set; }
        public int CategoryID { get; set; }
        public string ImagePath { get; set; }

    }
}

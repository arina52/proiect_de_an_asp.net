using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Recipe.AdminRecipe
{
    public class RecipeAdmin
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public RecipeAbout AboutObject { get; set; }

        public string ImagePath { get; set; }

        public CategoryRecipeDb Category { get; set; }

        public List<CategoryRecipeDb> CategoryDbList { get; set; }

        public string CreatedDate { get; set; }

        public string Status { get; set; }

        public string Author { get; set; }
    }
}

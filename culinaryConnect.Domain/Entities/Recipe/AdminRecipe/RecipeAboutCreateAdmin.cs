using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Recipe.AdminRecipe
{
    public class RecipeAboutCreateAdmin
    {
        public string CookingTime { get; set; }
        public string Description { get; set; }
        
        public List<string> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
    }
}

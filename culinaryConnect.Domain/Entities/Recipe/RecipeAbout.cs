using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Recipe
{
    public class RecipeAbout
    {
        public string CookingTime { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Recipe.AdminRecipe
{
    public class RecipeCreateAdminModel
    {
        public string Title { get; set; }

        public RecipeAboutCreateAdmin RecipeAbout { get; set; }
    }
}
    
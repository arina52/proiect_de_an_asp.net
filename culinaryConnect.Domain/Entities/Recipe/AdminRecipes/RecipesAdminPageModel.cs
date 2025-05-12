using culinaryConnect.Domain.Entities.Recipe.AdminRecipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Recipe.AdminRecipes
{
    public class RecipesAdminPageModel
    {
        public List<RecipesAdmin> Recipes { get; set; }

        public Status UpdateStatus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Recipe.AdminRecipe
{
    public class RecipeAdminPageModel
    {
        public RecipeAdmin RecipeInfo { get; set; }

        public RecipeUpdateAdminModel RecipeUpdate { get; set; }

        public RecipeDeleteAdminModel RecipeDelete { get; set; }
    }
}

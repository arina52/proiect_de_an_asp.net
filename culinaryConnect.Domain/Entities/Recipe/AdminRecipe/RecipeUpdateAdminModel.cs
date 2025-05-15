using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace culinaryConnect.Domain.Entities.Recipe.AdminRecipe
{
    public class RecipeUpdateAdminModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public int CategoryDbId  { get; set; }
    }
}

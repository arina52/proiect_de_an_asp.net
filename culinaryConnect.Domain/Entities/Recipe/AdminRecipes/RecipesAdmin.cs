using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Recipe.AdminRecipes
{
    public class RecipesAdmin
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
    }
}

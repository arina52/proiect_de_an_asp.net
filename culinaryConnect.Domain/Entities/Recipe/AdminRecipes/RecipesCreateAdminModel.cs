using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Recipe.AdminRecipes
{
    public class RecipesCreateAdminModel
    {
        public string Title { get; set; }

        public string CategoryID { get; set; }

        public DateTime CreatedDate { get; set; }

        public Status Status { get; set; }

        public string AuthorID { get; set; }
    }
}

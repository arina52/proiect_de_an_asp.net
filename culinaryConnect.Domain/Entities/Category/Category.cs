using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Category
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> RecipesID { get; set; }
    }
}

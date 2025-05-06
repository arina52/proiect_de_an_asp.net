using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Category
{
    public class CategoryPageModel
    {
        public CategoryForm FormCategory { get; set; }
        public List<Category> Categories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using culinaryConnect.Domain.Entities.CategoryModels.AdminCategory;

namespace culinaryConnect.Domain.Entities.CategoryModels.AdminCategories

{
    public class CategoriesPageModel
    {
        public CategoriesForm FormCategory { get; set; }
        public List<Category> Categories { get; set; }
    }
}

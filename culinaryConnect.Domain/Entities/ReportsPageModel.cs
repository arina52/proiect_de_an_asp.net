using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.Reports
{
    public class ReportsPageModel
    {
        public List<ReportsCategory> categories;

        public List<ReportsRecipe> recipes;

        public List<ReportsUser> users;
    }
}

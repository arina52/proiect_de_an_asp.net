using culinaryConnect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.BusinessLogic.Models
{
    public class FavoriteRecipeDB
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual UserDB.UserDB User { get; set; }

        public int RecipeId { get; set; }
        public virtual RecipeDB Recipe { get; set; }

    }
}

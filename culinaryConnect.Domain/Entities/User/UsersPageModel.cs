using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.Domain.Entities.User
{
    public class UsersPageModel
    {
        public List<User> Users { get; set; }
        public UserRegisterModel UserRegisterModel { get; set; }

        public UserDeleteModel UserDeleteModel { get; set; }

        public UserEditModel UserEditModel { get; set; }
    }
}

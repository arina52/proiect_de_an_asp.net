using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.BusinessLogic.Models.UserDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.BusinessLogic.Core
{
    public class UserService : IUserService
    {
        private readonly CulinaryContext _context;

        public UserService()
        {
            _context = new CulinaryContext();
        }

        public List<UserDB> GetAllUsers ()
        {
            var users = _context.Users.ToList();

            return users;
        }
    }
}

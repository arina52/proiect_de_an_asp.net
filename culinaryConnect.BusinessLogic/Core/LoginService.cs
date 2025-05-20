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
    public class LoginService : ILoginService
    {
        private readonly CulinaryContext _context;

        public LoginService()
        {
            _context = new CulinaryContext();
        }

        public UserDB GetUserByEmailAndPassword(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email && u.PasswordHash == password);
            return user;
        }

        public UserDB GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email);
            return user;
        }
    }
}

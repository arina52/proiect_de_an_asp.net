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
    public class SignupService : ISignupService
    {
        private readonly CulinaryContext _context;

        public SignupService()
        {
            _context = new CulinaryContext();
        }

        public UserDB GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email);
            return user;
        }

        public void AddNewUser(UserDB user) { 
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateSemiExistingUser(UserDB user, string hashedPassword, string userEmail)
        {
            user.PasswordHash = hashedPassword;
            user.UserName = userEmail;
            _context.SaveChanges();
        }
    }
}

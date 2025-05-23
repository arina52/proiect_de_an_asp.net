using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.BusinessLogic.Models.UserDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.BusinessLogic.Services
{
    public class SignupServiceBL : UserApi, ISignupService
    {
        
        public UserDB GetUserByEmail(string email)
        {
            return GetUserByEmailAction(email);
        }

        public void AddNewUser(UserDB user) { 
            AddNewUserToDB(user);
        }

        public void UpdateSemiExistingUser(UserDB user, string hashedPassword, string userEmail)
        {
            UpdateSemiExistingUserDB(user, hashedPassword, userEmail);
        }
    }
}

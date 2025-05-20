using culinaryConnect.BusinessLogic.Models.UserDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.BusinessLogic.Interfaces
{
    public interface ISignupService
    {
        UserDB GetUserByEmail(string email);

        void AddNewUser(UserDB user);

        void UpdateSemiExistingUser(UserDB user, string hashedPassword, string userEmail);
    }
}

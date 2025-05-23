using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.BusinessLogic.Models.UserDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.BusinessLogic.Services
{
    public class LoginServiceBL : UserApi, ILoginService
    {

        public UserDB GetUserByEmailAndPassword(string email, string password)
        {
            return GetUserByEmailandPasswordAction(email, password);
        }

        public UserDB GetUserByEmail(string email)
        {
            return GetUserByEmailAction(email);
        }
    }
}

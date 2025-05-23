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
    public class UserServiceBL : UserApi, IUserService
    {

        public List<UserDB> GetAllUsers ()
        {
            return GetAllUsersFromDB();
        }
    }
}

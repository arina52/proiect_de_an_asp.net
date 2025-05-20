using culinaryConnect.BusinessLogic.Models.UserDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace culinaryConnect.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        List<UserDB> GetAllUsers();
    }
}

using culinaryConnect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using culinaryConnect.BusinessLogic.Services;

namespace culinaryConnect.BusinessLogic
{
    public class BusinessLogic
    {
        public ILoginService GetLoginService()
        {
            return new LoginServiceBL();
        }
        public ISignupService GetSignupService()
        {
            return new SignupServiceBL();
        }
        public IUserService GetUserService()
        {
            return new UserServiceBL();
        }
        public IAcountService GetAccountService()
        {
            return new AccountServiceBL();
        }
        public IRecipeService GetRecipeService()
        {
            return new RecipeServiceBL();
        }
        public IAdminService GetAdminService()
        {
            return new AdminServiceBL();
        }
    }
}

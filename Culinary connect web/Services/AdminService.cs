using System.Linq;
using System.Web.Helpers;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models.AdminDB;

namespace culinaryConnect.Web.Services.AdminService
{
    public class AdminService
    {
        private readonly CulinaryContext _context = new CulinaryContext();

        public AdminDB GetByCredentials(string email, string password)
        {
            var salt = "123";
            var hashedPassword = Crypto.SHA256(password + salt);
            return _context.Admins.FirstOrDefault(a => a.AdminEmail == email && a.PasswordHash == hashedPassword);
        }
    }
}

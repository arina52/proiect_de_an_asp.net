using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.Domain.Entities.Admin;
using System.Web.Helpers;


namespace culinaryConnect.BusinessLogic.Core
{
    public class AdminService: IAdminService
    {
        private readonly CulinaryContext _context;
        public AdminService(CulinaryContext context)
        {
            _context = context;
        }

        public List<UserDB> GetAdminList()
        {
            var adminList = _context.Users
            .Where(u => u.Role == Role.Admin)
            .ToList();
            return adminList;
        }

        public UserDB GetByCredentials(string email, string password)
        {
            var Admins = GetAdminList();
            var salt = "123";
            var hashedPassword = Crypto.SHA256(password + salt);
            return Admins.FirstOrDefault(a => a.UserEmail == email 
            && a.PasswordHash == hashedPassword);
        }
    }
}

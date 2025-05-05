using System.Data.Entity;
using culinaryConnect.BusinessLogic.Models.AdminDB;
using culinaryConnect.BusinessLogic.Data;

namespace culinaryConnect.BusinessLogic.Seed
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<CulinaryContext>
    {
        protected override void Seed(CulinaryContext context)
        {
            context.Admins.Add(new AdminDB
            {
                AdminName = "daniel",
                AdminEmail = "daniel@gmail.com",
                // 123 password + 123 salt
                PasswordHash = "932F3C1B56257CE8539AC269D7AAB42550DACF8818D075F0BDF1990562AAE3EF"
            });

            context.SaveChanges();
        }
    }
}
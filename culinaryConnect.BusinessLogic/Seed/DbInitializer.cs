using System.Data.Entity;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models.User;

namespace culinaryConnect.BusinessLogic.Seed
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<CulinaryContext>
    {
        protected override void Seed(CulinaryContext context)
        {
            context.Users.Add(new User { 
                Email = "daniel@gmail.com",
                Name = "daniel",
                Password = "Password",
           });

            context.Users.Add(new User {
                Email = "petcov@gmail.com",
                Name = "Petcov",
                Password = "Password",
            });


            context.SaveChanges();
        }
    }
}

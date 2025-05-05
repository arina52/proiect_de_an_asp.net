using System.Collections.Generic;
using culinaryConnect.Domain.Entities.User;

namespace culinaryConnect.Domain.Entities.Admin
{
    public class AdminWraper
    {
        public List<User.User> Users { get; set; }
    }
}

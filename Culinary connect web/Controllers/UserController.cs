using culinaryConnect.BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Culinary_connect_web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private readonly CulinaryContext _context = new CulinaryContext();
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
    }
}
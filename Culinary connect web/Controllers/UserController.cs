using culinaryConnect.BusinessLogic.Core;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
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
        private readonly IUserService _userService = new UserService();
        public ActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }
    }
}
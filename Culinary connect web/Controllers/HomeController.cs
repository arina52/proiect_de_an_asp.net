using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models.UserDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace culinaryConnect.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CulinaryContext _context = new CulinaryContext();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutMe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubscribeNews(string email)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("index", "login");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email);

            if (user == null)
            {

                var newUser = new UserDB();
                newUser.UserEmail = email;
                newUser.SubscribedToNews = true;
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return View();
            }

            user.SubscribedToNews = true;
            _context.SaveChanges();

            return View("index");
        }
    }
}
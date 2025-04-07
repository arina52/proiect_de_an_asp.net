using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.Domain.Entities.User;
using System.Linq;
using System.Web.Mvc;

namespace Culinary_connect_web.Controllers
{
    public class LoginController : Controller
    {

        private readonly CulinaryContext _context = new CulinaryContext();

        [HttpGet]
        public ActionResult Index() { 
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid) {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            
                if (user != null)
                {
                    Session["UserID"] = user.Id;
                    Session["UserName"] = user.Name;

                    return RedirectToActionPermanent("Index", "Home");
                } else
                {
                    ViewBag.ErrorMessage = "Invalid email or password.";
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
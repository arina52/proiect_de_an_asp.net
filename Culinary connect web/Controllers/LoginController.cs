using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.Domain.Entities.User;
using System.Linq;
using System.Web.Mvc;
using System.Web.Helpers;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.BusinessLogic.Core;

namespace Culinary_connect_web.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILoginService _loginService = new LoginService();

        [HttpGet]
        public ActionResult Index() { 
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid) {
                var passwordSalted = model.UserPassword + "tralalero";
                var hashedPassword = Crypto.SHA256(passwordSalted);
                var user = _loginService.GetUserByEmailAndPassword(model.UserEmail, hashedPassword);
            
                if (user != null)
                {
                    Session["UserID"] = user.Id;
                    Session["UserName"] = user.UserName;

                    return RedirectToActionPermanent("index", "home");
                } else
                {
                    var isThereUserWithEmail = _loginService.GetUserByEmail(model.UserEmail);
                    if (isThereUserWithEmail != null)
                    {
                        if(isThereUserWithEmail.PasswordHash != null)
                        {
                            ViewBag.ErrorMessage = "Invalid password.";
                            return View("index", model);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Go create an account.";
                            return View("index", model);
                        }
                    }
                    ViewBag.ErrorMessage = "No such user! Go create account";
                    return View("index", model);
                }
            }
            ViewBag.ErrorMessage = "There is something wrong with the input";
            return View("index", model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("index", "login");
        }
    }
}
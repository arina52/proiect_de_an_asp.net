using System.Web.Helpers; // for Crypto
using culinaryConnect.BusinessLogic.Data;
using System.Linq;
using System.Web.Mvc;
using culinaryConnect.Domain.Entities.User;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.BusinessLogic.Core;
public class SignupController : Controller
{
    private readonly ISignupService _signupService = new SignupService();

    public ActionResult Index()
    {
        return View(new UserRegisterModel());
    }

    [HttpPost]
    public ActionResult Register(UserRegisterModel model)
    {

        if (!ModelState.IsValid)
        {
            ViewBag.ErrorMessage = "The input model is invalid";
            return View("Index", model);
        }
        const string salt = "tralalero";
        var hashedPassword = Crypto.SHA256(model.UserPassword + salt);

        // Check if user already exists
        var user = _signupService.GetUserByEmail(model.UserEmail);
        if (user != null)
        {
            if (user.PasswordHash != null) { 
                ViewBag.ErrorMessage = "User already exists.";
                return View("index", model);
            }
            else
            {
                _signupService.UpdateSemiExistingUser(user, hashedPassword, model.UserEmail);
                TempData["Success"] = "Account created successfully!";
                return RedirectToAction("index", "login");
            }
        }

        var newUser = new UserDB
        {
            UserName = model.UserName,
            UserEmail = model.UserEmail,
            PasswordHash = hashedPassword
        };

        _signupService.AddNewUser(newUser);
         
        TempData["Success"] = "Account created successfully!";
        return RedirectToAction("index", "login");
    }
}

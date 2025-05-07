using System.Web.Helpers; // for Crypto
using culinaryConnect.BusinessLogic.Data;
using System.Linq;
using System.Web.Mvc;
using culinaryConnect.Domain.Entities.User;
using culinaryConnect.BusinessLogic.Models.UserDB;
public class SignupController : Controller
{
    private readonly CulinaryContext _context = new CulinaryContext();

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
        var user = _context.Users.FirstOrDefault(u => u.UserEmail == model.UserEmail);
        if (user != null)
        {
            if (user.PasswordHash != null) { 
                ViewBag.ErrorMessage = "User already exists.";
                return View("index", model);
            }
            else
            {
                user.PasswordHash = hashedPassword;
                user.UserName = model.UserEmail;

                _context.SaveChanges();
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

        _context.Users.Add(newUser);
        _context.SaveChanges();

        TempData["Success"] = "Account created successfully!";
        return RedirectToAction("index", "login");
    }
}

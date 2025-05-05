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

        // Check if user already exists
        var userExists = _context.Users.Any(u => u.UserEmail == model.UserEmail);
        if (userExists)
        {
            ViewBag.ErrorMessage = "User already exists.";
            return View("index", model);
        }

        const string salt = "tralalero";

        // Hash the password
        var hashedPassword = Crypto.SHA256(model.UserPassword + salt);

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

using System.Web.Mvc;

namespace Culinary_connect_web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            } else
            {
                return RedirectToAction("index", "login");
            }
        }
    }
}
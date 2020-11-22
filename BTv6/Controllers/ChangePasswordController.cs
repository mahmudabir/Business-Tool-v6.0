using BTv6.Models;
using BTv6.Repositories.CommonRepositories;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class ChangePasswordController : Controller
    {
        // GET: ChangePassword
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["SID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Index(log_in l, string newpassword, string confirmnewpassword)
        {
            if (Session["SID"] != null)
            {
                if (newpassword == confirmnewpassword)
                {
                    LoginRepository login = new LoginRepository();
                    var lo = login.GetByID((string)Session["LID"]);

                    var LID = (string)Session["LID"];

                    if (ModelState.IsValid)
                    {
                        if (lo.PASS == l.PASS)
                        {
                            BusinessToolDBEntities context = new BusinessToolDBEntities();

                            var userFromDB = context.log_in.Where(x => x.LID == LID).FirstOrDefault();

                            userFromDB.PASS = (string)confirmnewpassword;

                            context.Entry(userFromDB).State = EntityState.Modified;
                            context.SaveChanges();
                            Session.Clear();
                        }
                        else
                        {
                            TempData["Error"] = "Wrong Old Password";
                            return RedirectToAction("Index", "ChangePassword");
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    TempData["Error1"] = "Passworrd does not match!";
                    return RedirectToAction("Index", "ChangePassword");
                }
                TempData["Error2"] = "Successfully Change Password";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
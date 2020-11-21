using BTv6.Models;
using BTv6.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["LID"] == null || Session["SID"] == null)
            {
                return View();
            }
            else
            {
                if ((int)Session["SID"] == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if ((int)Session["SID"] == 2)
                {
                    return RedirectToAction("Index", "Manager");
                }
                else if ((int)Session["SID"] == 3)
                {
                    return RedirectToAction("Index", "Salesman");
                }
                else if ((int)Session["SID"] == 4)
                {
                    return RedirectToAction("Index", "Deliveryman");
                }
                else if ((int)Session["SID"] == 5)
                {
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    TempData["Error"] = "Restricted!";
                    Session.Clear();
                    Session.Abandon();
                    return Content("You are a restricted User. Please Contact with the authority " +
                        "<a href=\"/Home/Index/\"><p>Goto Homepage</p></a>");
                }
            }
        }

        [HttpPost]
        public ActionResult Index(log_in login)
        {
            BusinessToolDBEntities context = new BusinessToolDBEntities();

            var userFromDB = context.log_in.Where(x => x.LID.Equals(login.LID) && x.PASS.Equals(login.PASS)).FirstOrDefault();



            if (userFromDB != null)
            {
                Session["LID"] = userFromDB.LID;
                Session["SID"] = userFromDB.SID;

                if ((int)Session["SID"] == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if ((int)Session["SID"] == 2)
                {
                    return RedirectToAction("Index", "Manager");
                }
                else if ((int)Session["SID"] == 3)
                {
                    return RedirectToAction("Index", "Salesman");
                }
                else if ((int)Session["SID"] == 4)
                {
                    return RedirectToAction("Index", "Deliveryman");
                }
                else if ((int)Session["SID"] == 5)
                {
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    TempData["error"] = "Restricted!";
                    return RedirectToAction("Index", "Login");
                }

            }
            else
            {
                TempData["error"] = "Invalid Login!";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
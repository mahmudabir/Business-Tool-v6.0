using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        [HttpGet]
        public ActionResult Index()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
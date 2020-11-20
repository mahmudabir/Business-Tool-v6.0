using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductManage()
        {
            return View();
        }
        public ActionResult OrderManage()
        {
            return View();
        }
        public ActionResult Approve()
        {
            return View();
        }
    }
}
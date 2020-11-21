using BTv6.Repositories.CommonRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class DeliverymanController : Controller
    {
        // GET: Deliveryman
        OrderRepository orderrepo = new OrderRepository();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DeliveryRecords()
        {
            if (Session["SID"] != null)
            {
                ViewData["confirmed"] = orderrepo.GetAcceptedList((string)Session["LID"]);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult PendingDeliveryList()
        {
            if (Session["SID"] != null)
            {
                ViewData["order"] = orderrepo.GetPendingDeliveryList((string)Session["LID"]);
                return View();
        }
            else
            {
                return RedirectToAction("Index", "Login");
            }
}
    }
}
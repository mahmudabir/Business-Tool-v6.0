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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DeliveryRecords()
        {
            return View();
        }
        public ActionResult PendingDeliveryList()
        {
            return View();
        }
    }
}
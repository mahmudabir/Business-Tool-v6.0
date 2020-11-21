using BTv6.Models;
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
        ProductRepository prodrepo = new ProductRepository();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DeliveryRecords()
        {
            if (Session["SID"] != null)
            {
                if (this.CheckDeliveryman((string)Session["SID"]))
                {

                    ViewData["confirmed"] = orderrepo.GetAcceptedList((string)Session["LID"]);
                    return View();
                }
                else
                {

                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet]
        public ActionResult PendingDeliveryList(string id)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckDeliveryman((string)Session["SID"]))
                {
                    ViewData["order"] = orderrepo.GetPendingDeliveryList((string)Session["LID"]);
                return View();
                 }
                else
                {

                    return RedirectToAction("Index", "Login");
                }
        }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpGet]

        public ActionResult Accepted(int id)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckDeliveryman((string)Session["SID"]))
                {
                    order od = new order();
                    od = orderrepo.GetOrderByID(id);
                    od.stat = "2";
                    orderrepo.Update(od);
                    return RedirectToAction("PendingDeliveryList");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]

        public ActionResult Returned(int id)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckDeliveryman((string)Session["SID"]))
                {
                    order od = new order();
                    product prod = new product();
                    od = orderrepo.GetOrderByID(id);

                    string prodId = od.prodid;
                    prod = prodrepo.GetProductByID(prodId);

                    int totalQuant = prod.QUANTITY + od.quant;
                    prodrepo.UpdateQuantityById(prodId, totalQuant);
                    orderrepo.DeleteOrderByID(id);
                    return RedirectToAction("PendingDeliveryList");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [NonAction]

        public bool CheckDeliveryman(string SID)
        {
            if (SID == "4")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    

}
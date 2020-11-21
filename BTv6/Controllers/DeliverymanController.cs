﻿using BTv6.Models;
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
        [HttpGet]
        public ActionResult PendingDeliveryList(string id)
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


        [HttpGet]

        public ActionResult Accepted(int id)
        {
            if (Session["SID"] != null)
            {

                order od = new order();
                od=orderrepo.GetOrderByID(id);
                od.stat = "0";
                orderrepo.Update(od);
                return RedirectToAction("PendingDeliveryList");
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

                order od = new order();
                od = orderrepo.GetOrderByID(id);
                od.stat = "1";
                orderrepo.Update(od);
                return RedirectToAction("PendingDeliveryList");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }

    

}
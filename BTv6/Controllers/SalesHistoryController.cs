using System;
using BTv6.Models;
using BTv6.Repositories.CommonRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class SalesHistoryController : Controller
    {
        // GET: SalesHistory
        public ActionResult Index()
        {
            if(Session["SID"] !=null)

            {
                SalesRepository sales = new SalesRepository();
                var salesList = sales.GetAll();
                return View(salesList);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
    }
}
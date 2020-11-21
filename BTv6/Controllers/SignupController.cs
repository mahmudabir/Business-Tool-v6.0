using BTv6.Models;
using BTv6.Repositories.CommonRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class SignupController : Controller
    {
        SignupRepository signupRepository = new SignupRepository();
        // GET: Signup
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(customer c, string password, string confirmpassword)
        {
            customer customerToInsert = new customer();
            log_in loginToIntert = new log_in();

            customerToInsert = c;
            customerToInsert.reg_date = DateTime.Now;
            customerToInsert.status = 2;

            loginToIntert.LID = c.cusid;
            loginToIntert.SID = 0;
            if (password == confirmpassword)
            {
                loginToIntert.PASS = password;
            }
            else
            {
                TempData["Error"] = "Passworrd does not match!";
                TempData["cusid"] = c.cusid;
                TempData["name"] = c.name;
                TempData["design"] = c.design;
                TempData["email"] = c.email;
                TempData["mobile"] = c.mobile;
                return RedirectToAction("Index", "Signup");
            }


            var available = signupRepository.CheckUser(c);

            if (!available)
            {
                LoginRepository loginRepository = new LoginRepository();
                loginRepository.Insert(loginToIntert);


                signupRepository.Insert(customerToInsert);

                Session.Clear();
                Session.Abandon();

                TempData["success"] = "You can Login after admin approval";

                return RedirectToAction("Index", "Signup");
            }
            else
            {
                TempData["Error"] = "Username already taken";
                TempData["cusid"] = c.cusid;
                TempData["name"] = c.name;
                TempData["design"] = c.design;
                TempData["email"] = c.email;
                TempData["mobile"] = c.mobile;

                return RedirectToAction("Index", "Signup");
            }
        }
    }
}
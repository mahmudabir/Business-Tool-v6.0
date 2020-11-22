using BTv6.Models;
using BTv6.Repositories.AdminRepositories;
using BTv6.Repositories.CommonRepositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class SignupController : Controller
    {
        private SignupRepository signupRepository = new SignupRepository();

        // GET: Signup
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(customer c, string password, string confirmpassword)
        {
            if (password == "" || confirmpassword == "")
            {
                TempData["Error"] = "Password field is required.";

                return View();
            }
            else
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
                    if (ModelState.IsValid)
                    {
                        LoginRepository loginRepository = new LoginRepository();
                        CustomerRepository customerRepository = new CustomerRepository();
                        EmployeeRepository employeeRepository = new EmployeeRepository();

                        var loginFromDB = loginRepository.GetAll().Where(x => x.LID == loginToIntert.LID).Count();

                        var customerEmailFromDB = customerRepository.GetAll().Where(x => x.email == customerToInsert.email).Count();

                        var employeeEmailFromDB = employeeRepository.GetAll().Where(x => x.E_MAIL == customerToInsert.email).Count();

                        if (loginFromDB <= 0)
                        {
                            bool check = true;

                            if (employeeEmailFromDB > 0)
                            {
                                check = false;
                            }
                            else
                            {
                            }

                            if (customerEmailFromDB > 0)
                            {
                                check = false;
                            }
                            else
                            {
                            }

                            if (check)
                            {
                                loginRepository.Insert(loginToIntert);

                                signupRepository.Insert(customerToInsert);

                                Session.Clear();
                                Session.Abandon();

                                TempData["success"] = "Success! Wait for admin approval";

                                return RedirectToAction("Index", "Signup");
                            }
                            else
                            {
                                TempData["Error"] = "Email already taken";
                                TempData["cusid"] = c.cusid;
                                TempData["name"] = c.name;
                                TempData["design"] = c.design;
                                TempData["email"] = c.email;
                                TempData["mobile"] = c.mobile;
                                return View();
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Username already taken";
                            TempData["cusid"] = c.cusid;
                            TempData["name"] = c.name;
                            TempData["design"] = c.design;
                            TempData["email"] = c.email;
                            TempData["mobile"] = c.mobile;
                            return View();
                        }
                    }
                    else
                    {
                        TempData["cusid"] = c.cusid;
                        TempData["name"] = c.name;
                        TempData["design"] = c.design;
                        TempData["email"] = c.email;
                        TempData["mobile"] = c.mobile;
                        return View();
                    }
                }
                else
                {
                    TempData["Error"] = "Username already taken";
                    TempData["cusid"] = c.cusid;
                    TempData["name"] = c.name;
                    TempData["design"] = c.design;
                    TempData["email"] = c.email;
                    TempData["mobile"] = c.mobile;
                    return View();
                }
            }
        }
    }
}
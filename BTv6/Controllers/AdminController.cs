using BTv6.Models;
using BTv6.Repositories.AdminRepositories;
using BTv6.Repositories.CommonRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        //Employee Management (Validation Needed)
        [HttpGet]
        public ActionResult Index()
        {
            if ((int)Session["SID"] == 1)
            {
                return View();
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult EmployeeManagement(employee employee)
        {
            if ((int)Session["SID"] == 1)
            {
                EmployeeRepository employees = new EmployeeRepository();
                var employeesList = employees.GetAll();

                return View("EmployeeManagement/Index", employeesList);
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost, ActionName("EmployeeManagement")]
        public ActionResult PostEmployeeManagement()
        {
            if ((int)Session["SID"] == 1)
            {
                if (Request["CREATE"] != null)
                {
                    return RedirectToAction("CreateEmployee");
                }

                else
                {
                    return RedirectToAction("EmployeeManagement");
                }

            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            if ((int)Session["SID"] == 1)
            {
                StatusRepository status = new StatusRepository();
                ViewData["design"] = status.GetAll();

                return View("EmployeeManagement/Create/Index");
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult CreateEmployee(employee employee)
        {
            if ((int)Session["SID"] == 1)
            {
                EmployeeRepository employees = new EmployeeRepository();
                LoginRepository logins = new LoginRepository();
                Profile_imagesRepository profile_Images = new Profile_imagesRepository();

                log_in l = new log_in();
                l.LID = (string)employee.EmpID;
                l.SID = (int)employee.DID;
                l.PASS = "12345";

                profile_images images = new profile_images();
                images.UID = (string)employee.EmpID;
                images.IMAGE = "~/Assets/image/profile/default.png";

                var av = employees.CheckUser(employee);

                if (!av)
                {
                    logins.InsertByObj(l);

                    employee.ADDED_BY = (string)Session["LID"];
                    employee.JOIN_DATE = DateTime.Now;
                    employees.InsertByObj(employee);

                    profile_Images.InsertByObj(images);

                    return RedirectToAction("EmployeeManagement");
                }

                else
                {
                    return RedirectToAction("CreateEmployee");
                }

            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult UpdateEmployee(string id)
        {
            if ((int)Session["SID"] == 1)
            {
                EmployeeRepository employees = new EmployeeRepository();
                StatusRepository status = new StatusRepository();
                ViewData["design"] = status.GetAll();
                var employee = employees.Get(id);

                return View("EmployeeManagement/Update/Index", employee);
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult UpdateEmployee(employee employee)
        {
            if ((int)Session["SID"] == 1)
            {
                EmployeeRepository employees = new EmployeeRepository();
                BusinessToolDBEntities context = new BusinessToolDBEntities();

                var empLOG = context.log_in.Where(x => x.LID == (string)employee.EmpID).FirstOrDefault();
                empLOG.SID = (int)employee.DID;
                context.Entry(empLOG).State = EntityState.Modified;
                context.SaveChanges();

                employees.Update(employee);

                if (Session["LID"].Equals((string)employee.EmpID) || (string)Session["LID"] == (string)employee.EmpID)
                {
                    return RedirectToAction("Index", "Logout");
                }

                else
                {
                    return RedirectToAction("EmployeeManagement");
                }
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult RestrictEmployeeLogin(string id)
        {
            if ((int)Session["SID"] == 1)
            {
                BusinessToolDBEntities context = new BusinessToolDBEntities();

                var empLOG = context.log_in.Where(x => x.LID == (string)id).FirstOrDefault();
                empLOG.SID = 0;
                context.Entry(empLOG).State = EntityState.Modified;
                context.SaveChanges();

                var emp = context.employees.Where(x => x.EmpID == (string)id).FirstOrDefault();
                emp.DID = 0;
                context.Entry(emp).State = EntityState.Modified;
                context.SaveChanges();

                if (Session["LID"].Equals((string)id) || (string)Session["LID"] == (string)id)
                {
                    return RedirectToAction("Index", "Logout");
                }

                else
                {
                    return RedirectToAction("EmployeeManagement");
                }
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult AllowEmployeeLogin(string id)
        {
            if ((int)Session["SID"] == 1)
            {
                EmployeeRepository employees = new EmployeeRepository();
                StatusRepository status = new StatusRepository();
                ViewData["design"] = status.GetAll();
                var employee = employees.Get(id);

                return View("EmployeeManagement/LoginAllow/Index", employee);
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult AllowEmployeeLogin(employee employee)
        {
            if ((int)Session["SID"] == 1)
            {
                EmployeeRepository employees = new EmployeeRepository();
                BusinessToolDBEntities context = new BusinessToolDBEntities();

                var empLOG = context.log_in.Where(x => x.LID == (string)employee.EmpID).FirstOrDefault();
                empLOG.SID = (int)employee.DID;
                context.Entry(empLOG).State = EntityState.Modified;
                context.SaveChanges();

                employees.Update(employee);

                if (Session["LID"].Equals((string)employee.EmpID) || (string)Session["LID"] == (string)employee.EmpID)
                {
                    return RedirectToAction("Index", "Logout");
                }

                else
                {
                    return RedirectToAction("EmployeeManagement");
                }
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        //Product Management
        [HttpGet]
        public ActionResult ProductManagement(product product)
        {
            if ((int)Session["SID"] == 1)
            {
                ProductRepository products = new ProductRepository();
                var productsList = products.GetAll();

                return View("ProductManagement/Index", productsList);

            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult UpdateProduct(string id)
        {
            if ((int)Session["SID"] == 1)
            {
                ProductRepository products = new ProductRepository();
                var prod = products.Get(id);

                return View("ProductManagement/Update/Index", prod);
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult UpdateProduct(product product)
        {
            if ((int)Session["SID"] == 1)
            {
                ProductRepository products = new ProductRepository();
                product.MOD_BY = (string)Session["LID"];
                product.Add_PDate = DateTime.Now;
                products.Update(product);

                return RedirectToAction("ProductManagement");
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult RestrictProdutSell(string id)
        {
            if ((int)Session["SID"] == 1)
            {
                BusinessToolDBEntities context = new BusinessToolDBEntities();

                var prod = context.products.Where(x => x.PID == (string)id).FirstOrDefault();
                prod.AVAILABILITY = "UNAVAILABLE";
                context.Entry(prod).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("ProductManagement");
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult AllowProdutSell(string id)
        {
            if ((int)Session["SID"] == 1)
            {
                BusinessToolDBEntities context = new BusinessToolDBEntities();

                var prod = context.products.Where(x => x.PID == (string)id).FirstOrDefault();
                prod.AVAILABILITY = "AVAILABLE";
                context.Entry(prod).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("ProductManagement");
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
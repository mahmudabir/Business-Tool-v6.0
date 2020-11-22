using BTv6.Models;
using BTv6.Repositories.AdminRepositories;
using BTv6.Repositories.CommonRepositories;
using BTv6.Repositories.CustomerRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            if(Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
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
        }

        [HttpGet]
        public ActionResult EmployeeManagement(employee employee)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
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
        }

        [HttpPost, ActionName("EmployeeManagement")]
        public ActionResult PostEmployeeManagement()
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
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
        }

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
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
        }

        [HttpPost]
        public ActionResult CreateEmployee(employee employee)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    if (!ModelState.IsValid)
                    {
                        StatusRepository status = new StatusRepository();
                        ViewData["design"] = status.GetAll();

                        return View("EmployeeManagement/Create/Index");
                    }

                    else
                    {
                        EmployeeRepository check = new EmployeeRepository();
                        var checkuser = check.GetByID(employee.EmpID);

                        if(checkuser == null)
                        {
                            TempData["err"] = "User Exists";
                            return RedirectToAction("CreateEmployee");
                        }
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

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }  
        }

        [HttpGet]
        public ActionResult UpdateEmployee(string id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    EmployeeRepository employees = new EmployeeRepository();
                    StatusRepository status = new StatusRepository();
                    ViewData["design"] = status.GetAll();
                    var employee = employees.Get(id);

                    if(employee == null)
                    {
                        return RedirectToAction("EmployeeManagement");
                    }

                    else
                    {
                        return View("EmployeeManagement/Update/Index", employee);
                    }                   
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateEmployee(employee employee, string id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {

                    if (!ModelState.IsValid)
                    {
                        EmployeeRepository employees = new EmployeeRepository();
                        StatusRepository status = new StatusRepository();
                        ViewData["design"] = status.GetAll();
                        var employeep = employees.Get(id);

                        if (employeep == null)
                        {
                            return RedirectToAction("EmployeeManagement");
                        }

                        else
                        {
                            return View("EmployeeManagement/Update/Index", employeep);
                        }
                    }

                    else
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
                    
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }   
        }

        [HttpGet]
        public ActionResult RestrictEmployeeLogin(string id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    EmployeeRepository ck = new EmployeeRepository();
                    var check = ck.GetByID(id);

                    if(check == null)
                    {
                        return RedirectToAction("EmployeeManagement");
                    }

                    else
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
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }   
        }

        [HttpGet]
        public ActionResult AllowEmployeeLogin(string id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    EmployeeRepository ck = new EmployeeRepository();
                    var check = ck.GetByID(id);

                    if (check == null)
                    {
                        return RedirectToAction("EmployeeManagement");
                    }
                    else
                    {
                        LoginRepository log = new LoginRepository();
                        var user = log.GetByID(id);

                        if(user.SID == 0)
                        {
                            EmployeeRepository employees = new EmployeeRepository();
                            StatusRepository status = new StatusRepository();
                            ViewData["design"] = status.GetAll();
                            var employee = employees.Get(id);

                            return View("EmployeeManagement/LoginAllow/Index", employee);
                        }

                        else
                        {
                            return RedirectToAction("EmployeeManagement");
                        }                     
                    }                  
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpPost]
        public ActionResult AllowEmployeeLogin(employee employee)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
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
        }

        //Product Management
        [HttpGet]
        public ActionResult ProductManagement(product product)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
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
        }

        [HttpPost, ActionName("ProductManagement")]
        public ActionResult PostProductManagement()
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    if (Request["CREATE"] != null)
                    {
                        return RedirectToAction("CreateProduct");
                    }

                    else
                    {
                        return RedirectToAction("ProductManagement");
                    }

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    return View("ProductManagement/Create/Index");
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpPost]
        public ActionResult CreateProduct(product product, HttpPostedFileBase avatar)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    ProductRepository products = new ProductRepository();

                    var av = products.CheckProduct(product);

                    if (!av)
                    {
                        product.MOD_BY = (string)Session["LID"];
                        product.Add_PDate = DateTime.Now;

                        if (product.QUANTITY > 0)
                        {
                            product.AVAILABILITY = "AVAILABLE";
                        }

                        else
                        {
                            product.AVAILABILITY = "UNAVAILABLE";
                        }

                        string path = HttpContext.Server.MapPath("~/Assets/image/product");

                        HttpPostedFileBase file = Request.Files["avatar"];

                        if (file != null)
                        {
                            if (Path.GetFileName(file.FileName).Length > 0)
                            {
                                string fullPath = Path.Combine(path, Path.GetFileName(file.FileName));
                                avatar.SaveAs(fullPath);
                                product.P_IMG = "~/Assets/image/product/" + Path.GetFileName(file.FileName);
                            }

                            else
                            {
                                product.P_IMG = "~/Assets/image/product/default.png";
                            }

                        }

                        else
                        {
                            product.P_IMG = "~/Assets/image/product/default.png";
                        }

                        products.InsertByObj(product);


                        return RedirectToAction("ProductManagement");
                    }

                    else
                    {
                        return RedirectToAction("CreateProduct");
                    }

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult UpdateProduct(string id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
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
        }

        [HttpPost]
        public ActionResult UpdateProduct(product product, HttpPostedFileBase avatar)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    ProductRepository products = new ProductRepository();
                    product.MOD_BY = (string)Session["LID"];
                    product.Add_PDate = DateTime.Now;

                    if (product.QUANTITY > 0)
                    {
                        product.AVAILABILITY = "AVAILABLE";
                    }

                    else
                    {
                        product.AVAILABILITY = "UNAVAILABLE";
                    }

                    string path = HttpContext.Server.MapPath("~/Assets/image/product");

                    HttpPostedFileBase file = Request.Files["avatar"];

                    if (file != null)
                    {
                        ProductRepository prodIMG = new ProductRepository();
                        var img = prodIMG.GetProductByID(product.PID);
                        string image = (string)img.P_IMG;

                        if (Path.GetFileName(file.FileName).Length > 0)
                        {
                            string fullPath = Path.Combine(path, Path.GetFileName(file.FileName));

                            if (image != "~/Assets/image/product/default.png")
                            {
                                System.IO.File.Delete(Server.MapPath(image));
                            }

                            avatar.SaveAs(fullPath);
                            product.P_IMG = "~/Assets/image/product/" + Path.GetFileName(file.FileName);
                        }

                        else
                        {
                            product.P_IMG = image;
                        }
                    }

                    else
                    {
                        ProductRepository prodIMG = new ProductRepository();
                        var img = prodIMG.GetProductByID(product.PID);
                        string image = (string)img.P_IMG;

                        product.P_IMG = image;
                    }

                    products.Update(product);

                    return RedirectToAction("ProductManagement");
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult RestrictProdutSell(string id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
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

        //Pending Registration Management
        [HttpGet]
        public ActionResult PendingRegistrationManagement(customer customer)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    CustomerRepository customers = new CustomerRepository();
                    var pendingRegistrationList = customers.GetByStatus(2);

                    return View("PendingRegistrationManagement/Index", pendingRegistrationList);

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult AcceptCustomer(string id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    BusinessToolDBEntities context = new BusinessToolDBEntities();

                    var cusLOG = context.log_in.Where(x => x.LID == (string)id).FirstOrDefault();
                    cusLOG.SID = 5;
                    context.Entry(cusLOG).State = EntityState.Modified;
                    context.SaveChanges();

                    var cus = context.customers.Where(x => x.cusid == (string)id).FirstOrDefault();
                    cus.status = 1;
                    context.Entry(cus).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("PendingRegistrationManagement");
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult RejectCustomer(string id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    BusinessToolDBEntities db = new BusinessToolDBEntities();
                    LoginRepository log = new LoginRepository();
                    CustomerRepository cus = new CustomerRepository();

                    cus.DeleteCustomerByID(id);
                    log.DeleteLoginByID(id);

                    return RedirectToAction("PendingRegistrationManagement");
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        //Customer Management
        [HttpGet]
        public ActionResult CustomerManagement(customer customer)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    CustomerRepository customers = new CustomerRepository();
                    var pendingRegistrationList = customers.GetByNotStatus(2);

                    return View("CustomerManagement/Index", pendingRegistrationList);

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult AcceptCustomerLogin(string id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    BusinessToolDBEntities context = new BusinessToolDBEntities();

                    var cusLOG = context.log_in.Where(x => x.LID == (string)id).FirstOrDefault();
                    cusLOG.SID = 5;
                    context.Entry(cusLOG).State = EntityState.Modified;
                    context.SaveChanges();

                    var cus = context.customers.Where(x => x.cusid == (string)id).FirstOrDefault();
                    cus.status = 1;
                    context.Entry(cus).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("CustomerManagement");
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        public ActionResult RejectCustomerLogin(string id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    BusinessToolDBEntities context = new BusinessToolDBEntities();

                    var cusLOG = context.log_in.Where(x => x.LID == (string)id).FirstOrDefault();
                    cusLOG.SID = 0;
                    context.Entry(cusLOG).State = EntityState.Modified;
                    context.SaveChanges();

                    var cus = context.customers.Where(x => x.cusid == (string)id).FirstOrDefault();
                    cus.status = 0;
                    context.Entry(cus).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("CustomerManagement");
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        //Notice Management
        [HttpGet]
        public ActionResult NoticeManagement(notice notice)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    NoticeRepository notices = new NoticeRepository();
                    List<notice> noticeList = notices.GetAll();

                    return View("NoticeManagement/Index", noticeList);

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpPost, ActionName("NoticeManagement")]
        public ActionResult PostNoticeManagement()
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    if (Request["CREATE"] != null)
                    {
                        return RedirectToAction("CreateNotice");
                    }

                    else
                    {
                        return RedirectToAction("NoticeManagement");
                    }

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult CreateNotice()
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    return View("NoticeManagement/Create/Index");
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpPost]
        public ActionResult CreateNotice(notice notice)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    if(!ModelState.IsValid)
                    {
                        return View("NoticeManagement/Create/Index");
                    }

                    else
                    {
                        NoticeRepository notices = new NoticeRepository();

                        notices.Insert(notice);

                        return RedirectToAction("NoticeManagement/Index");                       
                    }           
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult UpdateNotice(int id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    NoticeRepository notices = new NoticeRepository();

                    var notice = notices.GetByID(id);

                    if(notice == null)
                    {
                        return RedirectToAction("NoticeManagement/Index");
                    }

                    else
                    {
                        return View("NoticeManagement/Update/Index", notice);
                    }
                    
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateNotice(notice notice, int id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    if (!ModelState.IsValid)
                    {
                        NoticeRepository notices = new NoticeRepository();

                        var not = notices.GetByID(id);

                        return View("NoticeManagement/Update/Index", not);
                    }
                    
                    else
                    {
                        NoticeRepository notices = new NoticeRepository();

                        notices.Update(notice);

                        return RedirectToAction("NoticeManagement/Index");
                    }
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult DeleteNotice(int id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    NoticeRepository notices = new NoticeRepository();

                    notices.DeleteNoticeByID(id);

                    return RedirectToAction("NoticeManagement/Index");
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        //Customer Complains
        [HttpGet]
        public ActionResult CustomerComplains(complain complain)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    ComplainRepository complains = new ComplainRepository();
                    List<complain> complainList = complains.GetAll();

                    return View("CustomerComplains/Index", complainList);

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult ComplainDetails(int id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    ComplainRepository complains = new ComplainRepository();

                    var complain = complains.GetByID(id);

                    return View("CustomerComplains/ComplainDetails/Index", complain);

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult SalaryChart(employee employee)
        {

            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 1)
                {
                    EmployeeRepository emp = new EmployeeRepository();
                    var high = emp.GetAll().Where(x => x.SAL >= 50000).Count();
                    var low = emp.GetAll().Where(x => x.SAL <= 49999).Count();
                   

                    ViewData["h"] = high;
                    ViewData["l"] = low;

                    return View("Charts/SalaryChart");
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
        public ActionResult EmpTypeChart(employee employee)
        {

            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 1)
                {
                    EmployeeRepository emp = new EmployeeRepository();
                    var admin = emp.GetAll().Where(x => x.DID == 1).Count();
                    var manager = emp.GetAll().Where(x => x.DID == 2).Count();
                    var salesman = emp.GetAll().Where(x => x.DID == 3).Count();
                    var deliveryman = emp.GetAll().Where(x => x.DID == 4).Count();


                    ViewData["a"] = admin;
                    ViewData["m"] = manager;
                    ViewData["s"] = salesman;
                    ViewData["d"] = deliveryman;


                    return View("Charts/EmpTypeChart");
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
        public ActionResult EmployeeReports()
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    return View("EmployeeReports/Index");
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult CustomerReports()
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1)
                {
                    return View("CustomerReports/Index");
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult RegistrationChart(customer customer)
        {

            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 1)
                {
                    CustomerRepository cus = new CustomerRepository();
                    var active = cus.GetAll().Where(x => x.status == 1).Count();
                    var blocked = cus.GetAll().Where(x => x.status == 0).Count();
                    var pending = cus.GetAll().Where(x => x.status == 2).Count();

                    ViewData["a"] = active;
                    ViewData["b"] = blocked;
                    ViewData["p"] = pending;

                    return View("Charts/RegistrationChart");
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
    }
}
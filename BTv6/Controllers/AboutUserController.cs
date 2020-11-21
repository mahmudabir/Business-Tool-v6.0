using System;
using BTv6.Models;
using BTv6.Repositories.AdminRepositories;
using BTv6.Repositories.CommonRepositories;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BTv6.Controllers
{
    public class AboutUserController : Controller
    {
        // GET: AboutUser
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 1)
                {
                    ViewData["a"] = "ADMIN";
                }
                if ((int)Session["SID"] == 2)
                {
                    ViewData["m"] = "MANAGER";
                }
                if ((int)Session["SID"] == 3)
                {
                    ViewData["s"] = "SALESMAN";
                }
                if ((int)Session["SID"] == 4)
                {
                    ViewData["d"] = "DELIVERYMAN";
                }
                if ((int)Session["SID"] != 5)
                {
                    EmployeeRepository employee = new EmployeeRepository();
                    var emp = employee.GetByID((string)Session["LID"]);
                    ViewData["employee"] = emp;
                    Profile_imagesRepository p = new Profile_imagesRepository();
                    var image = p.GetByID((string)Session["LID"]);
                    ViewData["image"] = image;
                    return View(emp);

                }
                else
                {
                    CustomerRepository customer = new CustomerRepository();
                    var cus = customer.GetByID((string)Session["LID"]);
                    ViewData["customer"] = cus;
                    return View(cus);

                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 1)
                {
                    ViewData["a"] = "ADMIN";
                }
                if ((int)Session["SID"] == 2)
                {
                    ViewData["m"] = "MANAGER";
                }
                if ((int)Session["SID"] == 3)
                {
                    ViewData["s"] = "SALESMAN";
                }
                if ((int)Session["SID"] == 4)
                {
                    ViewData["d"] = "DELIVERYMAN";
                }

                if ((int)Session["SID"] != 5)
                {
                    EmployeeRepository employee = new EmployeeRepository();
                    var emp = employee.GetByID((string)Session["LID"]);
                    ViewData["employee"] = emp;
                    return View(emp);

                }
                else
                {
                    CustomerRepository customer = new CustomerRepository();
                    var cus = customer.GetByID((string)Session["LID"]);
                    ViewData["customer"] = cus;
                    return View(cus);

                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult EditProfile(HttpPostedFileBase avatar,string EmpID, string name, string mobile, string email)
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] != 5)
                {
                    EmployeeRepository employee = new EmployeeRepository();
                    var LID = (string)Session["LID"];

                    employee empToUpdate = new employee();

                    var empFromDB = employee.GetByID(EmpID);


                    empToUpdate = empFromDB;
                    empToUpdate.E_NAME = name;
                    empToUpdate.E_MOB = mobile;
                    empToUpdate.E_MAIL = email;
                    employee.UpdateUser(empToUpdate);

                    string path = Server.MapPath("~/Assets/image/profile");
                    string fileName = Path.GetFileName(avatar.FileName);
                    string fullPath = Path.Combine(path, fileName);
                    avatar.SaveAs(fullPath);
                    profile_images images = new profile_images();
                    images.UID = (string)Session["LID"];
                    images.IMAGE = "~/Assets/image/profile/" + fileName;
                    Profile_imagesRepository profile_Images = new Profile_imagesRepository();
                    var img = profile_Images.GetByID((string)Session["LID"]);
                    string image = (string)img.IMAGE;
                    if (image != "~/Assets/image/profile/default.png")
                    {
                        System.IO.File.Delete(Server.MapPath(image));
                    }

                    Profile_imagesRepository Update_profile_Images = new Profile_imagesRepository();
                    Update_profile_Images.UpdateImage(images);


                }
                else
                {
                    CustomerRepository customer = new CustomerRepository();
                    var LID = (String)Session["LID"];
                    customer cusToUpdate = new customer();

                    var cusFromDB = customer.GetByID(EmpID);

                    cusToUpdate = cusFromDB;
                    cusToUpdate.name = name;
                    cusToUpdate.mobile = mobile;
                    cusToUpdate.email = email;
                    customer.UpdateUser(cusToUpdate);
                }
                
                return RedirectToAction("Index", "AboutUser");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}
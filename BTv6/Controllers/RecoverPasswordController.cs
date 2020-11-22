using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BTv6.Models;
using BTv6.Repositories.CommonRepositories;
using BTv6.Repositories.AdminRepositories;

namespace BTv6.Controllers
{
    public class RecoverPasswordController : Controller
    {
        // GET: RecoverPassword

        

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string emailFor = "ResetPassword")
        {
            var verifyUrl = "/RecoverPassword/" + emailFor + "/";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("businesstoolsv5@gmail.com", "BusinessTools");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "business@tools";
            string subject = "Reset Password";

            string body = "Hi,<br/><br/>We got Request for Reset your password.Please Click on below link" +
                "<br/><br/><a href=" + link + ">Reset Password Link</a>";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string EmailID)
        {
        
            using (BusinessToolDBEntities dc = new BusinessToolDBEntities())
            {
                //employe info
                var account = dc.employees.Where(a => a.E_MAIL == EmailID).FirstOrDefault();
                //customer info
                var account12 = dc.customers.Where(a => a.email == EmailID).FirstOrDefault(); 
                
                
                if (account12 != null)
                {
                    //login info
                    var userFromDB12 = dc.log_in.Where(a => a.LID == account12.cusid).FirstOrDefault();
                    if (userFromDB12.SID != 5)
                    {
                        if (account != null)
                        {
                            //Send Mail
                            
                            Session["email"] = account.E_MAIL;
                            //send verification mail employee
                            SendVerificationLinkEmail(account.E_MAIL, "ResetPassword");

                        }
                        else
                        {
                            TempData["message"] = "Account Not Found";
                            return View();
                        }
                    }
                    else
                    {
                        var account1 = dc.customers.Where(a => a.email == EmailID).FirstOrDefault();
                        if (account1 != null)
                        {
                            Session["email"] = account1.email;
                            //send mail customer
                            SendVerificationLinkEmail(account1.email, "ResetPassword");
                        }
                        else
                        {
                            TempData["message"] = "Account Not Found";
                            return View();
                        }
                    }
                }
                else
                {
                    TempData["message"] = "Account Not Found";
                    return View();
                }
                
                
            }
            return RedirectToAction("Index","Login");
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            if (Session["email"]!=null)
            {
                ViewData["email"] = Session["email"];
                return View();
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
            
        }
        [HttpPost]
        public ActionResult ResetPassword(string EmailID)
        {
            using (BusinessToolDBEntities dc = new BusinessToolDBEntities())
            {
                var v = dc.employees.Where(a => a.E_MAIL == EmailID).FirstOrDefault();
                var account12 = dc.customers.Where(a => a.email == EmailID).FirstOrDefault();
                var userFromDB12 = dc.log_in.Where(a => a.LID == account12.cusid).FirstOrDefault();
                if (userFromDB12.SID != 5)
                {
                    if (v.E_MAIL == Request["EmailID"])
                    {
                        if (Request["newpass"] == Request["connewpass"])
                        {
                            var userFromDB = dc.log_in.Where(a => a.LID == v.EmpID).FirstOrDefault();
                            userFromDB.PASS = Request["connewpass"];
                            dc.Entry(userFromDB).State = EntityState.Modified;
                            dc.SaveChanges();
                            Session.Clear();
                        }
                        else
                        {
                            TempData["error"] = "Password Doesn't Match";
                            return RedirectToAction("ResetPassword", "RecoverPassword");
                        }
                        TempData["suc"] = "Password Recovered";
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    var v1 = dc.customers.Where(a => a.email == EmailID).FirstOrDefault();
                    if (v1.email == Request["EmailID"])
                    {
                        if (Request["newpass"] == Request["connewpass"])
                        {
                            var userFromDB = dc.log_in.Where(a => a.LID == v1.cusid).FirstOrDefault();
                            userFromDB.PASS = Request["connewpass"];
                            dc.Entry(userFromDB).State = EntityState.Modified;
                            dc.SaveChanges();
                            Session.Clear();
                        }
                        else
                        {
                            TempData["error"] = "Password Doesn't Match";
                            return RedirectToAction("ResetPassword", "RecoverPassword");
                        }
                        TempData["suc"] = "Password Recovered";
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                
                
            }
            
        }

    }
}
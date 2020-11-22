using BTv6.Models;
using BTv6.Repositories.AdminRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class NoticeController : Controller
    {
        // GET: Notice
        [HttpGet]
        public ActionResult Index(notice notice)
        {
            if(Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            
            else
            {
                if ((int)Session["SID"] == 1 || (int)Session["SID"] == 2 || (int)Session["SID"] == 3 || (int)Session["SID"] == 4)
                {
                    NoticeRepository notices = new NoticeRepository();
                    List<notice> noticeList = notices.GetAll();

                    return View(noticeList);

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet]
        public ActionResult DetailsNotice(int id)
        {
            if (Session["LID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if ((int)Session["SID"] == 1 || (int)Session["SID"] == 2 || (int)Session["SID"] == 3 || (int)Session["SID"] == 4)
                {
                    NoticeRepository notices = new NoticeRepository();

                    var notice = notices.GetByID(id);

                    if(notice != null)
                    {
                        return View("Details/Index", notice);
                    }

                    
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }      
        }
    }
}
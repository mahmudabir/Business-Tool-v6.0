using BTv6.Models;
using BTv6.Repositories.CommonRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class ChatController : Controller
    {
        ChatRepository chatrepo = new ChatRepository();

        public ActionResult Index()
        {

            if (Session["SID"] != null)
            {
                TempData["userId"] = (string)Session["LID"];
                ViewData["receiver"] = chatrepo.GetAllByReceiverId((string)Session["LID"]);
                ViewData["sender"] = chatrepo.GetAllBySenderId((string)Session["LID"]);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult Index(chat ct)
        {
            if (Session["SID"] != null)
            {
                chat chatToInsert = new chat();
                chatToInsert.RECEIVER = ct.RECEIVER;
                chatToInsert.SENDER = (string)Session["LID"];
                chatToInsert.SUB = ct.SUB;
                chatToInsert.TEXT = ct.TEXT;
                chatToInsert.STATUS = 0;
                chatrepo.Insert(chatToInsert);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
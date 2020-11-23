using BTv6.Models;
using BTv6.Repositories.AdminRepositories;
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
        EmployeeRepository emprepo = new EmployeeRepository();

         [HttpGet]
        public ActionResult Index(string id)
        {

            if (Session["SID"] != null)
            {
                ViewData["receiver"] = chatrepo.GetAllByReceiverId((string)Session["LID"]);
                TempData["ncount"] = chatrepo.GetAllByReceiverId((string)Session["LID"]).Count();
                TempData["ocount"] = chatrepo.GetAllSeenById((string)Session["LID"]).Count();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpGet]
        public ActionResult showmessage(int id)
        {
            if (Session["SID"] != null)
            {

                chat ct = new chat();
                ct = chatrepo.GetChatByID(id);
                ct.STATUS = 1;
                chatrepo.Update(ct);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }

        [HttpGet]
        public ActionResult unseenmessage(int id)
        {
            if (Session["SID"] != null)
            {

                chat ct = new chat();
                ct = chatrepo.GetChatByID(id);
                ct.STATUS = 0;
                chatrepo.Update(ct);

                return RedirectToAction("Index");
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

                if (Request["sendmessage"] != null)
                {
                    return View("send");
                }

                else if (Request["seenmessage"] != null)
                {

                    ViewData["receiver"] = chatrepo.GetAllSeenById((string)Session["LID"]);
                    return View("seen");
                }

                else if (Request["unseenmessage"] != null)
                {
                    return RedirectToAction("Index");
                }

                else if (Request["SEND"] != null)
                {
                    chat chatToInsert = new chat();
                    chatToInsert.RECEIVER = ct.RECEIVER;
                    var sid = emprepo.GetByID(chatToInsert.RECEIVER);

                    if(emprepo.GetByID(chatToInsert.RECEIVER)!=null){
                    if (sid.DID == 1 || sid.DID == 2 || sid.DID == 3 || sid.DID == 4 || sid.DID == 5)
                    {

                        chatToInsert.SENDER = (string)Session["LID"];
                        chatToInsert.SUB = ct.SUB;
                        chatToInsert.TEXT = ct.TEXT;
                        if (chatToInsert.TEXT != null && chatToInsert.SUB != null)
                        {
                            if (chatToInsert.SENDER != chatToInsert.RECEIVER)
                            {
                                chatrepo.Insert(chatToInsert);
                                TempData["message"] = "Message Sent Successfully";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["error"] = "You are not allowed to send message to yourself";
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            TempData["error"] = "Message Can't Send";
                            return RedirectToAction("Index");
                        }
                    }


                    else
                    {
                        TempData["error"] = "Message Can't Send";
                        return RedirectToAction("Index");
                    }
                }
                    else
                    {
                        TempData["error"] = "Message Can't Send";
                        return RedirectToAction("Index");
                    }
                }


                else if (Request["BACK"] != null)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
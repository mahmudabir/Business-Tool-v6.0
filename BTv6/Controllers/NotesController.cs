using BTv6.Models;
using BTv6.Repositories.CommonRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class NotesController : Controller
    {
        // GET: Notes
        NotesRepository noterepo = new NotesRepository();
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["SID"] != null)
            {
                if (this.checkUser((int)Session["SID"])){
                    ViewData["notes"] = noterepo.GetNotice((string)Session["LID"]);
                    return View();
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

        [HttpPost]
        public ActionResult Index(note nt, string id)
        {


            if (Request["PUSH"] != null)
            {

                if (Session["SID"] != null)
                {

                    note noteToInsert = new note();

                    noteToInsert.OwnerID = (string)Session["LID"];
                    noteToInsert.NoteName = (string)nt.NoteName;
                    noteToInsert.Text = (string)nt.Text;

                    TempData["names"] = noteToInsert.NoteName;
                    TempData["texts"] = noteToInsert.Text;
                    if (noteToInsert.NoteName != null && noteToInsert.Text != null)
                    {
                         noterepo.Insert(noteToInsert);
                         TempData["message"] = "Note is Saved!";
                         return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["error"] = "Fill all the fields";
                        return RedirectToAction("Index");
                    }

                }

                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            else if (Request["Delete"] != null)
            {
                if (Session["SID"] != null)
                {
                    note checkNote = new note();
                    checkNote.NoteName = nt.NoteName;
                    checkNote.Text = nt.Text;

                    if (checkNote.NoteName != null && checkNote.Text != null)
                    {
                        note getNoteId = new note();
                        getNoteId.NoteID = nt.NoteID;
                        noterepo.Delete(getNoteId.NoteID);
                        TempData["message"] = "Delete Successfull";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["error"] = "Can't delete anything";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            else if (Request["SEE"] != null)
            {
                if (Session["SID"] != null)
                {

                    note getNoteId = new note();
                    getNoteId.NoteID = nt.NoteID;
                    
                        var lastnote = noterepo.GetNotice((string)Session["LID"]).LastOrDefault();
                        var firstnote = noterepo.GetNotice((string)Session["LID"]).FirstOrDefault();
                        var NoteIs = noterepo.searchNotice(getNoteId.NoteID);
                        var count = noterepo.GetNotice((string)Session["LID"]).FirstOrDefault();
                        if (getNoteId.NoteID != null && count!=null && NoteIs != null && getNoteId.NoteID >= firstnote.NoteID && getNoteId.NoteID <= lastnote.NoteID)
                        {

                            TempData["names"] = NoteIs.NoteName;
                            TempData["texts"] = NoteIs.Text;
                            TempData["id"] = getNoteId.NoteID;
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["error"] = "Invalid Search";
                            return RedirectToAction("Index");
                        }
                 }
                    
                
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            else if (Request["Refresh"] != null)
            {
                TempData["names"] = "";
                TempData["texts"] = "";
                TempData["id"] = "";
                return RedirectToAction("Index");
             }

            else if (Request["Update"] != null)
            {
                note noteToUpdate = new note();
                noteToUpdate.NoteID = nt.NoteID;
                noteToUpdate.OwnerID = (string)Session["LID"];
                noteToUpdate.NoteName = (string)nt.NoteName;
                noteToUpdate.Text = (string)nt.Text;
                if (noteToUpdate.NoteID !=null && noteToUpdate.NoteName !=null && noteToUpdate.Text!=null)
                {
                    

                    TempData["names"] = noteToUpdate.NoteName;
                    TempData["texts"] = noteToUpdate.Text;

                    if (Session["SID"] != null)
                    {
                        noterepo.Update(noteToUpdate);
                        TempData["message"] = "Note Successfully Modified";
                        return RedirectToAction("Index");
                    }

                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    TempData["error"] = "Nothing Modified";
                    return RedirectToAction("Index");
                }
            }

            else
            {
                return RedirectToAction("Index");
            }

        }

        public bool checkUser(int SID)
        {
            if (SID == 1 || SID == 2 || SID == 3 || SID == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
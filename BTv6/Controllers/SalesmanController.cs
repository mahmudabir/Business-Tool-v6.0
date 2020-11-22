using BTv6.Models;
using BTv6.Repositories.CommonRepositories;
using BTv6.Repositories.SalesmanRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class SalesmanController : Controller
    {
        // GET: Salesman
        ProductRepository prodrepo = new ProductRepository();
        SalesRepository salerepo = new SalesRepository();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SellProducts()
        {
            if (Session["SID"] != null)
            {
                if (this.checkSalesman((int)Session["SID"]))
                {
                    ViewData["products"] = prodrepo.GetAvailableProduct();
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
        public ActionResult SellProducts(sale sl,string id)
        {
            if (Session["SID"] != null)
            {
                if (this.checkSalesman((int)Session["SID"]))
                {
                    if (Request["SEARCH"] != null)
                    {
                        sale saleId = new sale();
                        saleId.PID = (string)sl.PID;
                        var OnlyProd = prodrepo.Get(saleId.PID);
                        if (saleId.PID != null && OnlyProd != null)
                        {
                            TempData["pid"] = OnlyProd.PID;
                            TempData["pname"] = OnlyProd.P_NAME;
                            TempData["ptype"] = OnlyProd.TYPE;

                            TempData["price"] = OnlyProd.SELL_PRICE;
                            return RedirectToAction("SellProducts");
                        }
                        else
                        {
                            TempData["message"] = "Invalid Search";
                            return RedirectToAction("SellProducts");

                        }

                    }

                    else if (Request["SELL"] != null)
                    {
                        sale saleToInsert = new sale();
                        product prod = new product();
                        saleToInsert.PID = sl.PID;
                        if (saleToInsert.PID != null)
                        {
                            var OnlyProd = prodrepo.Get(saleToInsert.PID);
                            saleToInsert.QUANT = (int)sl.QUANT;
                            saleToInsert.OB_AMMOUNT = (float)(sl.QUANT * OnlyProd.SELL_PRICE);
                            saleToInsert.PROFIT = (float)(saleToInsert.OB_AMMOUNT - sl.QUANT * OnlyProd.BUY_PRICE);
                            saleToInsert.C_NAME = sl.C_NAME;
                            saleToInsert.C_MOB = sl.C_MOB;
                            saleToInsert.SOLD_BY = (string)Session["LID"];
                            saleToInsert.Sell_SDate = DateTime.Now;



                            prod.PID = (string)saleToInsert.PID;
                            prod.P_NAME = (string)OnlyProd.P_NAME;
                            prod.P_IMG = OnlyProd.P_IMG;
                            prod.TYPE = (string)OnlyProd.TYPE;
                            prod.AVAILABILITY = (string)OnlyProd.AVAILABILITY;
                            prod.BUY_PRICE = (int)OnlyProd.BUY_PRICE;
                            prod.SELL_PRICE = (int)OnlyProd.SELL_PRICE;
                            prod.MOD_BY = (string)OnlyProd.MOD_BY;
                            prod.Add_PDate = (DateTime)OnlyProd.Add_PDate;
                            prod.QUANTITY = (int)(OnlyProd.QUANTITY - sl.QUANT);


                            if (saleToInsert.QUANT != null && saleToInsert.QUANT > 0 && saleToInsert.C_NAME != null && saleToInsert.C_MOB != null)
                            {
                                if (prod.QUANTITY >= 0)
                                {
                                    prodrepo.UpdateQuantity(prod, prod.PID);
                                    salerepo.Insert(saleToInsert);

                                    TempData["message"] = "Product sold successfully";
                                    return RedirectToAction("SellProducts");
                                }
                                else
                                {
                                    TempData["pid"] = OnlyProd.PID;
                                    TempData["pname"] = OnlyProd.P_NAME;
                                    TempData["ptype"] = OnlyProd.TYPE;
                                    TempData["QUANT"] = saleToInsert.QUANT;
                                    TempData["C_NAME"] = saleToInsert.C_NAME;
                                    TempData["C_MOB"] = saleToInsert.C_MOB;
                                    TempData["price"] = OnlyProd.SELL_PRICE;
                                    TempData["message"] = "Sorry,This Item is Out of Quantity";
                                    return RedirectToAction("SellProducts");
                                }

                            }


                            else
                            {
                                TempData["pid"] = OnlyProd.PID;
                                TempData["pname"] = OnlyProd.P_NAME;
                                TempData["ptype"] = OnlyProd.TYPE;
                                TempData["QUANT"] = saleToInsert.QUANT;
                                TempData["C_NAME"] = saleToInsert.C_NAME;
                                TempData["C_MOB"] = saleToInsert.C_MOB;
                                TempData["price"] = OnlyProd.SELL_PRICE;
                                TempData["message"] = "Fill All required Fields";
                                return RedirectToAction("SellProducts");
                            }


                        }
                        else
                        {
                            TempData["message"] = "Load data first";
                            return RedirectToAction("SellProducts");
                        }

                    }

                    else if (Request["REFRESH"] != null)
                    {
                        TempData["QUANT"] = "";
                        return RedirectToAction("SellProducts");
                    }
                    else
                    {
                        return RedirectToAction("SellProducts");
                    }
                }
                else
                {
                    return RedirectToAction("Index","Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }



        [HttpGet]
        public ActionResult SaleTypeChart()
        {

            if (Session["SID"] != null)
            {
                if (this.checkSalesman((int)Session["SID"]))
                {
                   

                    var saleCount = salerepo.GetSaleProductByUser((string)Session["LID"]).Count();
                    

                    ViewData["sale"] = saleCount;
                   
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

        [HttpGet]
        public ActionResult NoteChart()
        {

            if (Session["SID"] != null)
            {
                if (this.checkSalesman((int)Session["SID"]))
                {

                    NotesRepository noterepo =new NotesRepository();
                    var AdminNoteCount = noterepo.GetNotice("1").Count();
                    var ManagerNoteCount = noterepo.GetNotice("2").Count();
                    var SalesmanNoteCount = noterepo.GetNotice("3").Count();
                    var DeliverymanNoteCount = noterepo.GetNotice("4").Count();

                    ViewData["anote"] = AdminNoteCount;
                    ViewData["mnote"] = ManagerNoteCount;
                    ViewData["snote"] = SalesmanNoteCount;
                    ViewData["dnote"] = DeliverymanNoteCount;


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


        [NonAction]
        public bool checkSalesman(int id)
        {
            if (id == 3)
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
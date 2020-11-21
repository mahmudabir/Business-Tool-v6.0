using System;
using BTv6.Models;
using System.IO;
using BTv6.Repositories.AdminRepositories;
using BTv6.Repositories.CommonRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BTv6.Controllers
{
    public class ManagerController : Controller
    {
        
    // GET: Manager
    [HttpGet]
        public ActionResult Index()
        {
            if (Session["SID"] !=null)
            {
                if ((int)Session["SID"] == 2)
                {
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
        public ActionResult ProductManage(product product)
        {
            if (Session["SID"] !=null)
            {
                if ((int)Session["SID"] == 2)
                {
                    ProductRepository products = new ProductRepository();
                    var productsList = products.GetAll();

                    return View(productsList);

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
        public ActionResult ProductManage()
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
                {
                    if (Request["CREATE"] != null)
                    {
                        return RedirectToAction("InsertProduct", "Manager");
                    }

                    else
                    {
                        return RedirectToAction("ProductManage", "Manager");
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
        [HttpGet]
        public ActionResult EditProduct(string id)
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
                {
                    ProductRepository products = new ProductRepository();
                    var prod = products.Get(id);

                    return View(prod);
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
        public ActionResult EditProduct(product product, HttpPostedFileBase avatar)
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
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
                    //string fileName = Path.GetFileName(avatar.FileName);
                    HttpPostedFileBase file = Request.Files["avatar"];

                    if (file != null)
                    {
                        ProductRepository prodIMG = new ProductRepository();
                        var img = prodIMG.GetProductByID(product.PID);
                        string image = (string)img.P_IMG;
                        if (image != "~/Assets/image/product/default.png")
                        {
                            System.IO.File.Delete(Server.MapPath(image));
                        }

                        string fullPath = Path.Combine(path, Path.GetFileName(file.FileName));
                        avatar.SaveAs(fullPath);
                        product.P_IMG = "~/Assets/image/product/" + Path.GetFileName(file.FileName);
                    }

                    products.Update(product);

                    return RedirectToAction("ProductManage", "Manager");
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
        public ActionResult InsertProduct()
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
                {
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
        public ActionResult InsertProduct(product product, HttpPostedFileBase avatar)
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
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
                        //string fileName = Path.GetFileName(avatar.FileName);
                        HttpPostedFileBase file = Request.Files["avatar"];

                        if (file != null)
                        {
                            string fullPath = Path.Combine(path, Path.GetFileName(file.FileName));
                            avatar.SaveAs(fullPath);
                            product.P_IMG = "~/Assets/image/product/" + Path.GetFileName(file.FileName);
                        }

                        else
                        {
                            product.P_IMG = "~/Assets/image/product/default.png";
                        }

                        products.InsertByObj(product);


                        return RedirectToAction("ProductManage", "Manager");
                    }


                    {
                        return RedirectToAction("InsertProduct", "Manager");
                    }

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



        /*if((int) Session["SID"]!=null)
            {

            }
            else
            {
                return RedirectToAction("Index", "Login");
        }*/



        [HttpGet]
        public ActionResult OrderManage()
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
                {
                    OrderRepository order = new OrderRepository();
                    var orderFromDB = order.GetAll();
                    return View(orderFromDB);
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
        public ActionResult Approve(order order)
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
                {
                    /*order order1 = new order();*/
                    //OrderRepository order = new OrderRepository();

                    //return View(order.GetPendingOrder("0"));
                    //BusinessToolDBEntities db = new BusinessToolDBEntities();
                    //List<order> list = db.orders.Where(x => x.stat == "0").ToList();
                    OrderRepository orders = new OrderRepository();
                    List<order> list = orders.GetAll().ToList();

                    return View(list);
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
        public ActionResult Approve(order order, int id)
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
                {
                    BusinessToolDBEntities context = new BusinessToolDBEntities();
                    var OrderDB = context.orders.Where(x => x.orderid == id).FirstOrDefault();
                    OrderDB.stat = "1";
                    context.Entry(OrderDB).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("OrderManage", "Manager");
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
        public ActionResult ProductChart(product product)
        {

            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
                {
                    ProductRepository productRepository = new ProductRepository();

                    var mod1 = productRepository.GetAll().Where(x => x.MOD_BY == "1").Count();
                    var mod2 = productRepository.GetAll().Where(x => x.MOD_BY == "2").Count();
                    var mod3 = productRepository.GetAll().Where(x => x.MOD_BY == "3").Count();

                    ViewData["mod1"] = mod1;
                    ViewData["mod2"] = mod2;
                    ViewData["mod3"] = mod3;



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
        public ActionResult OrderPendingChart(order order)
        {

            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
                {
                    OrderRepository orderRepository = new OrderRepository();

                    var pending = orderRepository.GetAll().Where(x => x.stat == "0").Count();
                    var approved = orderRepository.GetAll().Where(x => x.stat == "1").Count();
                    var deliver = orderRepository.GetAll().Where(x => x.stat == "2").Count();

                    ViewData["p"] = pending;
                    ViewData["a"] = approved;
                    ViewData["d"] = deliver;



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
    }
}
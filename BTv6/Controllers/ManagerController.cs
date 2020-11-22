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
                    var prod = products.GetProductByID(id);

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
        public ActionResult EditProduct(product product, string id)
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
                {
                    if (!ModelState.IsValid)
                    {
                        ProductRepository products = new ProductRepository();
                        var prod = products.GetProductByID(id);

                        return View(prod);
                    }

                    else
                    {
                        ProductRepository img = new ProductRepository();
                        var prod = img.GetProductByID(id);
                        ProductRepository products = new ProductRepository();
                        product.MOD_BY = (string)Session["LID"];
                        product.Add_PDate = DateTime.Now;
                        product.P_IMG = prod.P_IMG;

                        if (product.QUANTITY > 0)
                        {
                            product.AVAILABILITY = "AVAILABLE";
                        }

                        else
                        {
                            product.AVAILABILITY = "UNAVAILABLE";
                        }

                        if (product.BUY_PRICE <= product.SELL_PRICE)
                        {
                            products.Update(product);

                            return RedirectToAction("ProductManage", "Manager");
                        }
                        else
                        {
                            TempData["err2"] = "Sell Price Should >= BuyPrice";

                            ProductRepository productsd = new ProductRepository();
                            var prodd = productsd.Get(id);

                            return View(prodd);
                        }
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
        public ActionResult InsertProduct(product product)
        {
            if (Session["SID"] != null)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                
                else
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

                            product.P_IMG = "~/Assets/image/product/default.png";

                            if (product.BUY_PRICE <= product.SELL_PRICE)
                            {
                                products.InsertByObj(product);


                                return RedirectToAction("ProductManage", "Manager");
                            }
                            else
                            {
                                TempData["err2"] = "Sell Price Should >= BuyPrice";

                                return RedirectToAction("InsertProduct");
                            }

                            
                        }

                        else
                        {
                            TempData["err"] = "Product ID Exists";
                            return RedirectToAction("InsertProduct");
                        }

                    }

                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
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
        public ActionResult Approve(int id)
        {
            if (Session["SID"] != null)
            {
                if ((int)Session["SID"] == 2)
                {
                    /*order order1 = new order();
                    OrderRepository order = new OrderRepository();

                    return View(order.GetPendingOrder("0"));
                    BusinessToolDBEntities db = new BusinessToolDBEntities();
                    List<order> list = db.orders.Where(x => x.stat == "0").ToList();*/

                    OrderRepository orders = new OrderRepository();
                    var list = orders.GetOrderByID(id);

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
                    OrderDB.deliveryby = Request["deliveryby"];
                    context.Entry(OrderDB).State = EntityState.Modified;
                    context.SaveChanges();
                    TempData["suc"] = "Order Approved";
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
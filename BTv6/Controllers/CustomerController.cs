using BTv6.Models;
using BTv6.Repositories.CommonRepositories;
using BTv6.Repositories.CustomerRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTv6.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
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
        public ActionResult Complain()
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
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
        public ActionResult Complain(complain cmpln)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    ComplainRepository complainRepository = new ComplainRepository();

                    complain complainToInsert = new complain();

                    complainToInsert.OwnerID = (string)Session["LID"];
                    complainToInsert.sub = (string)cmpln.sub;
                    complainToInsert.Text = (string)cmpln.Text;

                    complainRepository.Insert(complainToInsert);

                    TempData["success"] = "Your complain was submitted!";

                    return RedirectToAction("Index", "Customer");
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
        public ActionResult ConfirmedOrder()
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    OrderRepository orderRepository = new OrderRepository();

                    var confirmedOrderListByUser = orderRepository.GetConfirmedOrderByUser((string)Session["LID"]);

                    return View(confirmedOrderListByUser);
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
        public ActionResult OrderProduct()
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    ProductRepository productRepository = new ProductRepository();

                    var productsToView = productRepository.GetAll();

                    return View(productsToView);
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


        //[HttpPost]
        //public ActionResult OrderProduct(string PID)
        //{
        //    if (Session["SID"] != null)
        //    {
        //        if (this.CheckCustomer((int)Session["SID"]))
        //        {

        //            return RedirectToAction("BuyProduct", "Customer", PID);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Login");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
        //}

        [HttpGet]
        public ActionResult BuyProduct(string id)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    ProductRepository productRepository = new ProductRepository();

                    var productFromDB = productRepository.GetProductByID(id);

                    return View(productFromDB);
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
        public ActionResult BuyProduct(product prdct, int orderQuantity)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    OrderRepository orderRepository = new OrderRepository();
                    ProductRepository productRepository = new ProductRepository();

                    var productFromDB = productRepository.Get(prdct.PID);

                    if ((productFromDB.QUANTITY < prdct.QUANTITY) || (prdct.QUANTITY == 0))
                    {
                        TempData["error"] = "Quantity can not be 0 or exceed available quantity.";
                        return View(prdct.PID);
                    }
                    else
                    {
                        //assining values to the object and then passing for intertin the value
                        order orderToInsert = new order();

                        orderToInsert.prodid = productFromDB.PID;
                        orderToInsert.quant = orderQuantity;
                        orderToInsert.ammout = orderQuantity * prdct.SELL_PRICE;
                        orderToInsert.stat = "0";
                        orderToInsert.ord_date = DateTime.Now;
                        orderToInsert.deliveryby = "1";
                        orderToInsert.orderby = (string)Session["LID"];

                        orderRepository.Insert(orderToInsert);

                        //Update product quantity
                        productFromDB.QUANTITY = productFromDB.QUANTITY - orderQuantity;
                        productRepository.Update(productFromDB);
                        return RedirectToAction("OrderProduct");
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
        public ActionResult PendingOrder(string id)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    OrderRepository orderRepository = new OrderRepository();

                    var pendingOrderListByUser = orderRepository.GetPendingOrderByUser((string)Session["LID"]);

                    return View(pendingOrderListByUser);
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

        //[HttpPost, ActionName("PendingOrder")]
        //public ActionResult PendingOrderPost(string id)
        //{
        //    if (Session["SID"] != null)
        //    {
        //        if (this.CheckCustomer((int)Session["SID"]))
        //        {
        //            OrderRepository orderRepository = new OrderRepository();

        //            var orderFromDB = orderRepository.Get(id);


        //            if (orderFromDB.stat == "0")
        //            {
        //                var quantityToAdd = orderFromDB.quant;

        //                ProductRepository productRepository = new ProductRepository();

        //                var productFromDB = productRepository.Get(orderFromDB.prodid);


        //                orderRepository.Delete(id);

        //                TempData["success"] = "Your Order Canceled Successfully!";
        //                return RedirectToAction("PendingOrder", id);

        //            }
        //            else
        //            {
        //                TempData["error"] = "Your Order Cannot be Cancelled!";
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Login");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
        //}

        [HttpGet]
        public ActionResult RecievedOrder()
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    OrderRepository orderRepository = new OrderRepository();

                    var recievedOrderListByUser = orderRepository.GetRecievedOrderByUser((string)Session["LID"]);

                    return View(recievedOrderListByUser);
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
        public ActionResult EditPendingOrder(int id)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    ProductRepository productRepository = new ProductRepository();
                    OrderRepository orderRepository = new OrderRepository();


                    var orderFromDB = orderRepository.GetOrderByID(id);
                    var productFromDB = productRepository.GetProductByID(orderFromDB.prodid);

                    ViewData["order"] = (order)orderFromDB;
                    ViewData["product"] = (product)productFromDB;

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
        public ActionResult EditPendingOrder(order ordr, product prdct)
        {

            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    OrderRepository orderRepository = new OrderRepository();
                    ProductRepository productRepository = new ProductRepository();

                    var orderFromDB = orderRepository.GetOrderByID(ordr.orderid);
                    var productFromDB = productRepository.GetProductByID(ordr.prodid);

                    if ((productFromDB.QUANTITY < prdct.QUANTITY) || (orderFromDB.orderby != (string)Session["LID"]))
                    {
                        TempData["error"] = "Quantity can not be 0 or exceed available quantity.";
                        return View(ordr.orderid);
                    }
                    else
                    {
                        //assining values to the object and then passing for intertin the value
                        order orderToUpdate = orderFromDB;

                        orderToUpdate.quant = ordr.quant;
                        orderToUpdate.ammout = ordr.quant * prdct.SELL_PRICE;
                        orderToUpdate.deliveryby = "1";
                        orderToUpdate.orderby = (string)Session["LID"];

                        orderRepository.Update(orderToUpdate);

                        //Update product quantity
                        var updatedQuantity = ordr.quant - orderFromDB.quant;

                        product productToUpdate = productFromDB;
                        if (updatedQuantity > 0)
                        {
                            productToUpdate.QUANTITY = productFromDB.QUANTITY + Math.Abs(updatedQuantity);
                            productRepository.Update(productToUpdate);
                            return RedirectToAction("OrderProduct");
                        }
                        else
                        {
                            productToUpdate.QUANTITY = productFromDB.QUANTITY - Math.Abs(updatedQuantity);
                            productRepository.Update(productToUpdate);
                            return RedirectToAction("OrderProduct");
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
        public ActionResult DeletePendingOrder(int id)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    OrderRepository orderRepository = new OrderRepository();

                    var orderFromDB = orderRepository.GetOrderByID(id);

                    if (orderFromDB.orderby == (string)Session["LID"])
                    {
                        if (orderFromDB.stat == "0")
                        {
                            ProductRepository productRepository = new ProductRepository();

                            var productFromDB = productRepository.GetProductByID(orderFromDB.prodid);

                            var productToUpdate = productFromDB;

                            productToUpdate.QUANTITY = orderFromDB.quant + productFromDB.QUANTITY;

                            orderRepository.DeleteOrderByID(id);
                            productRepository.Update(productToUpdate);


                            TempData["success"] = "Your Order Canceled Successfully!";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["error"] = "Your Order Cannot be Cancelled!";
                            return RedirectToAction("Index");
                        }
                    }
                    {
                        TempData["error"] = "Your Order Cannot be Cancelled!";
                        return RedirectToAction("Index");
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

        [NonAction]
        public bool CheckCustomer(int SID)
        {
            if (SID == 5)
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
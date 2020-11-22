using BTv6.Models;
using BTv6.Repositories.CommonRepositories;
using BTv6.Repositories.CustomerRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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

        [HttpPost]
        public ActionResult Index(int id)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    TempData["q"] = id;
                    return RedirectToAction("LastOrderChart", "Customer");
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

                    if (ModelState.IsValid)
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
                        //TempData["error"] = "Your complain was not submitted!";

                        return RedirectToAction("Complain", "Customer");
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


        [HttpGet]
        public ActionResult BuyProduct(string id)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    ProductRepository productRepository = new ProductRepository();

                    var productFromDB = productRepository.GetProductByID(id);

                    if (productFromDB != null)
                    {
                        if (productFromDB.AVAILABILITY == "AVAILABLE" && productFromDB.QUANTITY > 0)
                        {
                            return View(productFromDB);
                        }
                        else
                        {
                            TempData["error"] = "The Product you are searching is not Available.";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["error"] = "The Product you are searching is not Available.";
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
                        if (productFromDB.QUANTITY == orderQuantity)
                        {
                            productFromDB.QUANTITY = productFromDB.QUANTITY - orderQuantity;
                            productFromDB.AVAILABILITY = "UNAVILABLE";

                            productRepository.Update(productFromDB);
                            return RedirectToAction("OrderProduct");
                        }
                        else
                        {
                            productFromDB.QUANTITY = productFromDB.QUANTITY - orderQuantity;

                            productRepository.Update(productFromDB);
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

        [HttpPost, ActionName("RecievedOrder")]
        public ActionResult PostRecievedOrder()
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    return RedirectToAction("RecievedOrder");
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

                    if (orderFromDB != null)
                    {
                        if (orderFromDB.orderby == (string)Session["LID"])
                        {
                            var productFromDB = productRepository.GetProductByID(orderFromDB.prodid);

                            ViewData["order"] = (order)orderFromDB;
                            ViewData["product"] = (product)productFromDB;

                            return View();
                        }
                        else
                        {
                            TempData["error"] = "Sorry, Cannot view the order!";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["error"] = "Sorry, Cannot view the order!";
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

        [HttpPost]
        public ActionResult EditPendingOrder(int orderid, int quant)
        {

            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    OrderRepository orderRepository = new OrderRepository();
                    ProductRepository productRepository = new ProductRepository();

                    var orderFromDB = orderRepository.GetOrderByID(orderid);
                    var productFromDB = productRepository.GetProductByID(orderFromDB.prodid);

                    if (orderFromDB.orderby != (string)Session["LID"])
                    {
                        TempData["error"] = "Sorry, Cannot update the order!";
                        return View(orderid);
                    }
                    else
                    {
                        //Here the logic for updating order & product will reside
                        if ((quant - orderFromDB.quant) > 0)
                        {
                            if (productFromDB.QUANTITY < (quant - orderFromDB.quant))
                            {
                                TempData["error"] = "Quantity you selected is not available now!";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                int productToSubtract = quant - orderFromDB.quant;

                                //subtract product
                                order orderToUpdate = new order();
                                product productToUpdate = new product();

                                productToUpdate = productFromDB;
                                orderToUpdate = orderFromDB;

                                productToUpdate.QUANTITY = productFromDB.QUANTITY - productToSubtract;

                                // if product 0 then change to Unavailable
                                if (productToUpdate.QUANTITY == 0)
                                {
                                    //update product
                                    productToUpdate.AVAILABILITY = "UNAVAILABLE";
                                    productRepository.Update(productToUpdate);

                                    //update order quant
                                    orderToUpdate.quant = quant;
                                    orderToUpdate.ammout = quant * productFromDB.SELL_PRICE;
                                    orderRepository.Update(orderToUpdate);
                                    TempData["success"] = "Your Order Updated Successfully!";
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    productRepository.Update(productToUpdate);

                                    //update order quant
                                    orderToUpdate.quant = quant;
                                    orderToUpdate.ammout = quant * productFromDB.SELL_PRICE;
                                    orderRepository.Update(orderToUpdate);

                                    TempData["success"] = "Your Order Updated Successfully!";
                                    return RedirectToAction("Index");
                                }
                            }
                        }
                        else
                        {
                            order orderToUpdate = new order();
                            product productToUpdate = new product();

                            productToUpdate = productFromDB;
                            orderToUpdate = orderFromDB;

                            int productToAdd = orderFromDB.quant - quant;



                            //add product
                            productToUpdate.QUANTITY = productFromDB.QUANTITY + productToAdd;


                            //Change "Available"
                            if (productToUpdate.QUANTITY > 0)
                            {
                                productToUpdate.AVAILABILITY = "AVAILABLE";
                                productRepository.Update(productToUpdate);


                                orderToUpdate.quant = quant;
                                orderToUpdate.ammout = quant * productFromDB.SELL_PRICE;
                                orderRepository.Update(orderToUpdate);

                                TempData["success"] = "Your Order Updated Successfully!";
                                return RedirectToAction("Index");

                            }
                            else
                            {
                                productRepository.Update(productToUpdate);

                                orderToUpdate.quant = quant;
                                orderToUpdate.ammout = quant * productFromDB.SELL_PRICE;
                                orderRepository.Update(orderToUpdate);

                                TempData["success"] = "Your Order Updated Successfully!";
                                return RedirectToAction("Index");
                            }

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

        // Chart Action methods
        [HttpGet]
        public ActionResult OrderTypeChart()
        {

            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    OrderRepository orderRepository = new OrderRepository();

                    var recievedOrderCount = orderRepository.GetRecievedOrderByUser((string)Session["LID"]).Count();
                    var pendingOrderCount = orderRepository.GetPendingOrderByUser((string)Session["LID"]).Count();
                    var confirmedOrderCount = orderRepository.GetConfirmedOrderByUser((string)Session["LID"]).Count();

                    ViewData["rOrder"] = recievedOrderCount;
                    ViewData["pOrder"] = pendingOrderCount;
                    ViewData["cOrder"] = confirmedOrderCount;

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
        public ActionResult LastOrderChart(int id = 5)
        {
            if (Session["SID"] != null)
            {
                if (this.CheckCustomer((int)Session["SID"]))
                {
                    OrderRepository orderRepository = new OrderRepository();
                    ProductRepository productRepository = new ProductRepository();

                    if (TempData["q"] != null)
                    {
                        id = (int)TempData["q"];
                        var orderListFromDB = orderRepository.GetOrderByUser((string)Session["LID"]).OrderByDescending(x => x.orderid).Take(id);
                        var productListFromDB = productRepository.GetAll();


                        List<string> oList = new List<string>();
                        List<string> pList = new List<string>();

                        foreach (var item in orderListFromDB)
                        {
                            oList.Add(item.quant.ToString());

                            pList.Add(productRepository.GetProductByID(item.prodid).P_NAME);
                        }

                        var oArray = oList.ToArray();
                        var pArray = pList.ToArray();


                        ViewData["olist"] = oArray;
                        ViewData["plist"] = pArray;


                        var orderItemTypeChart = new Chart(width: 600, height: 400)
                        .AddTitle("Ordered Item Chart")
                        .AddSeries(
                        name: "Orders",
                        xValue: pArray,
                        yValues: oArray)
                        .Write();

                        ViewData["orderChart"] = orderItemTypeChart;

                        return View();
                    }
                    else
                    {
                        var orderListFromDB = orderRepository.GetOrderByUser((string)Session["LID"]).OrderByDescending(x => x.orderid).Take(id);
                        var productListFromDB = productRepository.GetAll();


                        List<string> oList = new List<string>();
                        List<string> pList = new List<string>();

                        foreach (var item in orderListFromDB)
                        {
                            oList.Add(item.quant.ToString());

                            pList.Add(productRepository.GetProductByID(item.prodid).P_NAME);
                        }

                        var oArray = oList.ToArray();
                        var pArray = pList.ToArray();


                        ViewData["olist"] = oArray;
                        ViewData["plist"] = pArray;


                        var orderItemTypeChart = new Chart(width: 600, height: 400)
                        .AddTitle("Ordered Item Chart")
                        .AddSeries(
                        name: "Orders",
                        xValue: pArray,
                        yValues: oArray)
                        .Write();

                        ViewData["orderChart"] = orderItemTypeChart;

                        return View();
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


        // Non Action Methods
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
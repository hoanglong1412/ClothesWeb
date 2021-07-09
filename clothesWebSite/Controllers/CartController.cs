using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clothesWebSite.Models;
using clothesWebSite.DAO;
using PayPal.Api;
using Payment = clothesWebSite.Models.Payment;
using System.Net;
using PayPal;

namespace clothesWebSite.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        ProductDAO productDAO = new ProductDAO();
        PaymentDAO paymentDAO = new PaymentDAO();
        PaymentDetailDAO paymentDetailDAO = new PaymentDetailDAO();
        MyDBContext db = new MyDBContext();
        public ActionResult viewCart()
        {
            List<ShoppingCart> listCart = getCart();
            IEnumerable<Product> listPro = productDAO.getHotProduct(3);
            ViewBag.AllCount = countAll();
            ViewBag.AllTotal = totalAll();
            ViewBag.listProduct = listPro;
            ViewBag.cart = listCart;
            return View(listCart);
        }

        //lay gio hang
        public List<ShoppingCart> getCart()
        {
            List<ShoppingCart> listCart = Session["Cart"] as List<ShoppingCart>;
            if(listCart == null)
            {
                listCart = new List<ShoppingCart>();
                Session["Cart"] = listCart;
            }
            return listCart;
        }

        //them hang vao gio
        public ActionResult addToCart(int id, string strURL)
        {
            List<ShoppingCart> listCart = getCart();
            ShoppingCart product = listCart.Where(m => m.cart_product_id == id).FirstOrDefault();
            if(product == null)
            {
                product = new ShoppingCart(id);
                listCart.Add(product);
                return Redirect(strURL);
            }
            else
            {
                product.cart_quantity ++;
                return Redirect(strURL);
            }
        }

        //lay tong so luong hang trong gio
        private int countAll()
        {
            int count = 0;
            List<ShoppingCart> listCart = Session["Cart"] as List<ShoppingCart>;
            if(listCart != null)
            {
                count = listCart.Sum(m => m.cart_quantity);
            }
            return count;
        }

        //lay tong gia tien trong gio hang
        private double? totalAll()
        {
            double? total = 0;
            List<ShoppingCart> listCart = Session["Cart"] as List<ShoppingCart>;
            if (listCart != null)
            {
                total = listCart.Sum(m => m.cart_total) ;
            }
            return total;
        }


        //lay view dem so luong hang trong gio o layout shared
        public ActionResult CartPartialView()
        {
            ViewBag.AllCount = countAll();

            return PartialView();
        }

        //xoa 1 san pham trong gio hang
        public ActionResult deleteRow(int id)
        {
            List<ShoppingCart> listCart = getCart();
            ShoppingCart product = listCart.Where(m => m.cart_product_id == id).FirstOrDefault();
            if(product != null)
            {
                listCart.RemoveAll(m => m.cart_product_id == id);
                return RedirectToAction("viewCart");
            }
            return RedirectToAction("viewCart");


        }

        //cap nhat toan bo gio hang
        [HttpPost]
        public ActionResult updateCart(FormCollection form)
        {
            List<ShoppingCart> listCart = getCart();
            int i = 1;
            foreach (ShoppingCart p in listCart)
            {
                string name = "quantity" + i;
                p.cart_quantity = int.Parse(form[name]);
                i++;
            }
            return RedirectToAction("viewCart");


        }

        //lay view cua tong so luong va tong so tien cua gio hang o trong view gio hang
        public ActionResult orderSummaryPartialView()
        {
            ViewBag.AllCount = countAll();
            ViewBag.AllTotal = totalAll();
            return PartialView();
        }

        public ActionResult checkoutAddress()
        {
            if (Session["user"] == null || Session["user"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult checkoutDeliveryMethod(FormCollection form)
        {
            if (Session["user"] == null || Session["user"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.name = form["name"];
            ViewBag.address = form["address"];
            ViewBag.phone = form["phone"];
            return View();
        }

        [HttpPost]
        public ActionResult checkoutPaymentMethod(FormCollection form)
        {
            if (Session["user"] == null || Session["user"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.name = form["name"];
            ViewBag.address = form["address"];
            ViewBag.phone = form["phone"];
            ViewBag.delivery = form["delivery"];
            return View();
        }

        [HttpPost]
        public ActionResult OrderPreview(FormCollection form)
        {
            if (Session["user"] == null || Session["user"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.name = form["name"];
            ViewBag.address = form["address"];
            ViewBag.phone = form["phone"];
            ViewBag.delivery = form["delivery"];
            ViewBag.payment = form["payment"];
            List<ShoppingCart> listCart = getCart();
            ViewBag.AllCount = countAll();
            ViewBag.AllTotal = totalAll();
            ViewBag.cart = listCart;
            return View();
        }

        [HttpPost]
        public ActionResult Order(FormCollection form)
        {
            String name = form["name"];
            String address = form["address"];
            String phone = form["phone"];
            String delivery = form["delivery"];
            String paymentmethod = form["payment"];
            User user = (User)Session["user"];
            List<ShoppingCart> listCart = getCart();

            

            Payment payment = new Payment();
            payment.name_receiver = name;
            payment.address_deliver = address;
            payment.deliver_type = delivery;
            payment.payway = paymentmethod;
            payment.phone_receiver = phone;
            payment.state = 1;
            payment.user_id = user.user_id;
           

            //Add payment for update when paypal is success or failure
            Session["Payment"] = payment;
            //Paypal payment. Need to pay first before update to database
            if (paymentmethod.Equals(PaymentDAO.PAYPAL))
            {
                return RedirectToAction("PaymentWithPaypal");
            }

            //Else just update directly
            db.Payments.Add(payment);
            db.SaveChanges();

            foreach (var item in listCart)
            {
                paymentDetailDAO.addPaymentDetail(payment.payment_id, item.cart_product_id, item.cart_quantity, item.cart_sale_price);
            }


            Session["Cart"] = null;
            return RedirectToAction("orderHistory","User");
        }

        //--------------------------------------------------------------------
        //Paypal payment will go from here
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            Payment dbPayment = Session["Payment"] as Payment;
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Cart/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailurePaypal");
                    }
                }
            }
            catch (PayPalException ex)
            {
                string errorMessage;
                if (ex is ConnectionException)
                {
                    errorMessage = ex.Message;

                }
                else
                {
                    errorMessage = ex.ToString();
                }
                System.Diagnostics.Debug.WriteLine("Paypal Error: " + errorMessage + ex.StackTrace + ex.Data.ToString());
                return View("FailurePaypal");
            }
            //on successful payment, show success page to user.  
            //Update to database
            db.Payments.Add(dbPayment);
            db.SaveChanges();
            List<ShoppingCart> listCart = getCart();
            foreach (var item in listCart)
            {
                paymentDetailDAO.addPaymentDetail(dbPayment.payment_id, item.cart_product_id, item.cart_quantity, item.cart_sale_price);
            }

            Session["Cart"] = null;
            return RedirectToAction("orderHistory", "User");
        }

        public ActionResult FailurePaypal()
        {
            return View();
        }

        public ActionResult SuccessPaypal()
        {
            return View();
        }

        private PayPal.Api.Payment payment;
        private PayPal.Api.Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new PayPal.Api.Payment()
            {
                id = paymentId
            };
            return payment.Execute(apiContext, paymentExecution);
        }
        private PayPal.Api.Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //Get current payment
            Payment curPayment = Session["Payment"] as Payment;

            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            var shoppingCart = getCart();
            double shippingPrice = 10;
            double totalPrice = 0;
            foreach (var item in shoppingCart)
            {
                totalPrice += item.cart_total??0;
                itemList.items.Add(new Item()
                {
                    name = item.cart_product_name,
                    currency = "USD",
                    price = item.cart_sale_price.ToString(),
                    quantity = item.cart_quantity.ToString(),
                    sku = "sku"
                });
            }
           
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = shippingPrice.ToString(),
                subtotal = totalPrice.ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping)
                        + Convert.ToDouble(details.subtotal)).ToString(),
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Pay your cart on Olayigu with paypal",
                invoice_number = Convert.ToString((new Random()).Next(100000)), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            payment = new PayPal.Api.Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return payment.Create(apiContext);
        }

    }




}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clothesWebSite.Models;
using clothesWebSite.DAO;
using clothesWebSite.Models;

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
            db.Payments.Add(payment);
            db.SaveChanges();

            foreach (var item in listCart)
            {
                paymentDetailDAO.addPaymentDetail(payment.payment_id, item.cart_product_id, item.cart_quantity, item.cart_sale_price);
            }
            Session["Cart"] = null;
            return RedirectToAction("orderHistory","User");
        }
    }
}
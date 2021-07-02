using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clothesWebSite.Models;
using clothesWebSite.DAO;

namespace clothesWebSite.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        ProductDAO productDAO = new ProductDAO();
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

        public ActionResult CartPartialView()
        {
            ViewBag.AllCount = countAll();

            return PartialView();
        }

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
        [HttpPost]
        public ActionResult updateCart(FormCollection form)
        {
            List<ShoppingCart> listCart = getCart();
            //ShoppingCart product = listCart.Where(m => m.cart_product_id == id).FirstOrDefault();
            int i = 1;
            foreach (ShoppingCart p in listCart)
            {
                string name = "quantity" + i;
                p.cart_quantity = int.Parse(form[name]);
                i++;
            }
            return RedirectToAction("viewCart");


        }
    }
}
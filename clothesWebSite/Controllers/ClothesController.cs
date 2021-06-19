using clothesWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clothesWebSite.DAO;

namespace clothesWebSite.Controllers
{
    public class ClothesController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        // GET: Clothes
        public ActionResult Index()
        {
            IEnumerable<Product> listPro = productDAO.getList(8);
            ViewBag.product = listPro;
            return View();
        }
    }
}
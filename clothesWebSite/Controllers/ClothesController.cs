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
        BannerDAO bannerDAO = new BannerDAO();
        // GET: Clothes
        public ActionResult Index()
        {
            IEnumerable<Product> listPro = productDAO.getHotProduct(8);
            IEnumerable<Banner> listBannerTop = bannerDAO.getBannerTop();
            IEnumerable<Banner> listBannerBot = bannerDAO.getBannerBottom();
            ViewBag.product = listPro;
            ViewBag.bannerT = listBannerTop;
            ViewBag.bannerB = listBannerBot;
            return View();
        }
    }
}
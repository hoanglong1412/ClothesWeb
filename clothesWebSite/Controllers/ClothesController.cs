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
        PostDAO postDAO = new PostDAO();
        // GET: Clothes
        public ActionResult Index()
        {
            IEnumerable<Product> listPro = productDAO.getHotProduct(8);
            IEnumerable<Banner> listBannerTop = bannerDAO.getBannerTop();
            IEnumerable<Banner> listBannerBot = bannerDAO.getBannerBottom();
            IEnumerable<Post> listPost = postDAO.getNewPost(2);
            ViewBag.product = listPro;
            ViewBag.bannerT = listBannerTop;
            ViewBag.bannerB = listBannerBot;
            ViewBag.post = listPost;
            return View();
        }
        public ActionResult ProductDetail(int id)
        {
            Product product = productDAO.getOne(id);
            String idCategory = product.type_id;
            IEnumerable<Product> listProCate = productDAO.getSameCategory(idCategory,3,id);
            IEnumerable<Product> listHotPro = productDAO.getHotProduct(3,id);
            ViewBag.productCate = listProCate;
            ViewBag.productHot = listHotPro;
            return View(product);
        }
    }
}
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
        ProductTypeDAO productTypeDAO = new ProductTypeDAO();
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
            string idCategory = product.type_id;
            IEnumerable<Product> listProCate = productDAO.getSameCategory(idCategory,3,id);
            IEnumerable<Product> listHotPro = productDAO.getHotProduct(3,id);
            ViewBag.productCate = listProCate;
            ViewBag.productHot = listHotPro;
            return View(product);
        }
        public ActionResult ProductCategoty(string id)
        {
            IEnumerable<ProductType> listTypeM = productTypeDAO.getListM();
            IEnumerable<ProductType> listTypeF = productTypeDAO.getListF();
            IEnumerable<ProductType> listTypeK = productTypeDAO.getListK();
            IEnumerable<Product> listPro = productDAO.getSameCategory(id);
            ViewBag.listTypeM = listTypeM;
            ViewBag.listTypeF = listTypeF;
            ViewBag.listTypeK = listTypeK;
            ViewBag.listPro = listPro;
            return View();
        }
        public ActionResult ProductAllCategoty()
        {
            IEnumerable<ProductType> listTypeM = productTypeDAO.getListM();
            IEnumerable<ProductType> listTypeF = productTypeDAO.getListF();
            IEnumerable<ProductType> listTypeK = productTypeDAO.getListK();
            IEnumerable<Product> listPro = productDAO.getList();
            ViewBag.listTypeM = listTypeM;
            ViewBag.listTypeF = listTypeF;
            ViewBag.listTypeK = listTypeK;
            ViewBag.listPro = listPro;
            return View();
        }
   
    }
}
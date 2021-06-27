using clothesWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clothesWebSite.DAO;
using PagedList;
using PagedList.Mvc;
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
        public ActionResult ProductCategoty(string id, int getType)
        {
            if (getType == 0)
            {
                IEnumerable<Product> listPro = productDAO.getSameCategory(id);
                ViewBag.listPro = listPro;
            } else
            {
                IEnumerable<Product> listPro = productDAO.getSaleProductsWithCategory(id);
                ViewBag.listPro = listPro;
            }
            
            return View();
        }
        public ActionResult AllProduct(int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            int count = productDAO.ProductCount();
            IEnumerable<Product> listPro = productDAO.getHotProduct(count);
            return View(listPro.ToPagedList(pageNum, pageSize));
        }

        //public ActionResult Category()
        //{
        //    IEnumerable<ProductType> listTypeM = productTypeDAO.getListM();
        //    IEnumerable<ProductType> listTypeF = productTypeDAO.getListF();
        //    IEnumerable<ProductType> listTypeK = productTypeDAO.getListK();
        //    ViewBag.listTypeM = listTypeM;
        //    ViewBag.listTypeF = listTypeF;
        //    ViewBag.listTypeK = listTypeK;
        //    ViewBag.countM = productDAO.ProductCountM();
        //    ViewBag.countF = productDAO.ProductCountF();
        //    ViewBag.countK = productDAO.ProductCountK();
        //    return PartialView();
        //}

        public ActionResult AllPost()
        {
            IEnumerable<Post> listPost = postDAO.getNewPost(2);
            ViewBag.listPost = listPost;
            return View();
        }
        public ActionResult PostDeitail(int id)
        {
            Post post = postDAO.getOne(id);
            return View(post);
        }

        public ActionResult ProductSale(int? page, int itemShow = 12)
        {
            IEnumerable<Product> saleProducts = productDAO.getSaleProducts();
            if (itemShow > saleProducts.Count())
            {
                itemShow = saleProducts.Count();
            }
            int pageNum = (page ?? 1);
            int pageSize = itemShow;
            ViewBag.itemShow = itemShow;
            ViewBag.itemCount = saleProducts.Count();
            return View(saleProducts.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Category(int getType)
        {
            //Type = 0 lay tat ca san pham
            if (getType == 0)
            {
                IEnumerable<ProductType> listTypeM = productTypeDAO.getListM();
                IEnumerable<ProductType> listTypeF = productTypeDAO.getListF();
                IEnumerable<ProductType> listTypeK = productTypeDAO.getListK();
                ViewBag.listTypeM = listTypeM;
                ViewBag.listTypeF = listTypeF;
                ViewBag.listTypeK = listTypeK;
                ViewBag.countM = productDAO.ProductCountM();
                ViewBag.countF = productDAO.ProductCountF();
                ViewBag.countK = productDAO.ProductCountK();
            } 
           
            else if (getType == 1)
            {
                //Type == 1 Lay san pham khuyen mai
                IEnumerable<ProductType> listTypeM = productTypeDAO.getListM();
                IEnumerable<ProductType> listTypeF = productTypeDAO.getListF();
                IEnumerable<ProductType> listTypeK = productTypeDAO.getListK();
                ViewBag.listTypeM = listTypeM;
                ViewBag.listTypeF = listTypeF;
                ViewBag.listTypeK = listTypeK;
                int[] productGenderCount = productDAO
                    .getProductCountByCategoryGender(productDAO.getSaleProducts());
                ViewBag.countM = productGenderCount[0];
                ViewBag.countF = productGenderCount[1];
                ViewBag.countK = productGenderCount[2];
            }
            ViewBag.getType = getType;
            
            return PartialView();
        }
    }
}
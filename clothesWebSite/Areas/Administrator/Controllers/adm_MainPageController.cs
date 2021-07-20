using clothesWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clothesWebSite.Areas.Administrator.Controllers
{
    public class adm_MainPageController : Controller
    {
        MyDBContext db = new MyDBContext();
        // GET: Admin/adm_MainPage
        public ActionResult Index()
        {
            if(Session["taikhoanadmin"]==null)
            {
                return RedirectToAction("Login","adm_MainPage");
            }
            else
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            
            var taikhoan = collection["account"];
            var matkhau = collection["password"];
            if(String.IsNullOrEmpty(taikhoan))
            {
                if(String.IsNullOrEmpty(matkhau))
                {
                    ViewBag.ThongBao = "Thông tin đăng nhập đang trống";
                }
                else
                ViewBag.ThongBao = "Vui lòng điền Account";
            }
            else
            {
                if(String.IsNullOrEmpty(matkhau))
                {
                    ViewBag.ThongBao = "Vui lòng điền Password";
                }
                else
                {
                    Admin ad = db.Admins.SingleOrDefault(n => n.username == taikhoan && n.password == matkhau);
                    if (ad != null)
                    {
                        Session["taikhoanadmin"] = ad;
                        return RedirectToAction("Index", "adm_MainPage");
                    }
                    else
                        ViewBag.ThongBao = "Thông tin đăng nhập không đúng";
                }
            }
            return View();
        }

    }
}
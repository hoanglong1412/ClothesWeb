using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clothesWebSite.Models;
using clothesWebSite.DAO;

namespace clothesWebSite.Controllers
{
    public class UserController : Controller
    {
        MyDBContext db = new MyDBContext();
        UserDAO userDAO = new UserDAO();
        public ActionResult Login()
        {
            return View();
        }
        // GET: User
        [HttpPost]
        public ActionResult LoginConfirm(FormCollection form)
        {
            String email = form["email"];
            String password = form["password"];
            User user = userDAO.getRow(email, password);
            if(user != null)
            {
                ViewBag.Message = "alert('dang nhap thanh cong')";
                Session["user"] = user;
                Session["username"] = user.full_name;
                return RedirectToAction("Index", "Clothes");
            }
            else
            {
                ViewBag.Message = "Wrong email or password";
            }

            return View("Login");
        }

        [HttpPost]
        public ActionResult RegisterConfirm(FormCollection form)
        {
            String name = form["name"];
            String email = form["email"];
            String password = form["password"];
            User user = new User();
            user.full_name = name;
            user.email = email;
            user.password = password;
            user.user_role = 0;
            user.phone = "0123456789";
            db.Users.Add(user);
            db.SaveChanges();
            ViewBag.Message2 = "Successfully";
            return View("Login");
        }
    }
}
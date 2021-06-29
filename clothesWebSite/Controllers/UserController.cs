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
        readonly UserDAO userDAO = new UserDAO();
        readonly ContactDAO contactDAO = new ContactDAO();
        public ActionResult Logout()
        {
            Session["user"] = null;
            return View("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        // GET: User
        [HttpPost]
        public ActionResult LoginConfirm(FormCollection form)
        {
            String phone = form["phone"];
            String password = form["password"];
            if(String.IsNullOrEmpty(phone))
            {
                ViewData["Erro1"] = "please enter your phone number";
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewBag.Erro2 = "please enter your password";
            }
            else
            {
                User user = userDAO.getRow(phone, password);
                if (user != null)
                {
                    ViewBag.Message = "alert('dang nhap thanh cong')";
                    Session["user"] = user;
                    Session["username"] = user.full_name;
                    return RedirectToAction("Index", "Clothes");
                }
                else
                {
                    ViewBag.Message = "Wrong phone or password";
                }
            }
           

            return View("Login");
        }

        [HttpPost]
        public ActionResult RegisterConfirm(FormCollection form)
        {
            String name = form["name"];
            String phone = form["phone"];
            String password = form["password"];
            String repass = form["re_password"];
            if (String.IsNullOrEmpty(phone))
            {
                ViewBag.ErrorRe1 = "please enter your phone number";
            }
            else if(userDAO.checkPhone(phone) == false)
            {
                ViewBag.ErrorRe2 = "This account is already exist";
            }
            else if (String.IsNullOrEmpty(name))
            {
                ViewBag.ErrorRe3 = "please enter your name";
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewBag.ErrorRe4 = "please enter your password";
            }
            else if (password != repass)
            {
                ViewBag.ErrorRe5 = "Your re-password is wrong, please enter again";
            }
            else
            {
                userDAO.addUser(phone, name, password);
                ViewBag.Message2 = "Register Successful! Now you can Login in the next form -->";
                ViewBag.phone = phone;
                return View("Login");
            }
            return View("Login");
        }
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactConfirm(FormCollection form)
        {
            String first_name = form["first_name"];
            String last_name = form["last_name"];
            String email = form["email"];
            String phone = form["phone"];
            String content = form["content"];
            String name = first_name + "" + last_name;
            contactDAO.addContact(name, email, phone, content);
            ViewBag.success = "We already receive your contact, we will answer as fast as we can !!!";
            return View("Contact");
        }
        public ActionResult TextPage()
        {
            return View();
        }
        public ActionResult Faq()
        {
            return View();
        }

        [Route("my-account")]
        //controller thong tin tai khoan
        public ActionResult AccountDetail()
        {
            User user = (User)Session[UserDAO.KEY_USER];
            if (user != null)
            {
                if (TempData["ErrorPhone"] != null)
                {
                    ViewBag.ErrorPhone = TempData["ErrorPhone"];
                }
                return View();
            } else
            {
                return RedirectToAction("Login");
            }
            
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {
            string password = collection.Get("password_1");
            User user = Session[UserDAO.KEY_USER] as User;
            userDAO.updateUser(user.user_id, password);
            Session[UserDAO.KEY_USER] = null;
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult UpdateBasicInfo(FormCollection collection)
        {
            //Get Data from view
            User user = Session[UserDAO.KEY_USER] as User;
            string phone = collection.Get("phone");
            string fullname = collection.Get("fullname");
            string address = collection.Get("address");
            string email = collection.Get("email");
            DateTime? dateBirth;
            int gender = 0;
            try
            {
                 dateBirth = DateTime.Parse(collection.Get("datebirth"));
                 gender = Int16.Parse(collection.Get("gender"));
            } catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                dateBirth = null;
            }
            

            //Check if phone exists
            if (userDAO.checkPhone(phone) == false && phone != user.phone)
            {
                TempData["ErrorPhone"] = "Phone " + phone + " already exists, please try another phone";
            } else
            {
                //Update to database and session
                Session[UserDAO.KEY_USER] = userDAO
                    .updateUser(user.user_id, phone, fullname, dateBirth, gender, address, email);
                TempData["ErrorPhone"] = null;
            }

           
            return RedirectToAction("AccountDetail");
        }
    }
}
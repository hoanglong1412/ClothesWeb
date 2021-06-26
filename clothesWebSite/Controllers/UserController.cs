﻿using System;
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
        ContactDAO contactDAO = new ContactDAO();
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

        //controller thong tin tai khoan
        public ActionResult AccountDetail()
        {
            return View();
        }
    }
}
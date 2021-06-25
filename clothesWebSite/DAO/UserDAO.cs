using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using clothesWebSite.Models;
namespace clothesWebSite.DAO
{
    public class UserDAO
    {
        private MyDBContext db;
        public UserDAO()
        {
            db = new MyDBContext();
        }
        public User getRow(String phone, String password)
        {
            User user = db.Users.Where(m => m.phone == phone && m.password == password).FirstOrDefault();
            return user;
        }
        public Boolean checkPhone(String phone)
        {
            User user = db.Users.Where(m => m.phone == phone).FirstOrDefault();
            if (user == null)
            {
                return true;
            }
            return false;
        }


        public void addUser(String phone, String name, String password)
        {
            User user = new User();
            user.full_name = name;
            user.password = password;
            user.user_role = User.CUSTOMER;
            user.phone = phone;
            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
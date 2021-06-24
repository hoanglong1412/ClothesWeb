using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using clothesWebSite.Models;
namespace clothesWebSite.DAO
{
    public class UserDAO
    {
        MyDBContext db = new MyDBContext();
        public User getRow(String email,String password)
        {
            User user = db.Users.Where(m => m.email == email && m.password == password).FirstOrDefault();
            return user;
        }
    }
}
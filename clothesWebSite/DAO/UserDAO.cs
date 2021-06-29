using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using clothesWebSite.Models;
namespace clothesWebSite.DAO
{
    public class UserDAO
    {
        static public readonly string KEY_USER = "user";
        //Possible value for gender
        static public readonly int FEMALE = 0;
        static public readonly int MALE = 1;

        static public string getGender (int gender)
        {
            if (gender == MALE) return "Male";
            else return "Female";
        }

        //Possible value for user role
        static public readonly int CUSTOMER = 0;

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

        public User getRow(int id)
        {
            User user = db.Users.FirstOrDefault(u => u.user_id == id);
            return user;
        }
        public bool checkPhone(String phone)
        {
            User user = db.Users.FirstOrDefault(m => m.phone == phone);
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
            user.user_role = CUSTOMER;
            user.phone = phone;
            db.Users.Add(user);
            db.SaveChanges();
        }

        public User updateUser(int id, string phone, string name, DateTime? datebirth,
                                int gender, string address, string email)
        {
            User user = getRow(id);
            user.phone = phone;
            user.full_name = name;
            user.date_birth = datebirth;
            user.gender = gender;
            user.address = address;
            user.email = email;
            db.SaveChanges();
            return user;
        }

        public User updateUser(int id, string password)
        {
            User user = getRow(id);
            user.password = password;
            db.SaveChanges();
            return user;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using clothesWebSite.Models;

namespace clothesWebSite.DAO
{
    public class ContactDAO
    {
        private MyDBContext db;
        public ContactDAO()
        {
            db = new MyDBContext();
        }

        public void addContact(String name, String email,String phone ,String content)
        {
            Contact contact = new Contact();
            contact.sender_email = email;
            contact.sender_full_name = name;
            contact.sender_phone = phone;
            contact.content = content;
            contact.contact_type = 1;
            db.Contacts.Add(contact);
            db.SaveChanges();
        }
    }
}
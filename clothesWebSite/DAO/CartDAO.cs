using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using clothesWebSite.Models;

namespace clothesWebSite.DAO
{
    public class CartDAO
    {
        private MyDBContext db;
        public CartDAO()
        {
            db = new MyDBContext();
        }

    }
}
using clothesWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clothesWebSite.DAO
{
    public class ProductDAO
    {
        MyDBContext db = new MyDBContext();


        //lay ra num san pham giam theo ngay
        public IEnumerable<Product> getList(int num)
        {
            IEnumerable<Product> listProduct = db.Products.Where(m => m.state == 1).OrderByDescending(m => m.last_update).Take(num);
            return listProduct;
        }
    }
}
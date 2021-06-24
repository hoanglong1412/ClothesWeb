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
        public IEnumerable<Product> getHotProduct(int num)
        {
            IEnumerable<Product> listProduct = db.Products.Where(m => m.state == 1).OrderByDescending(m => m.create_day).Take(num);
            return listProduct;
        }
        public IEnumerable<Product> getHotProduct(int num,int id)
        {
            IEnumerable<Product> listProduct = db.Products.Where(m => m.state == 1 && m.product_id != id).OrderByDescending(m => m.create_day).Take(num);
            return listProduct;
        }
        public Product getOne(int id)
        {
            Product Product = db.Products.Where(m => m.state == 1 && m.product_id == id).FirstOrDefault();
            return Product;
        }
        public IEnumerable<Product> getSameCategory(String catelogyId,int num,int id)
        {
            IEnumerable<Product> listProduct = db.Products.Where(m => m.state == 1 && m.type_id == catelogyId && m.product_id != id).OrderByDescending(m => m.create_day).Take(num);
            return listProduct;
        }
    }
}
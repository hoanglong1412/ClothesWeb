using clothesWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clothesWebSite.DAO
{
    public class ProductDAO
    {

        private MyDBContext db;
        public ProductDAO()
        {
            db = new MyDBContext();
        }

        //lay ra num san pham giam theo ngay
        public IEnumerable<Product> getHotProduct(int num)
        {
            IEnumerable<Product> listProduct = db.Products.Where(m => m.state == 1).OrderByDescending(m => m.create_day).Take(num);
            return listProduct;
        }
        //lay san pham moi nhung khong phai san pham dang hien trong trang chi tiet
        public IEnumerable<Product> getHotProduct(int num, int id)
        {
            IEnumerable<Product> listProduct = db.Products.Where(m => m.state == 1 && m.product_id != id).OrderByDescending(m => m.create_day).Take(num);
            return listProduct;
        }
        //láy san pham theo id
        public Product getOne(int id)
        {
            Product Product = db.Products.Where(m => m.state == 1 && m.product_id == id).FirstOrDefault();
            return Product;
        }
        //lay san pham theo loai theo so luon va khong lay san pham dang co tren chi tiet san pham
        public IEnumerable<Product> getSameCategory(String catelogyId, int num, int id)
        {
            IEnumerable<Product> listProduct = db.Products.Where(m => m.state == 1 && m.type_id == catelogyId && m.product_id != id).OrderByDescending(m => m.create_day).Take(num);
            return listProduct;
        }
        //lay toan bo san pham
        public IEnumerable<Product> getList()
        {
            IEnumerable<Product> listProduct = db.Products;
            return listProduct;
        }
        //lay tat ca san pham cung loai
        public IEnumerable<Product> getSameCategory(string catelogyId)
        {
            IEnumerable<Product> listProduct = db.Products.Where(m => m.state == 1 && m.type_id == catelogyId).OrderByDescending(m => m.create_day);
            return listProduct;
        }
        //lay tong hang
        public int ProductCount()
        {
           int count = db.Products.Count();
            return count;
        }

        //lay tong san pham nam
        public int ProductCountM()
        {
            int count = db.Products.Where(m => m.type_id.Contains(Product.MALE)).Count();
            return count;
        }
        //lay tong san pham nu
        public int ProductCountF()
        {
            int count = db.Products.Where(m => m.type_id.Contains(Product.FEMALE)).Count();
            return count;
        }
        //lay tong san pham tre em
        public int ProductCountK()
        {
            int count = db.Products.Where(m => m.type_id.Contains(Product.KID)).Count();
            return count;
        }
    }
}
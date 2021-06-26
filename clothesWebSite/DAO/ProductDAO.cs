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

        //Lay san pham khuyen mai
        public IEnumerable<Product> getSaleProducts()
        {
            //Hàm any() là kiểm tra trong list có phần tử nào không. Vì bảng ProductDiscount có quan hệ
            // 1-1 nên nó không hiện trong Models, chúng ta có thể chi cập nó thong qua cha của nó
            // là Discount hoặc Product
            return db.Products.Where(p => p.Discounts.Any()).OrderByDescending(p => p.create_day);
        }

        public double getSalePrice(Product product)
        {
            Discount discount = db.Discounts.Find(product.Discounts.First());
            int salePrice = discount.discount_by_price ?? 0;
            return salePrice;
        }
    }
}
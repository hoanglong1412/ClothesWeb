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
            //Hàm any() là kiểm tra trong list có phần tử nào thỏa mãn điêu kiện không. Vì bảng ProductDiscount có quan hệ
            // 1-1 nên nó không hiện trong Models, chúng ta có thể chi cập nó thong qua cha của nó
            // là Discount hoặc Product
            return db.Products
                .Where(p => p.Discounts.Any(d => d.state == Discount.APPLIED
                        && d.to_date >= DateTime.Now && DateTime.Now >= d.from_date))
                .OrderByDescending(p => p.create_day);
        }

        //Lay san pham khuyen mai theo loai
        public IEnumerable<Product> getSaleProductsWithCategory(String categoryId)
        {
            return db.Products
                .Where(p => p.Discounts.Any(d => d.state == Discount.APPLIED
                        && d.to_date >= DateTime.Now && DateTime.Now >= d.from_date))
                .Where(p => p.type_id == categoryId)
                .OrderByDescending(p => p.create_day);
        }

        //Kiem tra san pham co khuyen mai
        static public bool isSaleProduct(Product p)
        {
            return p.Discounts.Any(d => d.state == Discount.APPLIED
                        && d.to_date >= DateTime.Now && DateTime.Now >= d.from_date);
        }
        //Lay gia san pham khuyen mai
        static public double getSalePrice(Product product)
        {
            Discount discount = product.Discounts
                .FirstOrDefault(d => d.state == Discount.APPLIED
                        && d.to_date >= DateTime.Now && DateTime.Now >= d.from_date);

            if (discount.discount_by_ratio != null)
            {
                return product.sale_price * discount.discount_by_ratio ?? 0;
            }
            else if (discount.discount_by_price != null)
            {
                return discount.discount_by_price ?? 0;
            }
            else
            {
                return 0;
            }

        }
        //Kiem tra san pham co phai la san pham moi
        static public bool isHotProduct(Product p)
        {
            TimeSpan t = p.create_day - DateTime.Now ?? TimeSpan.Zero;
            return t.Days <= 30;
        }

        public int[] getProductCountByCategoryGender(IEnumerable<Product> products)
        {
            int[] rs = new int[3];
            rs[0] = products.Where(p => p.type_id.Contains(Product.MALE)).Count();
            rs[1] = products.Where(p => p.type_id.Contains(Product.FEMALE)).Count();
            rs[2] = products.Where(p => p.type_id.Contains(Product.KID)).Count();
            return rs;
        }

        public IEnumerable<Product> findProduct(string key,int num)
        {
            IEnumerable<Product> listProduct = db.Products.Where(m => m.state == 1 && m.product_name.Contains(key)).OrderByDescending(m => m.create_day).Take(num);
            return listProduct;
        }
        public int findProductCount(string key)
        {
            int i = db.Products.Where(m => m.state == 1 && m.product_name.Contains(key)).Count();
            return i;
        }
    }
}
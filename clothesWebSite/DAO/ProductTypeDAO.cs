using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using clothesWebSite.Models;
using clothesWebSite.DAO;

namespace clothesWebSite.DAO
{
    public class ProductTypeDAO
    {
        private MyDBContext db;
        public ProductTypeDAO()
        {
            db = new MyDBContext();
        }

        //lay danh sach tat cac cac loai sasn pham
        public IEnumerable<ProductType> getList()
        {
            IEnumerable<ProductType> list = db.ProductTypes;
            return list;
        }
        //lay tat ca loai san pham nam
        public IEnumerable<ProductType> getListM()
        {
            IEnumerable<ProductType> list = db.ProductTypes.Where(m => m.type_id.Contains(Product.MALE));
            return list;
        }
        //lay tat ca loai san pham nu
        public IEnumerable<ProductType> getListF()
        {
            IEnumerable<ProductType> list = db.ProductTypes.Where(m => m.type_id.Contains(Product.FEMALE));
            return list;
        }
        //lay tat ca loai san pham tre em
        public IEnumerable<ProductType> getListK()
        {
            IEnumerable<ProductType> list = db.ProductTypes.Where(m => m.type_id.Contains(Product.KID));
            return list;
        }
    }
}
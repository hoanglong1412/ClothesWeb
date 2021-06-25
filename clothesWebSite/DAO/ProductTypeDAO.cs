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
        MyDBContext db = new MyDBContext();

        public IEnumerable<ProductType> getList(int key)
        {
            IEnumerable<ProductType> list;
            if (key == 1)
            {
                list = db.ProductTypes.Where(m => m.type_id.Contains(Product.MALE));
                return list;
            }
            else if (key == 2)
            {
                list = db.ProductTypes.Where(m => m.type_id.Contains(Product.FEMALE));
                return list;
            }
            else
            {
                list = db.ProductTypes.Where(m => m.type_id.Contains(Product.KID));
                return list;
            }
        }
        public IEnumerable<ProductType> getList()
        {
            IEnumerable<ProductType> list = db.ProductTypes;
            return list;
        }
        public IEnumerable<ProductType> getListM()
        {
            IEnumerable<ProductType> list = db.ProductTypes.Where(m => m.type_id.Contains(Product.MALE));
            return list;
        }
        public IEnumerable<ProductType> getListF()
        {
            IEnumerable<ProductType> list = db.ProductTypes.Where(m => m.type_id.Contains(Product.FEMALE));
            return list;
        }
        public IEnumerable<ProductType> getListK()
        {
            IEnumerable<ProductType> list = db.ProductTypes.Where(m => m.type_id.Contains(Product.KID));
            return list;
        }
    }
}
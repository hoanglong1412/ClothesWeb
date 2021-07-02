using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using clothesWebSite.Models;
using clothesWebSite.DAO;

namespace clothesWebSite.Models
{
    public class ShoppingCart
    {
        ProductDAO productDAO = new ProductDAO();
        public int cart_product_id { get; set; }
        public string cart_product_name { get; set; }
        public string cart_image_name { get; set; }
        public double? cart_sale_price { get; set; }
        public double cart_discount { get; set; }
        public int cart_quantity { get; set; }

        public double? cart_total
        {
            get { return cart_sale_price * cart_quantity; }
        }

        public ShoppingCart(int product_id)
        {
            cart_product_id = product_id;
            Product product = productDAO.getOne(cart_product_id);
            cart_product_name = product.product_name;
            cart_image_name = product.image_name;
            if (ProductDAO.isSaleProduct(product))
            {
                cart_sale_price = product.sale_price - ProductDAO.getSalePrice(product);
            }
            else
            {
                cart_sale_price = product.sale_price;
            }
            cart_discount = 0.00;
            cart_quantity = 1;
        }
    }
}
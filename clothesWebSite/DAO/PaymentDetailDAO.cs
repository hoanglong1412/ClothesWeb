using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using clothesWebSite.Models;

namespace clothesWebSite.DAO
{
    public class PaymentDetailDAO
    {
        private MyDBContext db;
        public PaymentDetailDAO()
        {
            db = new MyDBContext();
        }
        public void addPaymentDetail(int Payment_id, int product_id, int quantity, double? price)
        {

            PaymentDetail paymentDetail = new PaymentDetail();
            paymentDetail.payment_id = Payment_id;
            paymentDetail.product_id = product_id;
            paymentDetail.quantity = quantity;
            paymentDetail.price = price;
            db.PaymentDetails.Add(paymentDetail);
            db.SaveChanges();
        }
        public IEnumerable<PaymentDetail> getList(int payment_id)
        {
            IEnumerable<PaymentDetail> list = db.PaymentDetails.Where(m => m.payment_id == payment_id);
            return list;
        }
    }
    //public float Alltotal(int Payment_id,int user_id)
    //{
    //}
}

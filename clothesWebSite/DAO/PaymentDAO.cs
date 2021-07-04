using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using clothesWebSite.Models;

namespace clothesWebSite.DAO
{
    public class PaymentDAO
    {

        private MyDBContext db;
        public PaymentDAO()
        {
            db = new MyDBContext();
        }
        public void addPayment(int id,String name,  String address, String phone, String delivery,String paymentmethod)
        {
            Payment payment = new Payment();
            payment.name_receiver = name;
            payment.address_deliver = address;
            payment.deliver_type = delivery;
            payment.payway = paymentmethod;
            payment.phone_receiver = phone;
            payment.state = 1;
            payment.user_id = id;
            db.Payments.Add(payment);
            db.SaveChanges();
        }
        public IEnumerable<Payment> getList(int user_id)
        {
            IEnumerable<Payment> list = db.Payments.Where(m => m.user_id == user_id).OrderByDescending(m => m.create_date);
            return list;
        }
        public Payment getOne(int payment_id)
        {
            Payment payment = db.Payments.Where(m => m.payment_id == payment_id).FirstOrDefault();
            return payment;
        }
        static public double getTotalPrice(Payment payment)
        {
            return payment.PaymentDetails.Sum(m => m.price * m.quantity ?? 0);
        }
    }
}
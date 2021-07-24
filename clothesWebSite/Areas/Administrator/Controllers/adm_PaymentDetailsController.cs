using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using clothesWebSite.Models;

namespace clothesWebSite.Areas.Administrator.Controllers
{
    public class adm_PaymentDetailsController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Administrator/adm_PaymentDetails
        public ActionResult Index()
        {
            if (Session["taikhoanadmin"] == null)
                return RedirectToAction("Login", "adm_MainPage");

            var paymentDetails = db.PaymentDetails.Include(p => p.Payment).Include(p => p.Product);
            return View(paymentDetails.ToList());
        }

        // GET: Administrator/adm_PaymentDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["taikhoanadmin"] == null)
                return RedirectToAction("Login", "adm_MainPage");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            return View(paymentDetail);
        }

        // GET: Administrator/adm_PaymentDetails/Create
        public ActionResult Create()
        {
            if (Session["taikhoanadmin"] == null)
                return RedirectToAction("Login", "adm_MainPage");

            ViewBag.payment_id = new SelectList(db.Payments, "payment_id", "deliver_name");
            ViewBag.product_id = new SelectList(db.Products, "product_id", "type_id");
            return View();
        }

        // POST: Administrator/adm_PaymentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ordinal_number,payment_id,product_id,size,color,quantity,price")] PaymentDetail paymentDetail)
        {
            if (Session["taikhoanadmin"] == null)
                return RedirectToAction("Login", "adm_MainPage");

            if (ModelState.IsValid)
            {
                db.PaymentDetails.Add(paymentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.payment_id = new SelectList(db.Payments, "payment_id", "deliver_name", paymentDetail.payment_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "type_id", paymentDetail.product_id);
            return View(paymentDetail);
        }

        // GET: Administrator/adm_PaymentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["taikhoanadmin"] == null)
                return RedirectToAction("Login", "adm_MainPage");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.payment_id = new SelectList(db.Payments, "payment_id", "deliver_name", paymentDetail.payment_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "type_id", paymentDetail.product_id);
            return View(paymentDetail);
        }

        // POST: Administrator/adm_PaymentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ordinal_number,payment_id,product_id,size,color,quantity,price")] PaymentDetail paymentDetail)
        {
            if (Session["taikhoanadmin"] == null)
                return RedirectToAction("Login", "adm_MainPage");

            if (ModelState.IsValid)
            {
                db.Entry(paymentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.payment_id = new SelectList(db.Payments, "payment_id", "deliver_name", paymentDetail.payment_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "type_id", paymentDetail.product_id);
            return View(paymentDetail);
        }

        // GET: Administrator/adm_PaymentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["taikhoanadmin"] == null)
                return RedirectToAction("Login", "adm_MainPage");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            return View(paymentDetail);
        }

        // POST: Administrator/adm_PaymentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["taikhoanadmin"] == null)
                return RedirectToAction("Login", "adm_MainPage");

            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            db.PaymentDetails.Remove(paymentDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

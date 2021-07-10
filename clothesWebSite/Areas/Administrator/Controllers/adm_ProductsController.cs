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
    public class adm_ProductsController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/adm_Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductType);
            return View(products.ToList());
        }

        // GET: Admin/adm_Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/adm_Products/Create
        public ActionResult Create()
        {
            ViewBag.type_id = new SelectList(db.ProductTypes, "type_id", "type_name");
            return View();
        }

        // POST: Admin/adm_Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,type_id,product_name,sale_price,import_price,count_views,total_rating,count_rating,content,state,last_update,create_day,image_name")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.type_id = new SelectList(db.ProductTypes, "type_id", "type_name", product.type_id);
            return View(product);
        }

        // GET: Admin/adm_Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.type_id = new SelectList(db.ProductTypes, "type_id", "type_name", product.type_id);
            return View(product);
        }

        // POST: Admin/adm_Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,type_id,product_name,sale_price,import_price,count_views,total_rating,count_rating,content,state,last_update,create_day,image_name")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.type_id = new SelectList(db.ProductTypes, "type_id", "type_name", product.type_id);
            return View(product);
        }

        // GET: Admin/adm_Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/adm_Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProvidenceClinics.Models;

namespace ProvidenceClinics.Controllers
{
    public class s_ProductController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Product
        [Authorize]
        public ActionResult Index()
        {
            var s_Product = db.s_Product.Include(s => s.s_Category).Include(s => s.s_Supplier);
            return View(s_Product.ToList());
        }

        // GET: s_Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Product s_Product = db.s_Product.Find(id);
            if (s_Product == null)
            {
                return HttpNotFound();
            }
            return View(s_Product);
        }

        // GET: s_Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.s_Category, "CatID", "CategoryName");
            ViewBag.SupplierID = new SelectList(db.s_Supplier, "SupplierID", "SupplierName");
            return View();
        }

        // POST: s_Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,image,CategoryID,SupplierID,Description,Price,QuantityInStock")] s_Product s_Product)
        {
            if (ModelState.IsValid)
            {
                db.s_Product.Add(s_Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.s_Category, "CatID", "CategoryName", s_Product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.s_Supplier, "SupplierID", "SupplierName", s_Product.SupplierID);
            return View(s_Product);
        }

        // GET: s_Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Product s_Product = db.s_Product.Find(id);
            if (s_Product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.s_Category, "CatID", "CategoryName", s_Product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.s_Supplier, "SupplierID", "SupplierName", s_Product.SupplierID);
            return View(s_Product);
        }

        // POST: s_Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,image,CategoryID,SupplierID,Description,Price,QuantityInStock")] s_Product s_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.s_Category, "CatID", "CategoryName", s_Product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.s_Supplier, "SupplierID", "SupplierName", s_Product.SupplierID);
            return View(s_Product);
        }

        // GET: s_Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Product s_Product = db.s_Product.Find(id);
            if (s_Product == null)
            {
                return HttpNotFound();
            }
            return View(s_Product);
        }

        // POST: s_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Product s_Product = db.s_Product.Find(id);
            db.s_Product.Remove(s_Product);
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

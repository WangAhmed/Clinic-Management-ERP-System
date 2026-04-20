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
    public class s_TransactionController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Transaction
        [Authorize]
        public ActionResult Index()
        {
            var s_Transaction = db.s_Transaction.Include(s => s.s_Order).Include(s => s.s_Supplier);
            return View(s_Transaction.ToList());
        }

        // GET: s_Transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Transaction s_Transaction = db.s_Transaction.Find(id);
            if (s_Transaction == null)
            {
                return HttpNotFound();
            }
            return View(s_Transaction);
        }

        // GET: s_Transaction/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.s_Order, "OrderID", "OrderDate");
            ViewBag.SupplierID = new SelectList(db.s_Supplier, "SupplierID", "SupplierName");
            return View();
        }

        // POST: s_Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,OrderID,ProductID,SupplierID,TransactionDate,Quantity,PurchasePrice,SellingPrice")] s_Transaction s_Transaction)
        {
            if (ModelState.IsValid)
            {
                db.s_Transaction.Add(s_Transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.s_Order, "OrderID", "OrderDate", s_Transaction.OrderID);
            ViewBag.SupplierID = new SelectList(db.s_Supplier, "SupplierID", "SupplierName", s_Transaction.SupplierID);
            return View(s_Transaction);
        }

        // GET: s_Transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Transaction s_Transaction = db.s_Transaction.Find(id);
            if (s_Transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.s_Order, "OrderID", "OrderDate", s_Transaction.OrderID);
            ViewBag.SupplierID = new SelectList(db.s_Supplier, "SupplierID", "SupplierName", s_Transaction.SupplierID);
            return View(s_Transaction);
        }

        // POST: s_Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,OrderID,ProductID,SupplierID,TransactionDate,Quantity,PurchasePrice,SellingPrice")] s_Transaction s_Transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.s_Order, "OrderID", "OrderDate", s_Transaction.OrderID);
            ViewBag.SupplierID = new SelectList(db.s_Supplier, "SupplierID", "SupplierName", s_Transaction.SupplierID);
            return View(s_Transaction);
        }

        // GET: s_Transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Transaction s_Transaction = db.s_Transaction.Find(id);
            if (s_Transaction == null)
            {
                return HttpNotFound();
            }
            return View(s_Transaction);
        }

        // POST: s_Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Transaction s_Transaction = db.s_Transaction.Find(id);
            db.s_Transaction.Remove(s_Transaction);
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

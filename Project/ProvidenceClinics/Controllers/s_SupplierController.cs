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
    public class s_SupplierController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Supplier
        [Authorize]
        public ActionResult Index()
        {
            return View(db.s_Supplier.ToList());
        }

        // GET: s_Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Supplier s_Supplier = db.s_Supplier.Find(id);
            if (s_Supplier == null)
            {
                return HttpNotFound();
            }
            return View(s_Supplier);
        }

        // GET: s_Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: s_Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierID,SupplierName,ContactNumber,Email,Address")] s_Supplier s_Supplier)
        {
            if (ModelState.IsValid)
            {
                db.s_Supplier.Add(s_Supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(s_Supplier);
        }

        // GET: s_Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Supplier s_Supplier = db.s_Supplier.Find(id);
            if (s_Supplier == null)
            {
                return HttpNotFound();
            }
            return View(s_Supplier);
        }

        // POST: s_Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierID,SupplierName,ContactNumber,Email,Address")] s_Supplier s_Supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s_Supplier);
        }

        // GET: s_Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Supplier s_Supplier = db.s_Supplier.Find(id);
            if (s_Supplier == null)
            {
                return HttpNotFound();
            }
            return View(s_Supplier);
        }

        // POST: s_Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Supplier s_Supplier = db.s_Supplier.Find(id);
            db.s_Supplier.Remove(s_Supplier);
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

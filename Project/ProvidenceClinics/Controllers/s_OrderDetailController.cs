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
    public class s_OrderDetailController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_OrderDetail
        [Authorize]
        public ActionResult Index()
        {
            var s_OrderDetail = db.s_OrderDetail.Include(s => s.s_Order);
            return View(s_OrderDetail.ToList());
        }

        // GET: s_OrderDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_OrderDetail s_OrderDetail = db.s_OrderDetail.Find(id);
            if (s_OrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(s_OrderDetail);
        }

        // GET: s_OrderDetail/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.s_Order, "OrderID", "OrderDate");
            return View();
        }

        // POST: s_OrderDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetailID,OrderID,ProductID,Quantity,UnitPrice")] s_OrderDetail s_OrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.s_OrderDetail.Add(s_OrderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.s_Order, "OrderID", "OrderDate", s_OrderDetail.OrderID);
            return View(s_OrderDetail);
        }

        // GET: s_OrderDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_OrderDetail s_OrderDetail = db.s_OrderDetail.Find(id);
            if (s_OrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.s_Order, "OrderID", "OrderDate", s_OrderDetail.OrderID);
            return View(s_OrderDetail);
        }

        // POST: s_OrderDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetailID,OrderID,ProductID,Quantity,UnitPrice")] s_OrderDetail s_OrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_OrderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.s_Order, "OrderID", "OrderDate", s_OrderDetail.OrderID);
            return View(s_OrderDetail);
        }

        // GET: s_OrderDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_OrderDetail s_OrderDetail = db.s_OrderDetail.Find(id);
            if (s_OrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(s_OrderDetail);
        }

        // POST: s_OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_OrderDetail s_OrderDetail = db.s_OrderDetail.Find(id);
            db.s_OrderDetail.Remove(s_OrderDetail);
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

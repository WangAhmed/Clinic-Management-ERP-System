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
    public class s_OrderController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Order
        [Authorize]
        public ActionResult Index()
        {
            var s_Order = db.s_Order.Include(s => s.s_Doctor).Include(s => s.s_Patient);
            return View(s_Order.ToList());
        }

        // GET: s_Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Order s_Order = db.s_Order.Find(id);
            if (s_Order == null)
            {
                return HttpNotFound();
            }
            return View(s_Order);
        }

        // GET: s_Order/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName");
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName");
            return View();
        }

        // POST: s_Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,PatientID,DoctorID,OrderDate,Status")] s_Order s_Order)
        {
            if (ModelState.IsValid)
            {
                db.s_Order.Add(s_Order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName", s_Order.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_Order.PatientID);
            return View(s_Order);
        }

        // GET: s_Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Order s_Order = db.s_Order.Find(id);
            if (s_Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName", s_Order.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_Order.PatientID);
            return View(s_Order);
        }

        // POST: s_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,PatientID,DoctorID,OrderDate,Status")] s_Order s_Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName", s_Order.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_Order.PatientID);
            return View(s_Order);
        }

        // GET: s_Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Order s_Order = db.s_Order.Find(id);
            if (s_Order == null)
            {
                return HttpNotFound();
            }
            return View(s_Order);
        }

        // POST: s_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Order s_Order = db.s_Order.Find(id);
            db.s_Order.Remove(s_Order);
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

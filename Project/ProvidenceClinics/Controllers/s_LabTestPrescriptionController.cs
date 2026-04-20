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
    public class s_LabTestPrescriptionController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_LabTestPrescription
        [Authorize]
        public ActionResult Index()
        {
            var s_LabTestPrescription = db.s_LabTestPrescription.Include(s => s.s_LabTest).Include(s => s.s_Prescription);
            return View(s_LabTestPrescription.ToList());
        }

        // GET: s_LabTestPrescription/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_LabTestPrescription s_LabTestPrescription = db.s_LabTestPrescription.Find(id);
            if (s_LabTestPrescription == null)
            {
                return HttpNotFound();
            }
            return View(s_LabTestPrescription);
        }

        // GET: s_LabTestPrescription/Create
        public ActionResult Create()
        {
            ViewBag.LabTestID = new SelectList(db.s_LabTest, "LabTestID", "TestName");
            ViewBag.PrescriptionID = new SelectList(db.s_Prescription, "PrescriptionID", "Dosage");
            return View();
        }

        // POST: s_LabTestPrescription/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LabTestPrescriptionID,LabTestID,PrescriptionID")] s_LabTestPrescription s_LabTestPrescription)
        {
            if (ModelState.IsValid)
            {
                db.s_LabTestPrescription.Add(s_LabTestPrescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LabTestID = new SelectList(db.s_LabTest, "LabTestID", "TestName", s_LabTestPrescription.LabTestID);
            ViewBag.PrescriptionID = new SelectList(db.s_Prescription, "PrescriptionID", "Dosage", s_LabTestPrescription.PrescriptionID);
            return View(s_LabTestPrescription);
        }

        // GET: s_LabTestPrescription/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_LabTestPrescription s_LabTestPrescription = db.s_LabTestPrescription.Find(id);
            if (s_LabTestPrescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.LabTestID = new SelectList(db.s_LabTest, "LabTestID", "TestName", s_LabTestPrescription.LabTestID);
            ViewBag.PrescriptionID = new SelectList(db.s_Prescription, "PrescriptionID", "Dosage", s_LabTestPrescription.PrescriptionID);
            return View(s_LabTestPrescription);
        }

        // POST: s_LabTestPrescription/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LabTestPrescriptionID,LabTestID,PrescriptionID")] s_LabTestPrescription s_LabTestPrescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_LabTestPrescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LabTestID = new SelectList(db.s_LabTest, "LabTestID", "TestName", s_LabTestPrescription.LabTestID);
            ViewBag.PrescriptionID = new SelectList(db.s_Prescription, "PrescriptionID", "Dosage", s_LabTestPrescription.PrescriptionID);
            return View(s_LabTestPrescription);
        }

        // GET: s_LabTestPrescription/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_LabTestPrescription s_LabTestPrescription = db.s_LabTestPrescription.Find(id);
            if (s_LabTestPrescription == null)
            {
                return HttpNotFound();
            }
            return View(s_LabTestPrescription);
        }

        // POST: s_LabTestPrescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_LabTestPrescription s_LabTestPrescription = db.s_LabTestPrescription.Find(id);
            db.s_LabTestPrescription.Remove(s_LabTestPrescription);
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

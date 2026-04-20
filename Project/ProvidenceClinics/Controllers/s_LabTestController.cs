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
    public class s_LabTestController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_LabTest
        [Authorize]
        public ActionResult Index()
        {
            var s_LabTest = db.s_LabTest.Include(s => s.s_Doctor).Include(s => s.s_Patient);
            return View(s_LabTest.ToList());
        }

        // GET: s_LabTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_LabTest s_LabTest = db.s_LabTest.Find(id);
            if (s_LabTest == null)
            {
                return HttpNotFound();
            }
            return View(s_LabTest);
        }

        // GET: s_LabTest/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName");
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName");
            return View();
        }

        // POST: s_LabTest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LabTestID,TestName,DoctorID,PatientID,TestDate,ChemicalsUsed,LabProductsUsed,Cost,Price")] s_LabTest s_LabTest)
        {
            if (ModelState.IsValid)
            {
                db.s_LabTest.Add(s_LabTest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName", s_LabTest.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_LabTest.PatientID);
            return View(s_LabTest);
        }

        // GET: s_LabTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_LabTest s_LabTest = db.s_LabTest.Find(id);
            if (s_LabTest == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName", s_LabTest.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_LabTest.PatientID);
            return View(s_LabTest);
        }

        // POST: s_LabTest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LabTestID,TestName,DoctorID,PatientID,TestDate,ChemicalsUsed,LabProductsUsed,Cost,Price")] s_LabTest s_LabTest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_LabTest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName", s_LabTest.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_LabTest.PatientID);
            return View(s_LabTest);
        }

        // GET: s_LabTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_LabTest s_LabTest = db.s_LabTest.Find(id);
            if (s_LabTest == null)
            {
                return HttpNotFound();
            }
            return View(s_LabTest);
        }

        // POST: s_LabTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_LabTest s_LabTest = db.s_LabTest.Find(id);
            db.s_LabTest.Remove(s_LabTest);
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

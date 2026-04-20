using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Appointment.Models;

namespace Appointment.Controllers
{
    public class s_LabTestController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_LabTest
        public ActionResult Index()
        {
            int? doctorId = Session["UserID"] as int?;
            if (doctorId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if session is null
            }

            var s_LabTest = db.s_LabTest.Include(s => s.s_Doctor).Include(s => s.s_Patient).Where(s => s.DoctorID == doctorId);
            return View(s_LabTest.ToList());
        }

        // GET: s_LabTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int? doctorId = Session["UserID"] as int?;
            if (doctorId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if session is null
            }

            s_LabTest s_LabTest = db.s_LabTest.Find(id);
            if (s_LabTest == null || s_LabTest.DoctorID != doctorId)
            {
                return HttpNotFound();
            }
            return View(s_LabTest);
        }

        // GET: s_LabTest/Create
        public ActionResult Create()
        {
            int? doctorId = Session["UserID"] as int?;
            if (doctorId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if session is null
            }

            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName");
            return View();
        }

        // POST: s_LabTest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LabTestID,TestName,PatientID,TestDate,ChemicalsUsed,LabProductsUsed,Cost,Price")] s_LabTest s_LabTest)
        {
            int? doctorId = Session["UserID"] as int?;
            if (doctorId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if session is null
            }

            if (ModelState.IsValid)
            {
                s_LabTest.DoctorID = doctorId.Value; // Set DoctorID from session
                db.s_LabTest.Add(s_LabTest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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

            int? doctorId = Session["UserID"] as int?;
            if (doctorId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if session is null
            }

            s_LabTest s_LabTest = db.s_LabTest.Find(id);
            if (s_LabTest == null || s_LabTest.DoctorID != doctorId)
            {
                return HttpNotFound();
            }

            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_LabTest.PatientID);
            return View(s_LabTest);
        }

        // POST: s_LabTest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LabTestID,TestName,PatientID,TestDate,ChemicalsUsed,LabProductsUsed,Cost,Price")] s_LabTest s_LabTest)
        {
            int? doctorId = Session["UserID"] as int?;
            if (doctorId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if session is null
            }

            if (ModelState.IsValid)
            {
                s_LabTest.DoctorID = doctorId.Value; // Ensure DoctorID is set from session
                db.Entry(s_LabTest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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

            int? doctorId = Session["UserID"] as int?;
            if (doctorId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if session is null
            }

            s_LabTest s_LabTest = db.s_LabTest.Find(id);
            if (s_LabTest == null || s_LabTest.DoctorID != doctorId)
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
            int? doctorId = Session["UserID"] as int?;
            if (doctorId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if session is null
            }

            s_LabTest s_LabTest = db.s_LabTest.Find(id);
            if (s_LabTest == null || s_LabTest.DoctorID != doctorId)
            {
                return HttpNotFound();
            }

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

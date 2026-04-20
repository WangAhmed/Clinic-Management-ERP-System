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
    public class s_PrescriptionController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Prescription
        public ActionResult Index()
        {
            var s_Prescription = db.s_Prescription.Include(s => s.m_medicines).Include(s => s.s_Appointment);
            return View(s_Prescription.ToList());
        }

        // GET: s_Prescription/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Prescription s_Prescription = db.s_Prescription.Find(id);
            if (s_Prescription == null)
            {
                return HttpNotFound();
            }
            return View(s_Prescription);
        }

        // GET: s_Prescription/Create
        public ActionResult Create()
        {
            ViewBag.MedicineID = new SelectList(db.m_medicines, "medid", "name");
            ViewBag.AppointmentID = new SelectList(db.s_Appointment, "AppointmentID", "AppointmentDate");
            return View();
        }

        // POST: s_Prescription/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrescriptionID,AppointmentID,MedicineID,Dosage,Duration,Notes")] s_Prescription s_Prescription)
        {
            if (ModelState.IsValid)
            {
                db.s_Prescription.Add(s_Prescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MedicineID = new SelectList(db.m_medicines, "medid", "name", s_Prescription.MedicineID);
            ViewBag.AppointmentID = new SelectList(db.s_Appointment, "AppointmentID", "AppointmentDate", s_Prescription.AppointmentID);
            return View(s_Prescription);
        }

        // GET: s_Prescription/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Prescription s_Prescription = db.s_Prescription.Find(id);
            if (s_Prescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicineID = new SelectList(db.m_medicines, "medid", "name", s_Prescription.MedicineID);
            ViewBag.AppointmentID = new SelectList(db.s_Appointment, "AppointmentID", "AppointmentDate", s_Prescription.AppointmentID);
            return View(s_Prescription);
        }

        // POST: s_Prescription/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrescriptionID,AppointmentID,MedicineID,Dosage,Duration,Notes")] s_Prescription s_Prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Prescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicineID = new SelectList(db.m_medicines, "medid", "name", s_Prescription.MedicineID);
            ViewBag.AppointmentID = new SelectList(db.s_Appointment, "AppointmentID", "AppointmentDate", s_Prescription.AppointmentID);
            return View(s_Prescription);
        }

        // GET: s_Prescription/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Prescription s_Prescription = db.s_Prescription.Find(id);
            if (s_Prescription == null)
            {
                return HttpNotFound();
            }
            return View(s_Prescription);
        }

        // POST: s_Prescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Prescription s_Prescription = db.s_Prescription.Find(id);
            db.s_Prescription.Remove(s_Prescription);
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

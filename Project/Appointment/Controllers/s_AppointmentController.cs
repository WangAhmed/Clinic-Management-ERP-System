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
    public class s_AppointmentController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Appointment
        public ActionResult Index()
        {
            var s_Appointment = db.s_Appointment.Include(s => s.s_Doctor).Include(s => s.s_Patient).Include(s => s.s_Status);
            return View(s_Appointment.ToList());
        }

        // GET: s_Appointment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Appointment s_Appointment = db.s_Appointment.Find(id);
            if (s_Appointment == null)
            {
                return HttpNotFound();
            }
            return View(s_Appointment);
        }

        // GET: s_Appointment/Create
        public ActionResult Create(int doctorId)
        {
            var patientId = Session["PatientID"] != null ? (int)Session["PatientID"] : (int?)null;

            if (patientId == null)
            {
                return RedirectToAction("Index", "Signin");
            }

            ViewBag.DoctorID = doctorId;
            ViewBag.PatientID = patientId;
            ViewBag.Statusid = new SelectList(db.s_Status, "StatusID", "StatusName");

            var appointment = new s_Appointment
            {
                DoctorID = doctorId,
                PatientID = patientId.Value
            };

            return View(appointment);
        }


        // POST: s_Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentID,PatientID,DoctorID,AppointmentDate,AppointmentTime,fee,Statusid")] s_Appointment s_Appointment)
        {
            if (ModelState.IsValid)
            {
                db.s_Appointment.Add(s_Appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID", "image", s_Appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_Appointment.PatientID);
            ViewBag.Statusid = new SelectList(db.s_Status, "StatusID", "StatusName", s_Appointment.Statusid);
            return View(s_Appointment);
        }

        // GET: s_Appointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Appointment s_Appointment = db.s_Appointment.Find(id);
            if (s_Appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID", "image", s_Appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_Appointment.PatientID);
            ViewBag.Statusid = new SelectList(db.s_Status, "StatusID", "StatusName", s_Appointment.Statusid);
            return View(s_Appointment);
        }

        // POST: s_Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentID,PatientID,DoctorID,AppointmentDate,AppointmentTime,fee,Statusid")] s_Appointment s_Appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID", "image", s_Appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_Appointment.PatientID);
            ViewBag.Statusid = new SelectList(db.s_Status, "StatusID", "StatusName", s_Appointment.Statusid);
            return View(s_Appointment);
        }

        // GET: s_Appointment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Appointment s_Appointment = db.s_Appointment.Find(id);
            if (s_Appointment == null)
            {
                return HttpNotFound();
            }
            return View(s_Appointment);
        }

        // POST: s_Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Appointment s_Appointment = db.s_Appointment.Find(id);
            db.s_Appointment.Remove(s_Appointment);
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

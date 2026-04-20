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
    public class s_FeedbackController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Feedback
        [Authorize]
        public ActionResult Index()
        {
            var s_Feedback = db.s_Feedback.Include(s => s.s_Doctor).Include(s => s.s_Patient);
            return View(s_Feedback.ToList());
        }

        // GET: s_Feedback/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Feedback s_Feedback = db.s_Feedback.Find(id);
            if (s_Feedback == null)
            {
                return HttpNotFound();
            }
            return View(s_Feedback);
        }

        // GET: s_Feedback/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName");
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName");
            return View();
        }

        // POST: s_Feedback/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeedbackID,PatientID,DoctorID,Comments,Rating")] s_Feedback s_Feedback)
        {
            if (ModelState.IsValid)
            {
                db.s_Feedback.Add(s_Feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName", s_Feedback.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_Feedback.PatientID);
            return View(s_Feedback);
        }

        // GET: s_Feedback/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Feedback s_Feedback = db.s_Feedback.Find(id);
            if (s_Feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName", s_Feedback.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_Feedback.PatientID);
            return View(s_Feedback);
        }

        // POST: s_Feedback/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeedbackID,PatientID,DoctorID,Comments,Rating")] s_Feedback s_Feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID","FirstName", s_Feedback.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_Feedback.PatientID);
            return View(s_Feedback);
        }

        // GET: s_Feedback/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Feedback s_Feedback = db.s_Feedback.Find(id);
            if (s_Feedback == null)
            {
                return HttpNotFound();
            }
            return View(s_Feedback);
        }

        // POST: s_Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Feedback s_Feedback = db.s_Feedback.Find(id);
            db.s_Feedback.Remove(s_Feedback);
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

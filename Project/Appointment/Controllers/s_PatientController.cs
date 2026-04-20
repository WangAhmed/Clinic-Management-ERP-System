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
    public class s_PatientController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Patient
        public ActionResult Index()
        {
            var s_Patient = db.s_Patient.Include(s => s.Register);
            return View(s_Patient.ToList());
        }

        // GET: s_Patient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Patient s_Patient = db.s_Patient.Find(id);
            if (s_Patient == null)
            {
                return HttpNotFound();
            }
            return View(s_Patient);
        }

        // GET: s_Patient/Create
        public ActionResult Create()
        {
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username");
            return View();
        }

        // POST: s_Patient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientID,regid,FirstName,LastName,image,DateOfBirth,Gender,ContactNumber,Email,Address")] s_Patient s_Patient)
        {
            if (ModelState.IsValid)
            {
                db.s_Patient.Add(s_Patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", s_Patient.regid);
            return View(s_Patient);
        }

        // GET: s_Patient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Patient s_Patient = db.s_Patient.Find(id);
            if (s_Patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", s_Patient.regid);
            return View(s_Patient);
        }

        // POST: s_Patient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientID,regid,FirstName,LastName,image,DateOfBirth,Gender,ContactNumber,Email,Address")] s_Patient s_Patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", s_Patient.regid);
            return View(s_Patient);
        }

        // GET: s_Patient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Patient s_Patient = db.s_Patient.Find(id);
            if (s_Patient == null)
            {
                return HttpNotFound();
            }
            return View(s_Patient);
        }

        // POST: s_Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Patient s_Patient = db.s_Patient.Find(id);
            db.s_Patient.Remove(s_Patient);
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

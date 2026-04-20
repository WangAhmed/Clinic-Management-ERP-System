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
    public class s_DoctorController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Doctor
        [Authorize]
        public ActionResult Index()
        {
            var s_Doctor = db.s_Doctor.Include(s => s.Register).Include(s => s.s_Specialist);
            return View(s_Doctor.ToList());
        }

        // GET: s_Doctor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Doctor s_Doctor = db.s_Doctor.Find(id);
            if (s_Doctor == null)
            {
                return HttpNotFound();
            }
            return View(s_Doctor);
        }

        // GET: s_Doctor/Create
        public ActionResult Create()
        {
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username");
            ViewBag.SpecialistID = new SelectList(db.s_Specialist, "SpecialistID", "SpecialistName");
            return View();
        }

        // POST: s_Doctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorID,regid,image,FirstName,LastName,SpecialistID,ContactNumber,Email")] s_Doctor s_Doctor)
        {
            if (ModelState.IsValid)
            {
                db.s_Doctor.Add(s_Doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", s_Doctor.regid);
            ViewBag.SpecialistID = new SelectList(db.s_Specialist, "SpecialistID", "SpecialistName", s_Doctor.SpecialistID);
            return View(s_Doctor);
        }

        // GET: s_Doctor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Doctor s_Doctor = db.s_Doctor.Find(id);
            if (s_Doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", s_Doctor.regid);
            ViewBag.SpecialistID = new SelectList(db.s_Specialist, "SpecialistID", "SpecialistName", s_Doctor.SpecialistID);
            return View(s_Doctor);
        }

        // POST: s_Doctor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorID,regid,image,FirstName,LastName,SpecialistID,ContactNumber,Email")] s_Doctor s_Doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", s_Doctor.regid);
            ViewBag.SpecialistID = new SelectList(db.s_Specialist, "SpecialistID", "SpecialistName", s_Doctor.SpecialistID);
            return View(s_Doctor);
        }

        // GET: s_Doctor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Doctor s_Doctor = db.s_Doctor.Find(id);
            if (s_Doctor == null)
            {
                return HttpNotFound();
            }
            return View(s_Doctor);
        }

        // POST: s_Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Doctor s_Doctor = db.s_Doctor.Find(id);
            db.s_Doctor.Remove(s_Doctor);
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

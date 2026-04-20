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
    public class s_AppointmentDetailController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_AppointmentDetail
        [Authorize]
        public ActionResult Index()
        {
            var s_AppointmentDetail = db.s_AppointmentDetail.Include(s => s.s_Appointment);
            return View(s_AppointmentDetail.ToList());
        }

        // GET: s_AppointmentDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_AppointmentDetail s_AppointmentDetail = db.s_AppointmentDetail.Find(id);
            if (s_AppointmentDetail == null)
            {
                return HttpNotFound();
            }
            return View(s_AppointmentDetail);
        }

        // GET: s_AppointmentDetail/Create
        public ActionResult Create()
        {
            ViewBag.AppointmentID = new SelectList(db.s_Appointment, "AppointmentID", "AppointmentDate");
            return View();
        }

        // POST: s_AppointmentDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentDetailID,AppointmentID,Detail")] s_AppointmentDetail s_AppointmentDetail)
        {
            if (ModelState.IsValid)
            {
                db.s_AppointmentDetail.Add(s_AppointmentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppointmentID = new SelectList(db.s_Appointment, "AppointmentID", "AppointmentDate", s_AppointmentDetail.AppointmentID);
            return View(s_AppointmentDetail);
        }

        // GET: s_AppointmentDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_AppointmentDetail s_AppointmentDetail = db.s_AppointmentDetail.Find(id);
            if (s_AppointmentDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppointmentID = new SelectList(db.s_Appointment, "AppointmentID", "AppointmentDate", s_AppointmentDetail.AppointmentID);
            return View(s_AppointmentDetail);
        }

        // POST: s_AppointmentDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentDetailID,AppointmentID,Detail")] s_AppointmentDetail s_AppointmentDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_AppointmentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppointmentID = new SelectList(db.s_Appointment, "AppointmentID", "AppointmentDate", s_AppointmentDetail.AppointmentID);
            return View(s_AppointmentDetail);
        }

        // GET: s_AppointmentDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_AppointmentDetail s_AppointmentDetail = db.s_AppointmentDetail.Find(id);
            if (s_AppointmentDetail == null)
            {
                return HttpNotFound();
            }
            return View(s_AppointmentDetail);
        }

        // POST: s_AppointmentDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_AppointmentDetail s_AppointmentDetail = db.s_AppointmentDetail.Find(id);
            db.s_AppointmentDetail.Remove(s_AppointmentDetail);
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

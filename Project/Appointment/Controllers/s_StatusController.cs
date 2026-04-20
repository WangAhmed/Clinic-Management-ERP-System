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
    public class s_StatusController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Status
        public ActionResult Index()
        {
            return View(db.s_Status.ToList());
        }

        // GET: s_Status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Status s_Status = db.s_Status.Find(id);
            if (s_Status == null)
            {
                return HttpNotFound();
            }
            return View(s_Status);
        }

        // GET: s_Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: s_Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusID,StatusName")] s_Status s_Status)
        {
            if (ModelState.IsValid)
            {
                db.s_Status.Add(s_Status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(s_Status);
        }

        // GET: s_Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Status s_Status = db.s_Status.Find(id);
            if (s_Status == null)
            {
                return HttpNotFound();
            }
            return View(s_Status);
        }

        // POST: s_Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusID,StatusName")] s_Status s_Status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s_Status);
        }

        // GET: s_Status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Status s_Status = db.s_Status.Find(id);
            if (s_Status == null)
            {
                return HttpNotFound();
            }
            return View(s_Status);
        }

        // POST: s_Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Status s_Status = db.s_Status.Find(id);
            db.s_Status.Remove(s_Status);
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

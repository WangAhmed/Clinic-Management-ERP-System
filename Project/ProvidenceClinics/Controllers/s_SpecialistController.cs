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
    public class s_SpecialistController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Specialist
        [Authorize]
        public ActionResult Index()
        {
            return View(db.s_Specialist.ToList());
        }

        // GET: s_Specialist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Specialist s_Specialist = db.s_Specialist.Find(id);
            if (s_Specialist == null)
            {
                return HttpNotFound();
            }
            return View(s_Specialist);
        }

        // GET: s_Specialist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: s_Specialist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpecialistID,SpecialistName")] s_Specialist s_Specialist)
        {
            if (ModelState.IsValid)
            {
                db.s_Specialist.Add(s_Specialist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(s_Specialist);
        }

        // GET: s_Specialist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Specialist s_Specialist = db.s_Specialist.Find(id);
            if (s_Specialist == null)
            {
                return HttpNotFound();
            }
            return View(s_Specialist);
        }

        // POST: s_Specialist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpecialistID,SpecialistName")] s_Specialist s_Specialist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Specialist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s_Specialist);
        }

        // GET: s_Specialist/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Specialist s_Specialist = db.s_Specialist.Find(id);
            if (s_Specialist == null)
            {
                return HttpNotFound();
            }
            return View(s_Specialist);
        }

        // POST: s_Specialist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Specialist s_Specialist = db.s_Specialist.Find(id);
            db.s_Specialist.Remove(s_Specialist);
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

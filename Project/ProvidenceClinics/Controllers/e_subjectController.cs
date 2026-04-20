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
    public class e_subjectController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: e_subject
        [Authorize]
        public ActionResult Index()
        {
            return View(db.e_subject.ToList());
        }

        // GET: e_subject/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_subject e_subject = db.e_subject.Find(id);
            if (e_subject == null)
            {
                return HttpNotFound();
            }
            return View(e_subject);
        }

        // GET: e_subject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: e_subject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "subid,name")] e_subject e_subject)
        {
            if (ModelState.IsValid)
            {
                db.e_subject.Add(e_subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(e_subject);
        }

        // GET: e_subject/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_subject e_subject = db.e_subject.Find(id);
            if (e_subject == null)
            {
                return HttpNotFound();
            }
            return View(e_subject);
        }

        // POST: e_subject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "subid,name")] e_subject e_subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(e_subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(e_subject);
        }

        // GET: e_subject/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_subject e_subject = db.e_subject.Find(id);
            if (e_subject == null)
            {
                return HttpNotFound();
            }
            return View(e_subject);
        }

        // POST: e_subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            e_subject e_subject = db.e_subject.Find(id);
            db.e_subject.Remove(e_subject);
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

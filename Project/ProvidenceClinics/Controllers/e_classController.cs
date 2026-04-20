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
    public class e_classController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: e_class
        [Authorize]
        public ActionResult Index()
        {
            return View(db.e_class.ToList());
        }

        // GET: e_class/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_class e_class = db.e_class.Find(id);
            if (e_class == null)
            {
                return HttpNotFound();
            }
            return View(e_class);
        }

        // GET: e_class/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: e_class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "classid,name,location")] e_class e_class)
        {
            if (ModelState.IsValid)
            {
                db.e_class.Add(e_class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(e_class);
        }

        // GET: e_class/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_class e_class = db.e_class.Find(id);
            if (e_class == null)
            {
                return HttpNotFound();
            }
            return View(e_class);
        }

        // POST: e_class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "classid,name,location")] e_class e_class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(e_class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(e_class);
        }

        // GET: e_class/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_class e_class = db.e_class.Find(id);
            if (e_class == null)
            {
                return HttpNotFound();
            }
            return View(e_class);
        }

        // POST: e_class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            e_class e_class = db.e_class.Find(id);
            db.e_class.Remove(e_class);
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

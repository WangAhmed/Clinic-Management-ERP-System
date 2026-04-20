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
    public class e_staff_typeController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: e_staff_type
        [Authorize]
        public ActionResult Index()
        {
            return View(db.e_staff_type.ToList());
        }

        // GET: e_staff_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_staff_type e_staff_type = db.e_staff_type.Find(id);
            if (e_staff_type == null)
            {
                return HttpNotFound();
            }
            return View(e_staff_type);
        }

        // GET: e_staff_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: e_staff_type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stype_id,type")] e_staff_type e_staff_type)
        {
            if (ModelState.IsValid)
            {
                db.e_staff_type.Add(e_staff_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(e_staff_type);
        }

        // GET: e_staff_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_staff_type e_staff_type = db.e_staff_type.Find(id);
            if (e_staff_type == null)
            {
                return HttpNotFound();
            }
            return View(e_staff_type);
        }

        // POST: e_staff_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stype_id,type")] e_staff_type e_staff_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(e_staff_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(e_staff_type);
        }

        // GET: e_staff_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_staff_type e_staff_type = db.e_staff_type.Find(id);
            if (e_staff_type == null)
            {
                return HttpNotFound();
            }
            return View(e_staff_type);
        }

        // POST: e_staff_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            e_staff_type e_staff_type = db.e_staff_type.Find(id);
            db.e_staff_type.Remove(e_staff_type);
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

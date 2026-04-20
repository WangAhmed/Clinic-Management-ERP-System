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
    public class e_staffController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: e_staff
        [Authorize]
        public ActionResult Index()
        {
            var e_staff = db.e_staff.Include(e => e.e_staff_type).Include(e => e.Register);
            return View(e_staff.ToList());
        }

        // GET: e_staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_staff e_staff = db.e_staff.Find(id);
            if (e_staff == null)
            {
                return HttpNotFound();
            }
            return View(e_staff);
        }

        // GET: e_staff/Create
        public ActionResult Create()
        {
            ViewBag.stype_id = new SelectList(db.e_staff_type, "stype_id", "type");
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username");
            return View();
        }

        // POST: e_staff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "proid,regid,name,image,stype_id,qualification")] e_staff e_staff)
        {
            if (ModelState.IsValid)
            {
                db.e_staff.Add(e_staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.stype_id = new SelectList(db.e_staff_type, "stype_id", "type", e_staff.stype_id);
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", e_staff.regid);
            return View(e_staff);
        }

        // GET: e_staff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_staff e_staff = db.e_staff.Find(id);
            if (e_staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.stype_id = new SelectList(db.e_staff_type, "stype_id", "type", e_staff.stype_id);
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", e_staff.regid);
            return View(e_staff);
        }

        // POST: e_staff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "proid,regid,name,image,stype_id,qualification")] e_staff e_staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(e_staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stype_id = new SelectList(db.e_staff_type, "stype_id", "type", e_staff.stype_id);
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", e_staff.regid);
            return View(e_staff);
        }

        // GET: e_staff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_staff e_staff = db.e_staff.Find(id);
            if (e_staff == null)
            {
                return HttpNotFound();
            }
            return View(e_staff);
        }

        // POST: e_staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            e_staff e_staff = db.e_staff.Find(id);
            db.e_staff.Remove(e_staff);
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

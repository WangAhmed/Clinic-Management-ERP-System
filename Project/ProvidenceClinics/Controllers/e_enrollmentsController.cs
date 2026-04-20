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
    public class e_enrollmentsController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: e_enrollments
        [Authorize]
        public ActionResult Index()
        {
            var e_enrollments = db.e_enrollments.Include(e => e.e_class).Include(e => e.e_lsp).Include(e => e.e_students).Include(e => e.e_staff).Include(e => e.e_subject);
            return View(e_enrollments.ToList());
        }

        // GET: e_enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_enrollments e_enrollments = db.e_enrollments.Find(id);
            if (e_enrollments == null)
            {
                return HttpNotFound();
            }
            return View(e_enrollments);
        }

        // GET: e_enrollments/Create
        public ActionResult Create()
        {
            ViewBag.classid = new SelectList(db.e_class, "classid", "name");
            ViewBag.lspid = new SelectList(db.e_lsp, "lspid", "name");
            ViewBag.stdid = new SelectList(db.e_students, "stdid", "name");
            ViewBag.proid = new SelectList(db.e_staff, "proid", "name");
            ViewBag.subid = new SelectList(db.e_subject, "subid", "name");
            return View();
        }

        // POST: e_enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "enid,regid,stdid,classid,subid,lspid,proid,enrollment_date,fee")] e_enrollments e_enrollments)
        {
            if (ModelState.IsValid)
            {
                db.e_enrollments.Add(e_enrollments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.classid = new SelectList(db.e_class, "classid", "name", e_enrollments.classid);
            ViewBag.lspid = new SelectList(db.e_lsp, "lspid", "name", e_enrollments.lspid);
            ViewBag.stdid = new SelectList(db.e_students, "stdid", "name", e_enrollments.stdid);
            ViewBag.proid = new SelectList(db.e_staff, "proid", "name", e_enrollments.proid);
            ViewBag.subid = new SelectList(db.e_subject, "subid", "name", e_enrollments.subid);
            return View(e_enrollments);
        }

        // GET: e_enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_enrollments e_enrollments = db.e_enrollments.Find(id);
            if (e_enrollments == null)
            {
                return HttpNotFound();
            }
            ViewBag.classid = new SelectList(db.e_class, "classid", "name", e_enrollments.classid);
            ViewBag.lspid = new SelectList(db.e_lsp, "lspid", "name", e_enrollments.lspid);
            ViewBag.stdid = new SelectList(db.e_students, "stdid", "name", e_enrollments.stdid);
            ViewBag.proid = new SelectList(db.e_staff, "proid", "name", e_enrollments.proid);
            ViewBag.subid = new SelectList(db.e_subject, "subid", "name", e_enrollments.subid);
            return View(e_enrollments);
        }

        // POST: e_enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "enid,regid,stdid,classid,subid,lspid,proid,enrollment_date,fee")] e_enrollments e_enrollments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(e_enrollments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.classid = new SelectList(db.e_class, "classid", "name", e_enrollments.classid);
            ViewBag.lspid = new SelectList(db.e_lsp, "lspid", "name", e_enrollments.lspid);
            ViewBag.stdid = new SelectList(db.e_students, "stdid", "name", e_enrollments.stdid);
            ViewBag.proid = new SelectList(db.e_staff, "proid", "name", e_enrollments.proid);
            ViewBag.subid = new SelectList(db.e_subject, "subid", "name", e_enrollments.subid);
            return View(e_enrollments);
        }

        // GET: e_enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_enrollments e_enrollments = db.e_enrollments.Find(id);
            if (e_enrollments == null)
            {
                return HttpNotFound();
            }
            return View(e_enrollments);
        }

        // POST: e_enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            e_enrollments e_enrollments = db.e_enrollments.Find(id);
            db.e_enrollments.Remove(e_enrollments);
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

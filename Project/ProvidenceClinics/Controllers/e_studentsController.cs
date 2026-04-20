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
    public class e_studentsController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: e_students
        [Authorize]
        public ActionResult Index()
        {
            var e_students = db.e_students.Include(e => e.Register);
            return View(e_students.ToList());
        }

        // GET: e_students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_students e_students = db.e_students.Find(id);
            if (e_students == null)
            {
                return HttpNotFound();
            }
            return View(e_students);
        }

        // GET: e_students/Create
        public ActionResult Create()
        {
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username");
            return View();
        }

        // POST: e_students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stdid,regid,name,f_name,image,age")] e_students e_students)
        {
            if (ModelState.IsValid)
            {
                db.e_students.Add(e_students);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", e_students.regid);
            return View(e_students);
        }

        // GET: e_students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_students e_students = db.e_students.Find(id);
            if (e_students == null)
            {
                return HttpNotFound();
            }
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", e_students.regid);
            return View(e_students);
        }

        // POST: e_students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stdid,regid,name,f_name,image,age")] e_students e_students)
        {
            if (ModelState.IsValid)
            {
                db.Entry(e_students).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", e_students.regid);
            return View(e_students);
        }

        // GET: e_students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_students e_students = db.e_students.Find(id);
            if (e_students == null)
            {
                return HttpNotFound();
            }
            return View(e_students);
        }

        // POST: e_students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            e_students e_students = db.e_students.Find(id);
            db.e_students.Remove(e_students);
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

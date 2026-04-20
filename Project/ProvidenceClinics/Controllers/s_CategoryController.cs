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
    public class s_CategoryController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Category
        [Authorize]
        public ActionResult Index()
        {
            return View(db.s_Category.ToList());
        }

        // GET: s_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Category s_Category = db.s_Category.Find(id);
            if (s_Category == null)
            {
                return HttpNotFound();
            }
            return View(s_Category);
        }

        // GET: s_Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: s_Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatID,CategoryName,Description")] s_Category s_Category)
        {
            if (ModelState.IsValid)
            {
                db.s_Category.Add(s_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(s_Category);
        }

        // GET: s_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Category s_Category = db.s_Category.Find(id);
            if (s_Category == null)
            {
                return HttpNotFound();
            }
            return View(s_Category);
        }

        // POST: s_Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatID,CategoryName,Description")] s_Category s_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s_Category);
        }

        // GET: s_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Category s_Category = db.s_Category.Find(id);
            if (s_Category == null)
            {
                return HttpNotFound();
            }
            return View(s_Category);
        }

        // POST: s_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Category s_Category = db.s_Category.Find(id);
            db.s_Category.Remove(s_Category);
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

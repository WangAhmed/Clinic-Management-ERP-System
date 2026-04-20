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
    public class m_categoriesController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: m_categories
        [Authorize]
        public ActionResult Index()
        {
            return View(db.m_categories.ToList());
        }

        // GET: m_categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_categories m_categories = db.m_categories.Find(id);
            if (m_categories == null)
            {
                return HttpNotFound();
            }
            return View(m_categories);
        }

        // GET: m_categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: m_categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "m_catid,name")] m_categories m_categories)
        {
            if (ModelState.IsValid)
            {
                db.m_categories.Add(m_categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m_categories);
        }

        // GET: m_categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_categories m_categories = db.m_categories.Find(id);
            if (m_categories == null)
            {
                return HttpNotFound();
            }
            return View(m_categories);
        }

        // POST: m_categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "m_catid,name")] m_categories m_categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_categories);
        }

        // GET: m_categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_categories m_categories = db.m_categories.Find(id);
            if (m_categories == null)
            {
                return HttpNotFound();
            }
            return View(m_categories);
        }

        // POST: m_categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_categories m_categories = db.m_categories.Find(id);
            db.m_categories.Remove(m_categories);
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

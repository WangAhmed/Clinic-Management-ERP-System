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
    public class m_typesController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: m_types
        [Authorize]
        public ActionResult Index()
        {
            return View(db.m_types.ToList());
        }

        // GET: m_types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_types m_types = db.m_types.Find(id);
            if (m_types == null)
            {
                return HttpNotFound();
            }
            return View(m_types);
        }

        // GET: m_types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: m_types/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "medtypeid,type")] m_types m_types)
        {
            if (ModelState.IsValid)
            {
                db.m_types.Add(m_types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m_types);
        }

        // GET: m_types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_types m_types = db.m_types.Find(id);
            if (m_types == null)
            {
                return HttpNotFound();
            }
            return View(m_types);
        }

        // POST: m_types/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "medtypeid,type")] m_types m_types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_types);
        }

        // GET: m_types/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_types m_types = db.m_types.Find(id);
            if (m_types == null)
            {
                return HttpNotFound();
            }
            return View(m_types);
        }

        // POST: m_types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_types m_types = db.m_types.Find(id);
            db.m_types.Remove(m_types);
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

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
    public class m_suppliersController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: m_suppliers
        [Authorize]
        public ActionResult Index()
        {
            return View(db.m_suppliers.ToList());
        }

        // GET: m_suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_suppliers m_suppliers = db.m_suppliers.Find(id);
            if (m_suppliers == null)
            {
                return HttpNotFound();
            }
            return View(m_suppliers);
        }

        // GET: m_suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: m_suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "m_supid,name,contact_name,address,phone")] m_suppliers m_suppliers)
        {
            if (ModelState.IsValid)
            {
                db.m_suppliers.Add(m_suppliers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m_suppliers);
        }

        // GET: m_suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_suppliers m_suppliers = db.m_suppliers.Find(id);
            if (m_suppliers == null)
            {
                return HttpNotFound();
            }
            return View(m_suppliers);
        }

        // POST: m_suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "m_supid,name,contact_name,address,phone")] m_suppliers m_suppliers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_suppliers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_suppliers);
        }

        // GET: m_suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_suppliers m_suppliers = db.m_suppliers.Find(id);
            if (m_suppliers == null)
            {
                return HttpNotFound();
            }
            return View(m_suppliers);
        }

        // POST: m_suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_suppliers m_suppliers = db.m_suppliers.Find(id);
            db.m_suppliers.Remove(m_suppliers);
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

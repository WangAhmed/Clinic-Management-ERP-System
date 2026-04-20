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
    public class m_medicinesController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: m_medicines
        [Authorize]
        public ActionResult Index()
        {
            var m_medicines = db.m_medicines.Include(m => m.m_categories).Include(m => m.m_suppliers).Include(m => m.m_types);
            return View(m_medicines.ToList());
        }

        // GET: m_medicines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_medicines m_medicines = db.m_medicines.Find(id);
            if (m_medicines == null)
            {
                return HttpNotFound();
            }
            return View(m_medicines);
        }

        // GET: m_medicines/Create
        public ActionResult Create()
        {
            ViewBag.catid = new SelectList(db.m_categories, "m_catid", "name");
            ViewBag.m_supid = new SelectList(db.m_suppliers, "m_supid", "name");
            ViewBag.medtypeid = new SelectList(db.m_types, "medtypeid", "type");
            return View();
        }

        // POST: m_medicines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "medid,name,image,medtypeid,catid,m_supid,price,quantity_in_stock")] m_medicines m_medicines)
        {
            if (ModelState.IsValid)
            {
                db.m_medicines.Add(m_medicines);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.catid = new SelectList(db.m_categories, "m_catid", "name", m_medicines.catid);
            ViewBag.m_supid = new SelectList(db.m_suppliers, "m_supid", "name", m_medicines.m_supid);
            ViewBag.medtypeid = new SelectList(db.m_types, "medtypeid", "type", m_medicines.medtypeid);
            return View(m_medicines);
        }

        // GET: m_medicines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_medicines m_medicines = db.m_medicines.Find(id);
            if (m_medicines == null)
            {
                return HttpNotFound();
            }
            ViewBag.catid = new SelectList(db.m_categories, "m_catid", "name", m_medicines.catid);
            ViewBag.m_supid = new SelectList(db.m_suppliers, "m_supid", "name", m_medicines.m_supid);
            ViewBag.medtypeid = new SelectList(db.m_types, "medtypeid", "type", m_medicines.medtypeid);
            return View(m_medicines);
        }

        // POST: m_medicines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "medid,name,image,medtypeid,catid,m_supid,price,quantity_in_stock")] m_medicines m_medicines)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_medicines).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.catid = new SelectList(db.m_categories, "m_catid", "name", m_medicines.catid);
            ViewBag.m_supid = new SelectList(db.m_suppliers, "m_supid", "name", m_medicines.m_supid);
            ViewBag.medtypeid = new SelectList(db.m_types, "medtypeid", "type", m_medicines.medtypeid);
            return View(m_medicines);
        }

        // GET: m_medicines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_medicines m_medicines = db.m_medicines.Find(id);
            if (m_medicines == null)
            {
                return HttpNotFound();
            }
            return View(m_medicines);
        }

        // POST: m_medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_medicines m_medicines = db.m_medicines.Find(id);
            db.m_medicines.Remove(m_medicines);
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

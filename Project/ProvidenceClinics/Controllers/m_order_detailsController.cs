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
    public class m_order_detailsController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: m_order_details
        [Authorize]
        public ActionResult Index()
        {
            var m_order_details = db.m_order_details.Include(m => m.m_medicines).Include(m => m.m_orders);
            return View(m_order_details.ToList());
        }

        // GET: m_order_details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_order_details m_order_details = db.m_order_details.Find(id);
            if (m_order_details == null)
            {
                return HttpNotFound();
            }
            return View(m_order_details);
        }

        // GET: m_order_details/Create
        public ActionResult Create()
        {
            ViewBag.medicine_id = new SelectList(db.m_medicines, "medid", "name");
            ViewBag.order_id = new SelectList(db.m_orders, "oid", "ordertype");
            return View();
        }

        // POST: m_order_details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "odid,order_id,medicine_id,quantity,unit_price")] m_order_details m_order_details)
        {
            if (ModelState.IsValid)
            {
                db.m_order_details.Add(m_order_details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.medicine_id = new SelectList(db.m_medicines, "medid", "name", m_order_details.medicine_id);
            ViewBag.order_id = new SelectList(db.m_orders, "oid", "ordertype", m_order_details.order_id);
            return View(m_order_details);
        }

        // GET: m_order_details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_order_details m_order_details = db.m_order_details.Find(id);
            if (m_order_details == null)
            {
                return HttpNotFound();
            }
            ViewBag.medicine_id = new SelectList(db.m_medicines, "medid", "name", m_order_details.medicine_id);
            ViewBag.order_id = new SelectList(db.m_orders, "oid", "ordertype", m_order_details.order_id);
            return View(m_order_details);
        }

        // POST: m_order_details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "odid,order_id,medicine_id,quantity,unit_price")] m_order_details m_order_details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_order_details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.medicine_id = new SelectList(db.m_medicines, "medid", "name", m_order_details.medicine_id);
            ViewBag.order_id = new SelectList(db.m_orders, "oid", "ordertype", m_order_details.order_id);
            return View(m_order_details);
        }

        // GET: m_order_details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_order_details m_order_details = db.m_order_details.Find(id);
            if (m_order_details == null)
            {
                return HttpNotFound();
            }
            return View(m_order_details);
        }

        // POST: m_order_details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_order_details m_order_details = db.m_order_details.Find(id);
            db.m_order_details.Remove(m_order_details);
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

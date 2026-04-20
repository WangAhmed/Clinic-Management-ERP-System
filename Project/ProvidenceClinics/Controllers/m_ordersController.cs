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
    public class m_ordersController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: m_orders
        [Authorize]
        public ActionResult Index()
        {
            var m_orders = db.m_orders.Include(m => m.m_customers);
            return View(m_orders.ToList());
        }

        // GET: m_orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_orders m_orders = db.m_orders.Find(id);
            if (m_orders == null)
            {
                return HttpNotFound();
            }
            return View(m_orders);
        }

        // GET: m_orders/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.m_customers, "cusid","FirstName");
            return View();
        }

        // POST: m_orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "oid,customer_id,order_date,ordertype")] m_orders m_orders)
        {
            if (ModelState.IsValid)
            {
                db.m_orders.Add(m_orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.m_customers, "cusid","FirstName", m_orders.customer_id);
            return View(m_orders);
        }

        // GET: m_orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_orders m_orders = db.m_orders.Find(id);
            if (m_orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.m_customers, "cusid","FirstName", m_orders.customer_id);
            return View(m_orders);
        }

        // POST: m_orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "oid,customer_id,order_date,ordertype")] m_orders m_orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.m_customers, "cusid","FirstName", m_orders.customer_id);
            return View(m_orders);
        }

        // GET: m_orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_orders m_orders = db.m_orders.Find(id);
            if (m_orders == null)
            {
                return HttpNotFound();
            }
            return View(m_orders);
        }

        // POST: m_orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_orders m_orders = db.m_orders.Find(id);
            db.m_orders.Remove(m_orders);
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

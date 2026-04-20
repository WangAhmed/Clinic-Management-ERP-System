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
    public class m_customersController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: m_customers
        [Authorize]
        public ActionResult Index()
        {
            var m_customers = db.m_customers.Include(m => m.Register);
            return View(m_customers.ToList());
        }

        // GET: m_customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_customers m_customers = db.m_customers.Find(id);
            if (m_customers == null)
            {
                return HttpNotFound();
            }
            return View(m_customers);
        }

        // GET: m_customers/Create
        public ActionResult Create()
        {
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username");
            return View();
        }

        // POST: m_customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cusid,regid,image,name,address,phone")] m_customers m_customers)
        {
            if (ModelState.IsValid)
            {
                db.m_customers.Add(m_customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", m_customers.regid);
            return View(m_customers);
        }

        // GET: m_customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_customers m_customers = db.m_customers.Find(id);
            if (m_customers == null)
            {
                return HttpNotFound();
            }
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", m_customers.regid);
            return View(m_customers);
        }

        // POST: m_customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cusid,regid,image,name,address,phone")] m_customers m_customers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_customers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", m_customers.regid);
            return View(m_customers);
        }

        // GET: m_customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_customers m_customers = db.m_customers.Find(id);
            if (m_customers == null)
            {
                return HttpNotFound();
            }
            return View(m_customers);
        }

        // POST: m_customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_customers m_customers = db.m_customers.Find(id);
            db.m_customers.Remove(m_customers);
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

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
    public class s_ContactController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_Contact
        [Authorize]
        public ActionResult Index()
        {
            return View(db.s_Contact.ToList());
        }

        // GET: s_Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Contact s_Contact = db.s_Contact.Find(id);
            if (s_Contact == null)
            {
                return HttpNotFound();
            }
            return View(s_Contact);
        }

        // GET: s_Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: s_Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactID,Name,Email,Message")] s_Contact s_Contact)
        {
            if (ModelState.IsValid)
            {
                db.s_Contact.Add(s_Contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(s_Contact);
        }

        // GET: s_Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Contact s_Contact = db.s_Contact.Find(id);
            if (s_Contact == null)
            {
                return HttpNotFound();
            }
            return View(s_Contact);
        }

        // POST: s_Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactID,Name,Email,Message")] s_Contact s_Contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_Contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s_Contact);
        }

        // GET: s_Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Contact s_Contact = db.s_Contact.Find(id);
            if (s_Contact == null)
            {
                return HttpNotFound();
            }
            return View(s_Contact);
        }

        // POST: s_Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_Contact s_Contact = db.s_Contact.Find(id);
            db.s_Contact.Remove(s_Contact);
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

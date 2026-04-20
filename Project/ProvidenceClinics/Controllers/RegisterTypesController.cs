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
    public class RegisterTypesController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: RegisterTypes
        [Authorize]
        public ActionResult Index()
        {
            return View(db.RegisterTypes.ToList());
        }

        // GET: RegisterTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterType registerType = db.RegisterTypes.Find(id);
            if (registerType == null)
            {
                return HttpNotFound();
            }
            return View(registerType);
        }

        // GET: RegisterTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegisterTypeID,TypeName")] RegisterType registerType)
        {
            if (ModelState.IsValid)
            {
                db.RegisterTypes.Add(registerType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registerType);
        }

        // GET: RegisterTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterType registerType = db.RegisterTypes.Find(id);
            if (registerType == null)
            {
                return HttpNotFound();
            }
            return View(registerType);
        }

        // POST: RegisterTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegisterTypeID,TypeName")] RegisterType registerType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registerType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registerType);
        }

        // GET: RegisterTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterType registerType = db.RegisterTypes.Find(id);
            if (registerType == null)
            {
                return HttpNotFound();
            }
            return View(registerType);
        }

        // POST: RegisterTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterType registerType = db.RegisterTypes.Find(id);
            db.RegisterTypes.Remove(registerType);
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

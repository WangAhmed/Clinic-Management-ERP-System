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
    public class e_lspController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: e_lsp
        [Authorize]
        public ActionResult Index()
        {
            var e_lsp = db.e_lsp.Include(e => e.e_lsp_type);
            return View(e_lsp.ToList());
        }

        // GET: e_lsp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_lsp e_lsp = db.e_lsp.Find(id);
            if (e_lsp == null)
            {
                return HttpNotFound();
            }
            return View(e_lsp);
        }

        // GET: e_lsp/Create
        public ActionResult Create()
        {
            ViewBag.type_id = new SelectList(db.e_lsp_type, "lsptype_id", "type");
            return View();
        }

        // POST: e_lsp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "lspid,name,description,type_id")] e_lsp e_lsp)
        {
            if (ModelState.IsValid)
            {
                db.e_lsp.Add(e_lsp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.type_id = new SelectList(db.e_lsp_type, "lsptype_id", "type", e_lsp.type_id);
            return View(e_lsp);
        }

        // GET: e_lsp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_lsp e_lsp = db.e_lsp.Find(id);
            if (e_lsp == null)
            {
                return HttpNotFound();
            }
            ViewBag.type_id = new SelectList(db.e_lsp_type, "lsptype_id", "type", e_lsp.type_id);
            return View(e_lsp);
        }

        // POST: e_lsp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "lspid,name,description,type_id")] e_lsp e_lsp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(e_lsp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.type_id = new SelectList(db.e_lsp_type, "lsptype_id", "type", e_lsp.type_id);
            return View(e_lsp);
        }

        // GET: e_lsp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_lsp e_lsp = db.e_lsp.Find(id);
            if (e_lsp == null)
            {
                return HttpNotFound();
            }
            return View(e_lsp);
        }

        // POST: e_lsp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            e_lsp e_lsp = db.e_lsp.Find(id);
            db.e_lsp.Remove(e_lsp);
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

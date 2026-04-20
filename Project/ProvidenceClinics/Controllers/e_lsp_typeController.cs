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
    public class e_lsp_typeController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: e_lsp_type
        [Authorize]
        public ActionResult Index()
        {
            return View(db.e_lsp_type.ToList());
        }

        // GET: e_lsp_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_lsp_type e_lsp_type = db.e_lsp_type.Find(id);
            if (e_lsp_type == null)
            {
                return HttpNotFound();
            }
            return View(e_lsp_type);
        }

        // GET: e_lsp_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: e_lsp_type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "lsptype_id,type")] e_lsp_type e_lsp_type)
        {
            if (ModelState.IsValid)
            {
                db.e_lsp_type.Add(e_lsp_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(e_lsp_type);
        }

        // GET: e_lsp_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_lsp_type e_lsp_type = db.e_lsp_type.Find(id);
            if (e_lsp_type == null)
            {
                return HttpNotFound();
            }
            return View(e_lsp_type);
        }

        // POST: e_lsp_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "lsptype_id,type")] e_lsp_type e_lsp_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(e_lsp_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(e_lsp_type);
        }

        // GET: e_lsp_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            e_lsp_type e_lsp_type = db.e_lsp_type.Find(id);
            if (e_lsp_type == null)
            {
                return HttpNotFound();
            }
            return View(e_lsp_type);
        }

        // POST: e_lsp_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            e_lsp_type e_lsp_type = db.e_lsp_type.Find(id);
            db.e_lsp_type.Remove(e_lsp_type);
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

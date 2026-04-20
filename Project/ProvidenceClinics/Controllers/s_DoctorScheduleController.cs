using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProvidenceClinics.Models;

namespace ProvidenceClinics.Controllers
{
    public class s_DoctorScheduleController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_DoctorSchedule
        public ActionResult Index()
        {
            var s_DoctorSchedule = db.s_DoctorSchedule.Include(s => s.s_Doctor);
            return View(s_DoctorSchedule.ToList());
        }

        // GET: s_DoctorSchedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_DoctorSchedule s_DoctorSchedule = db.s_DoctorSchedule.Find(id);
            if (s_DoctorSchedule == null)
            {
                return HttpNotFound();
            }
            return View(s_DoctorSchedule);
        }

        // GET: s_DoctorSchedule/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID", "FirstName");
            return View();
        }

        // POST: s_DoctorSchedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorID,AvailableDate,StartTime,EndTime")] s_DoctorSchedule s_DoctorSchedule)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.s_DoctorSchedule.Add(s_DoctorSchedule);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    // Check if the exception is due to a unique constraint violation
                    if (ex.InnerException != null && ex.InnerException.InnerException != null)
                    {
                        var sqlException = ex.InnerException.InnerException as System.Data.SqlClient.SqlException;
                        if (sqlException != null && sqlException.Number == 2627) // Unique constraint violation
                        {
                            ModelState.AddModelError(string.Empty, "Duplicate schedule entry. The schedule you are trying to create already exists.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "An error occurred while saving the schedule. Please try again.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while saving the schedule. Please try again.");
                    }
                }
            }

            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID", "FirstName", s_DoctorSchedule.DoctorID);
            return View(s_DoctorSchedule);
        }

        // GET: s_DoctorSchedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_DoctorSchedule s_DoctorSchedule = db.s_DoctorSchedule.Find(id);
            if (s_DoctorSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID", "FirstName", s_DoctorSchedule.DoctorID);
            return View(s_DoctorSchedule);
        }

        // POST: s_DoctorSchedule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchID,DoctorID,AvailableDate,StartTime,EndTime")] s_DoctorSchedule s_DoctorSchedule)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(s_DoctorSchedule).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.SingleOrDefault();
                    if (entry != null)
                    {
                        var databaseValues = (s_DoctorSchedule)entry.GetDatabaseValues().ToObject();
                        var clientValues = (s_DoctorSchedule)entry.Entity;

                        if (databaseValues.DoctorID != clientValues.DoctorID)
                            ModelState.AddModelError("DoctorID", "Current value: " + databaseValues.DoctorID);
                        if (databaseValues.AvailableDate != clientValues.AvailableDate)
                            ModelState.AddModelError("AvailableDate", "Current value: " + databaseValues.AvailableDate);
                        if (databaseValues.StartTime != clientValues.StartTime)
                            ModelState.AddModelError("StartTime", "Current value: " + databaseValues.StartTime);
                        if (databaseValues.EndTime != clientValues.EndTime)
                            ModelState.AddModelError("EndTime", "Current value: " + databaseValues.EndTime);

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user after you got the original value. The edit operation was canceled and the current values in the database have been displayed. If you still want to edit this record, click the Save button again. Otherwise click the Back to List hyperlink.");
                    }
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null)
                    {
                        var sqlException = ex.InnerException.InnerException as System.Data.SqlClient.SqlException;
                        if (sqlException != null && sqlException.Number == 2627) // Unique constraint violation
                        {
                            ModelState.AddModelError(string.Empty, "Duplicate schedule entry. The schedule you are trying to update already exists.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "An error occurred while updating the schedule. Please try again.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while updating the schedule. Please try again.");
                    }
                }
            }
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID", "FirstName", s_DoctorSchedule.DoctorID);
            return View(s_DoctorSchedule);
        }

        // GET: s_DoctorSchedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_DoctorSchedule s_DoctorSchedule = db.s_DoctorSchedule.Find(id);
            if (s_DoctorSchedule == null)
            {
                return HttpNotFound();
            }
            return View(s_DoctorSchedule);
        }

        // POST: s_DoctorSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            s_DoctorSchedule s_DoctorSchedule = db.s_DoctorSchedule.Find(id);
            if (s_DoctorSchedule != null)
            {
                db.s_DoctorSchedule.Remove(s_DoctorSchedule);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError(string.Empty, "Unable to delete. The record you attempted to delete was modified by another user after you got the original value.");
                    return View(s_DoctorSchedule);
                }
            }
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

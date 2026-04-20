using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Appointment.Models;

namespace Appointment.Controllers
{
    public class s_DoctorScheduleController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: s_DoctorSchedule
        public ActionResult Index()
        {
            // Check if the session value is set
            if (Session["UserID"] == null)
            {
                // Redirect to an error page or login page if the session is not set
                return RedirectToAction("Index", "Signin"); // Adjust to your actual login action
            }

            int doctorId;

            // Try to parse the session value to an integer
            if (!int.TryParse(Session["UserID"].ToString(), out doctorId))
            {
                // Handle the case where the session value is not a valid integer
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid DoctorID in session.");
            }

            // Filter schedules based on the logged-in doctor's ID
            var s_DoctorSchedule = db.s_DoctorSchedule
                .Where(s => s.DoctorID == doctorId)
                .Include(s => s.s_Doctor);

            return View(s_DoctorSchedule.ToList());
        }

        // GET: s_DoctorSchedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Check if the session value is set
            if (Session["UserID"] == null)
            {
                // Redirect to an error page or login page if the session is not set
                return RedirectToAction("Login", "Account"); // Adjust to your actual login action
            }

            int doctorId;

            // Try to parse the session value to an integer
            if (!int.TryParse(Session["UserID"].ToString(), out doctorId))
            {
                // Handle the case where the session value is not a valid integer
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid DoctorID in session.");
            }

            s_DoctorSchedule s_DoctorSchedule = db.s_DoctorSchedule
                .FirstOrDefault(s => s.SchID == id && s.DoctorID == doctorId);

            if (s_DoctorSchedule == null)
            {
                return HttpNotFound();
            }

            return View(s_DoctorSchedule);
        }

        // GET: s_DoctorSchedule/Create
        public ActionResult Create()
        {
            // Check if the session value is set
            if (Session["UserID"] == null)
            {
                // Redirect to an error page or login page if the session is not set
                return RedirectToAction("Index", "Signin"); // Adjust to your actual login action
            }

            // Initialize a new instance of the model
            s_DoctorSchedule newSchedule = new s_DoctorSchedule();

            // Set the DoctorID from session
            newSchedule.DoctorID = Convert.ToInt32(Session["UserID"]);

            return View(newSchedule);
        }

        // POST: s_DoctorSchedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AvailableDate,StartTime,EndTime")] s_DoctorSchedule s_DoctorSchedule)
        {
            if (ModelState.IsValid)
            {
                // Check if the session value is set
                if (Session["UserID"] == null)
                {
                    // Redirect to an error page or login page if the session is not set
                    return RedirectToAction("Index", "Signin"); // Adjust to your actual login action
                }

                // Set the DoctorID from session
                s_DoctorSchedule.DoctorID = Convert.ToInt32(Session["UserID"]);

                // Add the new schedule to the database
                db.s_DoctorSchedule.Add(s_DoctorSchedule);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // If model state is not valid, re-display the form with validation errors
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

            // Get DoctorID from session
            int doctorId = (int)Session["UserID"];

            // Ensure the schedule belongs to the current doctor
            if (s_DoctorSchedule.DoctorID != doctorId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            // Populate the dropdown
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID", "DoctorName", s_DoctorSchedule.DoctorID);

            return View(s_DoctorSchedule);
        }

        // POST: s_DoctorSchedule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchID,AvailableDate,StartTime,EndTime")] s_DoctorSchedule s_DoctorSchedule)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Get DoctorID from session
                    int doctorId = (int)Session["UserID"];

                    // Define the SQL query with parameters
                    string query = @"
                UPDATE [dbo].[s_DoctorSchedule]
                SET [DoctorID] = @DoctorID,
                    [AvailableDate] = @AvailableDate,
                    [StartTime] = @StartTime,
                    [EndTime] = @EndTime
                WHERE [SchID] = @SchID";

                    // Execute the SQL query
                    db.Database.ExecuteSqlCommand(query,
                        new SqlParameter("@DoctorID", doctorId),
                        new SqlParameter("@AvailableDate", s_DoctorSchedule.AvailableDate),
                        new SqlParameter("@StartTime", s_DoctorSchedule.StartTime),
                        new SqlParameter("@EndTime", s_DoctorSchedule.EndTime),
                        new SqlParameter("@SchID", s_DoctorSchedule.SchID));

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Optionally log the exception or handle it in some way
                    System.Diagnostics.Debug.WriteLine($"Exception occurred: {ex.Message}");
                    // Consider removing the unused variable warning by using the variable or removing it if unnecessary
                    throw new InvalidOperationException("An error occurred while processing your request.", ex);
                }

            }

            // Re-populate the dropdown in case of validation errors
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID", "DoctorName", s_DoctorSchedule.DoctorID);
            return View(s_DoctorSchedule);
        }




        // GET: s_DoctorSchedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Check if the session value is set
            if (Session["UserID"] == null)
            {
                // Redirect to an error page or login page if the session is not set
                return RedirectToAction("Login", "Account"); // Adjust to your actual login action
            }

            int doctorId;

            // Try to parse the session value to an integer
            if (!int.TryParse(Session["UserID"].ToString(), out doctorId))
            {
                // Handle the case where the session value is not a valid integer
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid DoctorID in session.");
            }

            s_DoctorSchedule s_DoctorSchedule = db.s_DoctorSchedule
                .FirstOrDefault(s => s.SchID == id && s.DoctorID == doctorId);

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
            // Check if the session value is set
            if (Session["UserID"] == null)
            {
                // Redirect to an error page or login page if the session is not set
                return RedirectToAction("Login", "Account"); // Adjust to your actual login action
            }

            int doctorId;

            // Try to parse the session value to an integer
            if (!int.TryParse(Session["UserID"].ToString(), out doctorId))
            {
                // Handle the case where the session value is not a valid integer
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid DoctorID in session.");
            }

            s_DoctorSchedule s_DoctorSchedule = db.s_DoctorSchedule
                .FirstOrDefault(s => s.SchID == id && s.DoctorID == doctorId);

            if (s_DoctorSchedule != null)
            {
                db.s_DoctorSchedule.Remove(s_DoctorSchedule);
                db.SaveChanges();
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

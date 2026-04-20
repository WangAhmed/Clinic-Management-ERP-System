using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Appointment.Models;
using System.Data.Entity.Infrastructure;

namespace Appointment.Controllers
{
    public class HomeController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: Home/Index
        public ActionResult Index()
        {
            var s_Doctor = db.s_Doctor.Include(s => s.Register).Include(s => s.s_Specialist).Take(10);
            return View(s_Doctor.ToList());
        }

        // GET: Home/Search
        public ActionResult Search(int? SpecialistID = null, string DoctorName = null)
        {
            // Use the correct connection string name from the configuration
            string connectionString = ConfigurationManager.ConnectionStrings["ProvidenceClinicsSqlConnection"]?.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'ProvidenceClinicsSqlConnection' is not found.");
            }

            var doctorsList = new List<s_Doctor>();

            string query = @"
                            SELECT [DoctorID]
                                  ,[regid]
                                  ,[image]
                                  ,[FirstName]
                                  ,[LastName]
                                  ,[SpecialistID]
                                  ,[ContactNumber]
                                  ,[Email]
                              FROM [ProvidenceClinics].[dbo].[DocView]
                              WHERE (@SpecialistID IS NULL OR [SpecialistID] = @SpecialistID)
                          AND (@DoctorName IS NULL OR ([FirstName] LIKE '%' + @DoctorName + '%' OR [LastName] LIKE '%' + @DoctorName + '%'))";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SpecialistID", (object)SpecialistID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DoctorName", (object)DoctorName ?? DBNull.Value);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var doctor = new s_Doctor
                            {
                                DoctorID = reader.GetInt32(0),
                                regid = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                image = reader.IsDBNull(2) ? null : reader.GetString(2),
                                FirstName = reader.GetString(3),
                                LastName = reader.GetString(4),
                                SpecialistID = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                                ContactNumber = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Email = reader.IsDBNull(7) ? null : reader.GetString(7),
                            };
                            doctorsList.Add(doctor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception details for debugging
                System.Diagnostics.Debug.WriteLine($"Exception occurred: {ex.Message}");
                throw new InvalidOperationException("An error occurred while retrieving doctor data.", ex);
            }

            // Pass the list of doctors to the view
            return View(doctorsList);
        }

        // GET: Home/Detail
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                // Redirect to an error page if the ID is null
                return RedirectToAction("Error", "Home");
            }

            // Fetch the doctor from the database
            s_Doctor doctor = db.s_Doctor.Find(id);

            if (doctor == null)
            {
                // Redirect to a not found page if the doctor is not found
                return RedirectToAction("NotFound", "Home");
            }

            // Pass the doctor to the Details view
            return View(doctor);
        }



        public ActionResult Create(int doctorId)
        {
            // Retrieve PatientID from the session
            int patientId = Session["UserID"] != null ? (int)Session["UserID"] : 0;

            // Optionally handle the case where PatientID is not set
            if (patientId == 0)
            {
                return RedirectToAction("Index", "Signin");
            }

            // Pass DoctorID and PatientID to the view
            var appointment = new s_Appointment
            {
                DoctorID = doctorId,
                PatientID = patientId,
                AppointmentDate = DateTime.Today.ToString("yyyy-MM-dd"), // Convert DateTime to string
                AppointmentTime = DateTime.Now.ToString("HH:mm") // Convert DateTime to string
            };

            Session["DoctorID"] = doctorId;
            
            ViewBag.Statusid = new SelectList(db.s_Status, "StatusID", "StatusName");

            return View(appointment);
        }


        // POST: Home/Create
        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentID,PatientID,DoctorID,AppointmentDate,AppointmentTime,Statusid")] s_Appointment s_Appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Set the fee to a hard-coded value of 500
                    s_Appointment.fee = 500;

                    // Set PatientID and DoctorID from the session
                    s_Appointment.PatientID = int.Parse(Session["UserID"].ToString());
                    s_Appointment.DoctorID = int.Parse(Session["DoctorID"].ToString());

                    // Attempt to save the appointment
                    db.s_Appointment.Add(s_Appointment);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    // Check if the exception contains the specific SQL error code for the trigger
                    if (ex.InnerException?.InnerException is SqlException sqlEx &&
                        sqlEx.Message.Contains("outside the doctor's schedule"))
                    {
                        ViewBag.ErrorMessage = "The appointment time is outside the doctor's schedule.";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "An error occurred while saving the appointment. Please try again.";
                    }
                }
            }

            // Re-populate ViewBag properties to ensure dropdowns are filled
            ViewBag.DoctorID = new SelectList(db.s_Doctor, "DoctorID", "image", s_Appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.s_Patient, "PatientID", "FirstName", s_Appointment.PatientID);
            ViewBag.Statusid = new SelectList(db.s_Status, "StatusID", "StatusName", s_Appointment.Statusid);

            return View(s_Appointment);
        }


    }

}


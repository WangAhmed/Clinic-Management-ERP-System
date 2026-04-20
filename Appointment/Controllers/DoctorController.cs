using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Appointment.Models;


namespace Appointment.Controllers
{
    public class DoctorController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: Doctor
        public ActionResult Index()
        {
            getApp();
            ViewBag.Statuses = new SelectList(db.s_Status.ToList(), "StatusID", "StatusName");
            return View();
        }

        public void getApp()
        {
            List<AppointmentViewModel> appointmentList = new List<AppointmentViewModel>();
            int doctorId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProvidenceClinicsSqlConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT 
                        a.AppointmentID, a.PatientID, a.DoctorID, 
                        a.AppointmentDate, a.AppointmentTime, s.StatusName, 
                        a.fee, 
                        d.FirstName AS DoctorFirstName, d.LastName AS DoctorLastName, 
                        d.ContactNumber AS DoctorContactNumber, d.Email AS DoctorEmail, 
                        p.FirstName AS PatientFirstName, p.LastName AS PatientLastName, 
                        p.Email AS PatientEmail, p.ContactNumber AS PatientContactNumber, 
                        p.Gender AS PatientGender, p.DateOfBirth AS PatientDateOfBirth, 
                        p.Image AS PatientImage, sp.SpecialistName, sp.SpecialistID
                    FROM v_Appointment a
                    INNER JOIN s_Status s ON a.Statusid = s.StatusID
                    INNER JOIN s_Patient p ON a.PatientID = p.PatientID
                    INNER JOIN s_Doctor d ON a.DoctorID = d.DoctorID
                    INNER JOIN s_Specialist sp ON d.SpecialistID = sp.SpecialistID
                    WHERE a.DoctorID = @DoctorID", conn);

                cmd.Parameters.AddWithValue("@DoctorID", doctorId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AppointmentViewModel appointment = new AppointmentViewModel
                    {
                        AppointmentID = reader["AppointmentID"] != DBNull.Value ? Convert.ToInt32(reader["AppointmentID"]) : 0,
                        PatientID = reader["PatientID"] != DBNull.Value ? Convert.ToInt32(reader["PatientID"]) : 0,
                        DoctorID = reader["DoctorID"] != DBNull.Value ? Convert.ToInt32(reader["DoctorID"]) : 0,
                        AppointmentDate = reader["AppointmentDate"] != DBNull.Value ? Convert.ToDateTime(reader["AppointmentDate"]) : DateTime.MinValue,
                        AppointmentTime = reader["AppointmentTime"] != DBNull.Value ? TimeSpan.Parse(reader["AppointmentTime"].ToString()) : TimeSpan.Zero,
                        StatusName = reader["StatusName"].ToString(),
                        Fee = reader["fee"] != DBNull.Value ? Convert.ToDecimal(reader["fee"]) : 0.0m,
                        DoctorFirstName = reader["DoctorFirstName"].ToString(),
                        DoctorLastName = reader["DoctorLastName"].ToString(),
                        DoctorContactNumber = reader["DoctorContactNumber"].ToString(),
                        DoctorEmail = reader["DoctorEmail"].ToString(),
                        PatientFirstName = reader["PatientFirstName"].ToString(),
                        PatientLastName = reader["PatientLastName"].ToString(),
                        PatientEmail = reader["PatientEmail"].ToString(),
                        PatientContactNumber = reader["PatientContactNumber"].ToString(),
                        PatientGender = reader["PatientGender"].ToString(),
                        PatientDateOfBirth = reader["PatientDateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["PatientDateOfBirth"]) : DateTime.MinValue,
                        PatientImage = reader["PatientImage"].ToString(),
                        SpecialistName = reader["SpecialistName"].ToString(),
                        SpecialistID = reader["SpecialistID"] != DBNull.Value ? Convert.ToInt32(reader["SpecialistID"]) : 0
                    };

                    appointmentList.Add(appointment);
                }
            }

            ViewBag.Appointments = appointmentList;
        }

        // Action to update the StatusID of an appointment
        [HttpPost]
        public ActionResult UpdateStatus(int appointmentID, int newStatusID)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProvidenceClinicsSqlConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE s_Appointment
                    SET StatusID = @NewStatusID
                    WHERE AppointmentID = @AppointmentID", conn);

                cmd.Parameters.AddWithValue("@NewStatusID", newStatusID);
                cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ViewBag.Message = "Status updated successfully.";
                }
                else
                {
                    ViewBag.Message = "Failed to update status. Please check the Appointment ID.";
                }
            }

            return RedirectToAction("Index");
        }


        public ActionResult MyPatient()
        {
            // Ensure the session contains the DoctorID
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Signin"); // Redirect to login if session is null
            }

            int doctorId;
            if (!int.TryParse(Session["UserID"].ToString(), out doctorId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid DoctorID in session.");
            }

            // Query to get the patient history for the logged-in doctor
            var patientHistories = db.s_PatientHistory
                                     .Where(ph => ph.DoctorID == doctorId)
                                     .ToList();

            return View(patientHistories);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            s_Doctor s_Doctor = db.s_Doctor.Find(id);
            if (s_Doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", s_Doctor.regid);
            ViewBag.SpecialistID = new SelectList(db.s_Specialist, "SpecialistID", "SpecialistName", s_Doctor.SpecialistID);
            return View(s_Doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorID,regid,image,FirstName,LastName,SpecialistID,ContactNumber,Email")] s_Doctor s_Doctor, HttpPostedFileBase imageFile)
        {
            // Check if s_Doctor is null
            if (s_Doctor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Debugging: Check if the image file is being received
            if (imageFile != null)
            {
                Debug.WriteLine($"Image file received: {imageFile.FileName}");

                if (imageFile.ContentLength > 0)
                {
                    try
                    {
                        // Generate the path and save the file
                        string fileName = Path.GetFileName(imageFile.FileName);
                        string path = Path.Combine(Server.MapPath("data/"), fileName);

                        // Ensure the directory exists
                        string directoryPath = Path.GetDirectoryName(path);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Save the file to the server
                        imageFile.SaveAs(path);

                        // Update the image property
                        s_Doctor.image = fileName;
                        Debug.WriteLine($"Image file saved: {fileName}");
                    }
                    catch (Exception ex)
                    {
                        // Log the exception and handle it
                        Debug.WriteLine($"Error saving image file: {ex.Message}");
                        ModelState.AddModelError("", "Unable to save image file.");
                    }
                }
            }
            else
            {
                // If no new image file is uploaded, keep the existing image path
                s_Doctor.image = db.s_Doctor.Find(s_Doctor.DoctorID)?.image;
                Debug.WriteLine($"No new image file uploaded. Existing image: {s_Doctor.image}");
            }

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(s_Doctor).State = EntityState.Modified;
                    db.SaveChanges();
                    Debug.WriteLine("Doctor details updated successfully.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception and handle it
                    Debug.WriteLine($"Error updating doctor: {ex.Message}");
                    ModelState.AddModelError("", "Unable to update doctor details.");
                }
            }
            else
            {
                // Log validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine($"Model state error: {error.ErrorMessage}");
                }
            }

            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", s_Doctor.regid);
            ViewBag.SpecialistID = new SelectList(db.s_Specialist, "SpecialistID", "SpecialistName", s_Doctor.SpecialistID);
            return View(s_Doctor);
        }


       
    
    }

}

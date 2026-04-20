using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Appointment.Models;

namespace Appointment.Controllers
{
    public class PatientController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: Patient
        public ActionResult Index()
        {
            getApp();  
            Prescriptions(); 
            LabTest();
            return View();
        }

        public void getApp()
        {
            List<AppointmentViewModel> appointmentList = new List<AppointmentViewModel>();
            int patientId = Convert.ToInt32(Session["UserID"]); // Retrieve PatientID from the session

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
            WHERE a.PatientID = @PatientID", conn);

                cmd.Parameters.AddWithValue("@PatientID", patientId); // Add parameter to command

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

        public void Prescriptions()
        {
            List<PrescriptionViewModel> prescriptionList = new List<PrescriptionViewModel>();
            int patientId = Convert.ToInt32(Session["UserID"]); // Retrieve PatientID from the session

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProvidenceClinicsSqlConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT 
                        PrescriptionID, AppointmentID, MedicineName, Dosage, 
                        Duration, Notes, PatientID, DoctorID, image, 
                        FirstName, LastName
                    FROM vw_Prescriptions
                    WHERE PatientID = @PatientID", conn);

                cmd.Parameters.AddWithValue("@PatientID", patientId); // Add parameter to command

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PrescriptionViewModel prescription = new PrescriptionViewModel
                    {
                        PrescriptionID = reader["PrescriptionID"] != DBNull.Value ? Convert.ToInt32(reader["PrescriptionID"]) : 0,
                        AppointmentID = reader["AppointmentID"] != DBNull.Value ? Convert.ToInt32(reader["AppointmentID"]) : 0,
                        MedicineName = reader["MedicineName"].ToString(),
                        Dosage = reader["Dosage"].ToString(),
                        Duration = reader["Duration"].ToString(),
                        Notes = reader["Notes"].ToString(),
                        PatientID = reader["PatientID"] != DBNull.Value ? Convert.ToInt32(reader["PatientID"]) : 0,
                        DoctorID = reader["DoctorID"] != DBNull.Value ? Convert.ToInt32(reader["DoctorID"]) : 0,
                        Image = reader["image"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString()
                    };

                    prescriptionList.Add(prescription);
                }
            }

            ViewBag.Prescriptions = prescriptionList;
        }

        public void LabTest()
        {
            List<LabTestViewModel> labTestList = new List<LabTestViewModel>();
            int? userId = Session["UserID"] as int?; // Retrieve PatientID from the session

            if (userId.HasValue)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProvidenceClinicsSqlConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT TOP (1000)
                            LabTestID, TestName, DoctorID, PatientID, 
                            TestDate, Price, FirstName, LastName, 
                            image, Expr1, Expr2, SpecialistID, 
                            ChemicalsUsed, LabProductsUsed
                        FROM labview
                        WHERE PatientID = @UserID", conn);

                    cmd.Parameters.AddWithValue("@UserID", userId.Value); // Add parameter to command

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        LabTestViewModel labTest = new LabTestViewModel
                        {
                            LabTestID = reader["LabTestID"] != DBNull.Value ? Convert.ToInt32(reader["LabTestID"]) : 0,
                            TestName = reader["TestName"].ToString(),
                            DoctorID = reader["DoctorID"] != DBNull.Value ? Convert.ToInt32(reader["DoctorID"]) : 0,
                            PatientID = reader["PatientID"] != DBNull.Value ? Convert.ToInt32(reader["PatientID"]) : 0,
                            TestDate = reader["TestDate"] != DBNull.Value ? Convert.ToDateTime(reader["TestDate"]) : DateTime.MinValue,
                            Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0.0m,
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Image = reader["image"].ToString(),
                            Expr1 = reader["Expr1"].ToString(),
                            Expr2 = reader["Expr2"].ToString(),
                            SpecialistID = reader["SpecialistID"] != DBNull.Value ? Convert.ToInt32(reader["SpecialistID"]) : 0,
                            ChemicalsUsed = reader["ChemicalsUsed"].ToString(),
                            LabProductsUsed = reader["LabProductsUsed"].ToString()
                        };

                        labTestList.Add(labTest);
                    }
                }

                ViewBag.LabTests = labTestList;
            }
        }


        // GET: s_Patient/Edit/5
        public ActionResult Edit()
        {
            int? userId = Session["UserID"] as int?;
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            s_Patient s_Patient = db.s_Patient.FirstOrDefault(p => p.regid == userId);
            if (s_Patient == null)
            {
                return HttpNotFound();
            }

            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", s_Patient.regid);
            return View(s_Patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientID,regid,FirstName,LastName,DateOfBirth,Gender,ContactNumber,Email,Address")] s_Patient s_Patient, HttpPostedFileBase imageFile)
        {
            int? userId = Session["UserID"] as int?;
            if (userId == null || s_Patient.regid != userId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
               
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(imageFile.InputStream))
                    {
                        s_Patient.image = binaryReader.ReadBytes(imageFile.ContentLength);
                    }
                }

                db.Entry(s_Patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }

            ViewBag.regid = new SelectList(db.Registers, "RegisterID", "Username", s_Patient.regid);
            return View(s_Patient); 
        }


    }
}

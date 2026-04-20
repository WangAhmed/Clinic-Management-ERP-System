using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appointment.Models;

namespace Appointment.Controllers
{
    public class SignupController : Controller
    {
        private string connectionString = "data source=ZEE\\SQLEXPRESS;initial catalog=ProvidenceClinics;integrated security=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework";

        // GET: Signup
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Countries = GetCountries();
            ViewBag.Cities = GetCities(); // Load all cities

            return View();
        }
        // POST: Signup/PatientRegistration
        [AllowAnonymous]
        [HttpPost]
        public ActionResult PatientRegistration(FormCollection form, HttpPostedFileBase Image)
        {
            try
            {
                // Extract form data
                string username = form["Username"];
                string email = form["Email"];
                string password = form["Password"];
                string firstName = form["FirstName"];
                string lastName = form["LastName"];
                string dateOfBirth = form["DateOfBirth"];
                string gender = form["Gender"];
                string contactNumber = form["ContactNumber"];
                string address = form["Address"];
                int countryId = Convert.ToInt32(form["CountryID"]);
                int cityId = Convert.ToInt32(form["CityID"]);

                // Handle image upload
                string imagePath = null;
                if (Image != null && Image.ContentLength > 0)
                {
                    string folderPath = Server.MapPath("~/data/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string fileName = Path.GetFileName(Image.FileName);
                    imagePath = Path.Combine(folderPath, fileName);
                    Image.SaveAs(imagePath);
                    imagePath = "~/data/" + fileName;
                }

                // SQL command to execute stored procedure
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.Registeration", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@image", imagePath);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@fname", firstName);
                        cmd.Parameters.AddWithValue("@lastname", lastName);
                        cmd.Parameters.AddWithValue("@DOF", dateOfBirth);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Contact", contactNumber);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@CountryID", countryId);
                        cmd.Parameters.AddWithValue("@CityID", cityId);

                        // Hard-coded parameter
                        cmd.Parameters.AddWithValue("@RegTypeID", 3); // Hard-code RegTypeID for patient

                        // Output parameters
                        SqlParameter emailIdOutParam = new SqlParameter("@EMAIL_ID_OUT", SqlDbType.VarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(emailIdOutParam);

                        SqlParameter registerIdOutParam = new SqlParameter("@RegisterID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(registerIdOutParam);

                        // Open connection, execute the command
                        con.Open();
                        cmd.ExecuteNonQuery();

                        // Handle result
                        string outputEmail = cmd.Parameters["@EMAIL_ID_OUT"].Value != DBNull.Value ? (string)cmd.Parameters["@EMAIL_ID_OUT"].Value : null;
                        int registerId = cmd.Parameters["@RegisterID"].Value != DBNull.Value ? (int)cmd.Parameters["@RegisterID"].Value : 0;

                        ViewBag.Message = !string.IsNullOrEmpty(outputEmail) ? "Registration successful!" : "Registration failed.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                ViewBag.Message = "Error: " + ex.Message;
            }

            // Re-populate dropdown lists
            ViewBag.Countries = GetCountries();
            ViewBag.Cities = GetCities(); // Load all cities again

            return View("Index");
        }

        [AllowAnonymous]
        private SelectList GetCountries()
        {
            using (var context = new ProvidenceClinicsEntities())
            {
                var countries = context.Countries.ToList();
                return new SelectList(countries, "CountryID", "CountryName");
            }
        }
        [AllowAnonymous]
        private SelectList GetCities()
        {
            using (var context = new ProvidenceClinicsEntities())
            {
                var cities = context.Cities.ToList(); // Retrieve all cities
                return new SelectList(cities, "CityID", "CityName");
            }
        }
    }
}

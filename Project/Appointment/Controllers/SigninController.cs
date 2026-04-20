using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Appointment.Models;

namespace Appointment.Controllers
{
    public class SigninController : Controller
    {
        private ProvidenceClinicsEntities db = new ProvidenceClinicsEntities();

        // GET: Signin
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Signin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Register model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = db.Registers
                        .Where(u => u.Email == model.Email)
                        .SingleOrDefault();

                    if (user != null && user.PasswordHash == model.PasswordHash) // Ensure this is hashed securely
                    {
                        // Set session and cookies based on user type
                        if (user.RegisterTypeID == 3) // Patient
                        {
                            var patient = db.s_Patient
                                .Where(p => p.regid == user.RegisterID)
                                .SingleOrDefault();

                            if (patient != null)
                            {
                                // Set session
                                Session["UserType"] = "Patient";
                                Session["UserID"] = patient.PatientID; // Store PatientID
                                Session["UserName"] = patient.FirstName + " " + patient.LastName;

                                // Set authentication cookie
                                FormsAuthentication.SetAuthCookie(user.Email, false);

                                // Create and set custom cookie
                                HttpCookie userCookie = new HttpCookie("UserCookie");
                                userCookie["UserID"] = patient.PatientID.ToString();
                                userCookie["UserName"] = patient.FirstName + " " + patient.LastName;
                                userCookie["UserType"] = "Patient";
                                userCookie.Expires = DateTime.Now.AddDays(1); // Set expiration for 1 day
                                Response.Cookies.Add(userCookie);

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Patient details not found.";
                            }
                        }
                        else if (user.RegisterTypeID == 4) // Doctor
                        {
                            var doctor = db.s_Doctor
                                .Where(d => d.regid == user.RegisterID)
                                .SingleOrDefault();

                            if (doctor != null)
                            {
                                // Set session
                                Session["UserType"] = "Doctor";
                                Session["UserID"] = doctor.DoctorID; // Store DoctorID
                                Session["UserName"] = doctor.FirstName + " " + doctor.LastName;
                                Session["Image"] = doctor.image.ToString();

                                // Set authentication cookie
                                FormsAuthentication.SetAuthCookie(user.Email, false);

                                // Create and set custom cookie
                                HttpCookie userCookie = new HttpCookie("UserCookie");
                                userCookie["UserID"] = doctor.DoctorID.ToString();
                                userCookie["UserName"] = doctor.FirstName + " " + doctor.LastName;
                                userCookie["UserType"] = "Doctor";
                                userCookie.Expires = DateTime.Now.AddDays(1); // Set expiration for 1 day
                                Response.Cookies.Add(userCookie);

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Doctor details not found.";
                            }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Unauthorized user type.";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid email or password.";
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and handle error without redirecting to /Home/Error
                    System.Diagnostics.Debug.WriteLine($"Exception occurred: {ex.Message}");
                    ViewBag.ErrorMessage = "An error occurred while processing your request.";
                }
            }
            return View(model);
        }

        // GET: Signin/SignOut
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            // Clear session
            Session.Clear();

            // Remove custom cookie
            if (Request.Cookies["UserCookie"] != null)
            {
                HttpCookie cookie = new HttpCookie("UserCookie");
                cookie.Expires = DateTime.Now.AddDays(-1); // Set the expiration date to the past
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

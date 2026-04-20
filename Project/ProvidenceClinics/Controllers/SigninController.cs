using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProvidenceClinics.Models;

namespace ProvidenceClinic.Controllers
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
                // Check the credentials
                var user = db.Registers.FirstOrDefault(u => u.Email == model.Email && u.PasswordHash == model.PasswordHash);
                if (user != null)
                {
                    // Check if the user is a MasterAdmin
                    if (user.RegisterTypeID == 1)
                    {
                        // Store user data in session
                        Session["User"] = user;
                        Session["Username"] = user.Username;
                        Session["UserEmail"] = user.Email;
                        Session["UserProfileImage"] = user.image; // Assuming 'image' stores the user's profile picture

                        // Create a cookie
                        HttpCookie cookie = new HttpCookie("UserCookie");
                        cookie.Values["Email"] = user.Email;
                        cookie.Values["Username"] = user.Username;
                        cookie.Values["RegisterTypeID"] = user.RegisterTypeID.ToString();
                        cookie.Expires = DateTime.Now.AddHours(1); // Cookie expiration time
                        Response.Cookies.Add(cookie);

                        FormsAuthentication.SetAuthCookie(user.Email, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "You do not have the right to login as MasterAdmin.";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid email or password.";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please correct the errors and try again.";
            }
            return View(model);
        }

        // GET: Signin/SignOut
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            // Clear session
            Session.Clear();


            // Remove cookie
            if (Request.Cookies["UserCookie"] != null)
            {
                HttpCookie cookie = new HttpCookie("UserCookie");
                cookie.Expires = DateTime.Now.AddDays(-4); // Set the expiration date to the past
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

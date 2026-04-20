using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JewelleryShop.Controllers
{
    public class SignInController : Controller
    {
        // GET: SignIn
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Auth(string uname, string pwd)
        {

            //-----Step 1  make connection ----//
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
            conn.Open();

            //-----Step 2  command creation ----//
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            //----sql injection---//
            cmd.CommandText = "Select * from LOGIN  " +
                " where USER_NAME='" + uname + "' and PASSWORD='" + pwd + "'";

            //-----Step 3  sql datareader ----//
            SqlDataReader re = cmd.ExecuteReader();
            if (re.HasRows)
            {
                re.Read();

                Session["CUSTOMER_ID"] = re["CUSTOMER_ID"].ToString();
                Session["USER_NAME"] = re["USER_NAME"].ToString();
                Session["TYPE"] = re["MEM_TYPE"].ToString();


                return RedirectToAction("CheckOut");
                //Response.Redirect("CheckOut"); 
            }
            else
            {
                TempData["Msg"] = "Invalid User ID/Passward";
                return RedirectToAction("Index");
                //Response.Redirect("Index");
            }
            return View();
        }

        public ActionResult CheckOut()
        {
           ViewBag.uname = Session["USER_NAME"].ToString();
            return View();
        }

    }


}
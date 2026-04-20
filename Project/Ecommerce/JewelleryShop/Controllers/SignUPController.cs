using JewelleryShop.Models;
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
    public class SignUPController : Controller
    {
        public JsonResult CheckUserName(string EMAIL_ID)
        {
            if (getEmail(EMAIL_ID))
            {
                return Json("Email ID alreday exist plz choose different...", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: SignUP
        public ActionResult Index()
        {
            getCountry();
            getCity();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Registration m,string country,string city)
        {
            getCountry();
            getCity();

            if (ModelState.IsValid)
            {
                //----Step 1 sql connection----//
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
                conn.Open();

                //----Step 2 sql command----//
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "REGISTRATION";

                //----step 3 parameter
                SqlParameter pCUSTOMER_NAME = new SqlParameter("CUSTOMER_NAME", SqlDbType.VarChar, 100);
                SqlParameter pEMAIL_ID = new SqlParameter("EMAIL_ID", SqlDbType.VarChar, 50);
                SqlParameter pCOUNTRY_ID = new SqlParameter("COUNTRY_ID", SqlDbType.Int, 10);
                SqlParameter pCITY_ID = new SqlParameter("CITY_ID", SqlDbType.Int, 10);
                SqlParameter pPIN_CODE = new SqlParameter("PIN_CODE", SqlDbType.Int, 10);
                SqlParameter pKEYWORD = new SqlParameter("KEYWORD", SqlDbType.VarChar, 50);
                SqlParameter pADDRESS = new SqlParameter("ADDRESS", SqlDbType.VarChar, 50);
                SqlParameter pEMAIL_ID_OUT = new SqlParameter("EMAIL_ID_OUT", SqlDbType.VarChar, 50);
                SqlParameter pPWD = new SqlParameter("PWD", SqlDbType.VarChar, 50);
                SqlParameter pCUSTOMER_ID = new SqlParameter("CUSTOMER_ID", SqlDbType.Int, 10);

                //--step 4 value
                pCUSTOMER_NAME.Value = m.CUSTOMER_NAME;
                pEMAIL_ID.Value = m.EMAIL_ID;
                pCOUNTRY_ID.Value = country;
                pCITY_ID.Value = city;
                pPIN_CODE.Value = m.PIN_CODE;
                pKEYWORD.Value = m.KEYWORD;
                pADDRESS.Value = m.ADDRESS;
                pPWD.Value = m.PASSWORD;

                pEMAIL_ID_OUT.Direction = ParameterDirection.Output;
                pCUSTOMER_ID.Direction = ParameterDirection.Output;

                //step 5 value
                cmd.Parameters.Add(pCUSTOMER_NAME);
                cmd.Parameters.Add(pEMAIL_ID);
                cmd.Parameters.Add(pCOUNTRY_ID);
                cmd.Parameters.Add(pCITY_ID);
                cmd.Parameters.Add(pPIN_CODE);
                cmd.Parameters.Add(pKEYWORD);
                cmd.Parameters.Add(pADDRESS);
                cmd.Parameters.Add(pPWD);
                cmd.Parameters.Add(pEMAIL_ID_OUT);
                cmd.Parameters.Add(pCUSTOMER_ID);

                int noofrec = cmd.ExecuteNonQuery();
                if (noofrec > 0)
                {
                    ViewBag.Msg = "Your Registration has Been Completed";
                }
                else
                {

                    ViewBag.Msg = "Your Email ID is alreday Exist Please Choose Different";

                }

            }
            return View(m);
        }
        
    

        public void getCountry()
        {
            List<COUNTRY> list = new List<COUNTRY>();

            //-----Step 1  make connection ----//
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
            conn.Open();

            //-----Step 2  command creation ----//
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Country";

            //-----Step 3  sql datareader ----//
            SqlDataReader re = cmd.ExecuteReader();
            if (re.HasRows)
            {
                while (re.Read())
                {
                    COUNTRY cat = new COUNTRY();
                    cat.COUNTRY_NAME = re["COUNTRY_NAME"].ToString();
                    cat.COUNTRY_ID = int.Parse(re["COUNTRY_ID"].ToString());

                    list.Add(cat);
                }
            }
            ViewBag.clist = list;
        }

        public void getCity()
        {
            List<CITY> list = new List<CITY>();

            //-----Step 1  make connection ----//
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
            conn.Open();

            //-----Step 2  command creation ----//
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from City";

            //-----Step 3  sql datareader ----//
            SqlDataReader re = cmd.ExecuteReader();
            if (re.HasRows)
            {
                while (re.Read())
                {
                    CITY cat = new CITY();
                    cat.CITY_NAME = re["CITY_NAME"].ToString();
                    cat.CITY_ID = int.Parse(re["CITY_ID"].ToString());

                    list.Add(cat);
                }
            }
            ViewBag.slist = list;
        }

        public Boolean getEmail(string email)
        {
            Boolean F = false;


            //-----Step 1  make connection ----//
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
            conn.Open();

            //-----Step 2  command creation ----//
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            //-----sql injection-----//
            cmd.CommandText = "Select * from CUSTOMER Where EMAIL_ID ='" + email + "'";

            //-----Step 3  sql datareader ----//
            SqlDataReader re = cmd.ExecuteReader();
            if (re.HasRows)
            {
                F = true;
            }
            return F;

        }
        public ActionResult ShowCaptcha()
        {
            CaptchaImageResult img = new CaptchaImageResult();
            return img;

        }
    }
}
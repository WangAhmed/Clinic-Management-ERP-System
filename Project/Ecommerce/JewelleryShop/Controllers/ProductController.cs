using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JewelleryShop.Models;

namespace JewelleryShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string cat = "%", string pname = "%" )
        {
            getCategory();
            getProduct(cat, pname);
            return View();
        }
        public void getCategory()
        {
            List<CatWiseProduct> list = new List<CatWiseProduct>();

            //-----Step 1  make connection ----//
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
            conn.Open();

            //-----Step 2  command creation ----//
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from catwiseProduct";

            //-----Step 3  sql datareader ----//
            SqlDataReader re = cmd.ExecuteReader();
            if (re.HasRows)
            {
                while (re.Read())
                {
                    CatWiseProduct cat = new CatWiseProduct();
                    cat.CategoryName = re["CategoryName"].ToString();
                    cat.noofproduct = int.Parse(re["noofproduct"].ToString());

                    list.Add(cat);
                }
            }
            ViewBag.list = list;
        }
 
        public void getProduct(string cat, string pname)
        {
            


            List<SearchView> plist = new List<SearchView>();

            //-----Step 1  make connection ----//
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
            conn.Open();

            //-----Step 2  command creation ----//
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            //-----sql injection '"++"' ---//
            cmd.CommandText = "Select * from SearchView " +
                "where categoryname like '" + cat + "'" +
                "and productname+CompanyName+CategoryName like '%" + pname + "%'";

            //-----Step 3  sql datareader ----//
            SqlDataReader re = cmd.ExecuteReader();
            if (re.HasRows)
            {
                while (re.Read())
                {
                    SearchView sp = new SearchView();
                    sp.ProductID = int.Parse(re["ProductID"].ToString());
                    sp.ProductName = re["ProductName"].ToString();
                    sp.CategoryName = re["CategoryName"].ToString();
                    sp.CompanyName = re["CompanyName"].ToString();
                    sp.UnitPrice = decimal.Parse(re["UnitPrice"].ToString());
                    sp.image = re["image"].ToString();
                    sp.UnitsInStock = short.Parse(re["UnitsInStock"].ToString());
                    sp.Description = re["Description"].ToString();

                    plist.Add(sp);
                }
            }

            ViewBag.plist = plist;






        }





    }
}
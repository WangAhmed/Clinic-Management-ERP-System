using JewelleryShop.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JewelleryShop.Controllers
{
    public class Product2Controller : Controller
    {
        SqlProductsRepository db;

        public Product2Controller()
        {
            string connstring = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ToString();
             db = new SqlProductsRepository(connstring);
        }
        // GET: Product2
        public ActionResult Index(int page=1,string cat="",string pname="")
        {
            getcategory();
            getProduct(page,cat,pname);
            return View();
        }
        
        public void getcategory()
        {
            var category = db.CategoriesWiseProducts.ToList();
            ViewBag.CatReclist = category;
        }

        public void getProduct(int page, string cat, string pname)
        {
            int pagesize = 10;

            var product = db.SearchViews.Where(p => p.ProductName.Contains(pname))
                .Skip((page-1)*pagesize)
                .Take(pagesize);

            var noofrecord = db.SearchViews.ToList()
                .Count();

            var noofpages = noofrecord / pagesize;

            ViewBag.noofpages = noofpages;
            ViewBag.page = page;
            ViewBag.nextpage = page + 1;
            ViewBag.previouspage = page - 1;

            ViewBag.ProdReclist = product;
        }

        public ActionResult productDetail(string pid)
        {
            var prod = db.SearchViews.Where(p => p.ProductID.Equals(pid)).First();
            ViewBag.proddetail = prod;
            return View();
        }

        public ActionResult AddProduct()
        {
            getcategory();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product m, HttpPostedFileBase image)
        {
            getcategory();

            if (m.ProductName.IsNullOrWhiteSpace())
            {
                ViewBag.Msg = "Name must be entered";
            }

            else
            {
                string path = Server.MapPath("~\\img\\");
                string filename = image.FileName;
                path += filename;

                m.image = "\\img\\" + filename;
                image.SaveAs(path);

                if (ModelState.IsValid)
                {
                    m.SupplierID = 1;
                    db.SaveProduct(m);
                    ViewBag.Msg = "Your Record has been saved plz check";
                }
            }
            return View();
        }
        public ActionResult EditProduct(int pid)
        {
            Product Q = db.Products.Where(p => p.ProductID == pid).FirstOrDefault();
            getcategory();
            return View(Q);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product m, HttpPostedFileBase image)
        {
            getcategory();


            if (m.ProductName.IsNullOrWhiteSpace())
            {
                ViewBag.Msg = "Name must be entered";
            }

            else
            {
                string path = Server.MapPath("~\\img\\");
                string filename = image.FileName;
                path += filename;

                m.image = "\\img\\" + filename;
                image.SaveAs(path);

                if (ModelState.IsValid)
                {
                    m.SupplierID = 1;
                    db.SaveProduct(m);
                    ViewBag.Msg = "Your Record has been updated plz check";
                }
            }
            return View(m);
        }

        public ActionResult DeleteProduct(int pid)
        {
            var Q = db.Products.Where(p => p.ProductID == pid).FirstOrDefault();

            try
            {
                db.DeleteProduct(Q);
                ViewBag.Msg = "Your Product " + Q.ProductName + " has been deleted";
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "You can not delete this product b/c" + ex.Message;
            }

            getcategory();
            return View(Q);
        }


        public ActionResult AddToCart(int pid)
        {
            var Q = db.Products.Where(p => p.ProductID == pid).FirstOrDefault();

            getCart().AddItem(Q, 1);

            var shoplist = getCart().Lines.ToList();

            var T = getCart().ComputeTotalValue();
            var S = (getCart().ComputeTotalValue() * 5) / 100;

            ViewBag.SubTotal = T;
            ViewBag.Shipping = S;
            ViewBag.GTotal = T + S;
            ViewBag.list = shoplist;

            return View();
        }

        public ActionResult ShoppingCart()
        {
            var shoplist = getCart().Lines.ToList();

            var T = getCart().ComputeTotalValue();
            var S = (getCart().ComputeTotalValue() * 5) / 100;

            ViewBag.SubTotal = T;
            ViewBag.Shipping = S;
            ViewBag.GTotal = T + S;
            ViewBag.list = shoplist;

            return View();
        }

        public Cart getCart()
        {
            Cart cart = (Cart)Session["cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }

            return cart;
        }

        public ActionResult DeleteCart(int pid)
        {
            var Q = db.Products.Where(p => p.ProductID == pid).FirstOrDefault();

            getCart().RemoveLine(Q);

            var shoplist = getCart().Lines.ToList();

            var T = getCart().ComputeTotalValue();
            var S = (getCart().ComputeTotalValue() * 5) / 100;

            ViewBag.SubTotal = T;
            ViewBag.Shipping = S;
            ViewBag.GTotal = T + S;
            ViewBag.list = shoplist;

            return RedirectToAction("ShoppingCart");
        }

        public ActionResult ShoppingCartPartial()
        {
            var shoplist = getCart().Lines.ToList();

            var T = getCart().ComputeTotalValue();
            var S = (getCart().ComputeTotalValue() * 5) / 100;

            ViewBag.SubTotal = T;
            ViewBag.Shipping = S;
            ViewBag.GTotal = T + S;
            ViewBag.list = shoplist;

            return View();
        }








        /*
        public ActionResult AddProduct()
        {
            getcategory();          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product m, HttpPostedFileBase image)
        {
            getcategory();

            if (m.ProductName.IsNullOrWhiteSpace())
            {
                ViewBag.Msg = "Name must be entered";
            }

            else
            {
                string path = Server.MapPath("~\\data\\");
                string filename = image.FileName;
                path += filename;

                m.image = "\\data\\" + filename;
                image.SaveAs(path);

                if (ModelState.IsValid)
                {
                    db.SaveProduct(m);
                    ViewBag.Msg = "Your Record has been saved plz check";
                }
            }
            return View();
        }

        public ActionResult EditProduct(int pid)
        {
            var Q = db.Products.Where(p => p.ProductID == pid).FirstOrDefault();

            getCat();
            getSup();
            return View(Q);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product m, HttpPostedFileBase image)
        {
            getCat();
            getSup();

            if (m.ProductName.IsNullOrWhiteSpace())
            {
                ViewBag.Msg = "Name must be entered";
            }

            else
            {
                string path = Server.MapPath("~\\data\\");
                string filename = image.FileName;
                path += filename;

                m.image = "\\data\\" + filename;
                image.SaveAs(path);

                if (ModelState.IsValid)
                {
                    db.SaveProduct(m);
                    ViewBag.Msg = "Your Record has been updated plz check";
                }
            }
            return View(m);
        }

        public ActionResult DeleteProduct(int pid)
        {
            var Q = db.Products.Where(p => p.ProductID == pid).FirstOrDefault();

            try
            {
                db.DeleteProduct(Q);
                ViewBag.Msg = "Your Product " + Q.ProductName + " has been deleted";
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "You can not delete this product b/c" + ex.Message;
            }

            getCat();
            getSup();
            return View(Q);
        }

        public ActionResult AddToCart(int pid)
        {
            var Q = db.Products.Where(p => p.ProductID == pid).FirstOrDefault();

            getCart().AddItem(Q, 1);

            var shoplist = getCart().Lines.ToList();

            var T = getCart().ComputeTotalValue();
            var S = (getCart().ComputeTotalValue() * 5) / 100;

            ViewBag.SubTotal = T;
            ViewBag.Shipping = S;
            ViewBag.GTotal = T + S;
            ViewBag.list = shoplist;

            return View();
        }

        public ActionResult ShoppingCart()
        {
            var shoplist = getCart().Lines.ToList();

            var T = getCart().ComputeTotalValue();
            var S = (getCart().ComputeTotalValue() * 5) / 100;

            ViewBag.SubTotal = T;
            ViewBag.Shipping = S;
            ViewBag.GTotal = T + S;
            ViewBag.list = shoplist;

            return View();
        }


        public Cart getCart()
        {
            Cart cart = (Cart)Session["cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }

            return cart;
        }

        public ActionResult DeleteCart(int pid)
        {
            var Q = db.Products.Where(p => p.ProductID == pid).FirstOrDefault();

            getCart().RemoveLine(Q);

            var shoplist = getCart().Lines.ToList();

            var T = getCart().ComputeTotalValue();
            var S = (getCart().ComputeTotalValue() * 5) / 100;

            ViewBag.SubTotal = T;
            ViewBag.Shipping = S;
            ViewBag.GTotal = T + S;
            ViewBag.list = shoplist;

            return RedirectToAction("ShoppingCart");
        }


        public ActionResult ShoppingCartPartial()
        {
            var shoplist = getCart().Lines.ToList();

            var T = getCart().ComputeTotalValue();
            var S = (getCart().ComputeTotalValue() * 5) / 100;

            ViewBag.SubTotal = T;
            ViewBag.Shipping = S;
            ViewBag.GTotal = T + S;
            ViewBag.list = shoplist;

            return View();
        }

        */



    }
}
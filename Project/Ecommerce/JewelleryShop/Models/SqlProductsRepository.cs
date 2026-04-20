using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace JewelleryShop.Models
{
    public class SqlProductsRepository : IProductsRepository
    {
        private Table<Product> productsTable;
        private Table<Category> CategoryTable;
        private Table<Supplier> SupplierTable;
        private Table<CatWiseProduct> CatWiseProductTable;
        private Table<SearchView> searchviewtable ;


        public SqlProductsRepository(string connectionString)
        {
            DataContext db = new DataContext(connectionString);
            productsTable = db.GetTable<Product>();
            CategoryTable = db.GetTable<Category>();
            SupplierTable = db.GetTable<Supplier>();
            CatWiseProductTable = db.GetTable<CatWiseProduct>();
            searchviewtable = db.GetTable<SearchView>();
        }
        public IQueryable<Product> Products
        {
            get
            {
                 return productsTable;
            }
        }

        public IQueryable<Category> Categories
        {
            get
            {
                return CategoryTable;
            }
        }

        public IQueryable<Supplier> Suppliers
        {
            get
            {
                return SupplierTable;
            }
        }

        public IQueryable<CatWiseProduct> CategoriesWiseProducts
        {
            get
            {
                return CatWiseProductTable;
            }
        }

        public IQueryable<SearchView> SearchViews
        {
            get
            {
                return searchviewtable;
            }
        }

        public void DeleteProduct(Product product)
        {
            productsTable.DeleteOnSubmit(product);
            productsTable.Context.SubmitChanges();
        }

        public void SaveProduct(Product product)
        {
            //if its a new product,just attach it to the datacontext
            if (product.ProductID == 0)
                productsTable.InsertOnSubmit(product);
            else
            {
                //if we are updating an excisting product,tell the datacontext
                //to be the responsible for saving the instance
                productsTable.Attach(product);
                //also tell the datacontext to detect any  changes since the last change 
                productsTable.Context.Refresh(RefreshMode.KeepCurrentValues, product);


            }
            productsTable.Context.SubmitChanges();
        }
    }
}
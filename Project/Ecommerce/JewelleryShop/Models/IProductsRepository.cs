using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelleryShop.Models
{
    public interface IProductsRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Supplier> Suppliers { get; }
        IQueryable<CatWiseProduct> CategoriesWiseProducts { get; }

        IQueryable<SearchView> SearchViews { get; }

        //ADD,UPDATE,DELETE

        void SaveProduct(Product product);
        void DeleteProduct(Product product);
    }
}
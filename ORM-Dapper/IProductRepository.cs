using System;
using System.Collections.Generic;

namespace ORM_Dapper
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        void UpdateProduct(Product product);
        public void InsertProduct(Product productToInsert);
        public void DeleteProduct(Product product);
    }
}

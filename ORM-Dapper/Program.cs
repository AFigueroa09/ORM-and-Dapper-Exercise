using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            DapperDepartmentRepository departmentRepo = new DapperDepartmentRepository(conn);

            departmentRepo.InsertDepartment("Tony's New Department");

            var departments = departmentRepo.GetAllDepartments();

            foreach (var department in departments)
            {
                Console.WriteLine(department.DepartmentID);
                Console.WriteLine(department.Name);
                Console.WriteLine();
                Console.WriteLine();
            }

            ProductRepository productRepo = new ProductRepository(conn);

            Product product = new Product();
            product.Name = "Dell Inspiron";
            product.OnSale = 0;
            product.Price = 2000;
            product.CategoryID = 1;
            product.StockLevel = 100;

            productRepo.InsertProduct(product);

            var products = productRepo.GetAllProducts();

            Product lastProduct = new Product();
            foreach (var currentProduct in products)
            {
                Console.WriteLine(currentProduct.ProductID);
                Console.WriteLine(currentProduct.Name);
                Console.WriteLine();
                Console.WriteLine();

                lastProduct = currentProduct;
            }

            Product dellInspiron = productRepo.GetProduct(lastProduct.ProductID);
            dellInspiron.StockLevel = dellInspiron.StockLevel--;

            productRepo.UpdateProduct(dellInspiron);

            products = productRepo.GetAllProducts();

            foreach (var currentProduct in products)
            {
                Console.WriteLine(currentProduct.ProductID);
                Console.WriteLine(currentProduct.Name);
                Console.WriteLine();
                Console.WriteLine();
            }

            productRepo.DeleteProduct(dellInspiron);

            products = productRepo.GetAllProducts();

            foreach (var currentProduct in products)
            {
                Console.WriteLine(currentProduct.ProductID);
                Console.WriteLine(currentProduct.Name);
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}

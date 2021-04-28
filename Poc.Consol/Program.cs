using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Poc.Bll;

using Poc.Model;


namespace Poc.Consol
{
    class Program
    {

        
        static void Main(string[] args)
        {

            Product product = NewMethod();

            ProductService productService = new ProductService();
            var result = productService.CreateProduct(product);
            if (result.Result)
            {
                Console.WriteLine("success");
            }
            else
            {
                ResourceManager rm = new ResourceManager("Poc.Consol.Resource1", Assembly.GetExecutingAssembly());

                var errors = result.ReplaceValidationKeys(rm);
                string message = "";
                foreach (var error in errors)
                {
                    
                    message += "<br>" + error;
                }

                Console.WriteLine(message);
            }
        }

        private static Product NewMethod()
        {
            Category category = new Category();
            category.Name = "cat 19";

            Product product = new Product();
            product.Category = category;
            product.Name = "product 19";
            product.Price = 9.5;
            product.ProductId = Guid.NewGuid();
            return product;
        }
    }
}

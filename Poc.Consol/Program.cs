using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poc.Bll;

using Poc.Model;
using Poc.Repository;

namespace Poc.Consol
{
    class Program
    {

        
        static void Main(string[] args)
        {
            UnitOfWork unitOfWork = Entities.UOW;

            Category category = new Category();
            category.Name = "cat 19";
            
            Product product = new Product();
            product.Category = category;
            product.Name = "product 19";
            product.Price = 9.5;
            product.ProductId = Guid.NewGuid();
            
            unitOfWork.Repository<Product>().Add(product);
            unitOfWork.SaveChanges();
        }
    }
}

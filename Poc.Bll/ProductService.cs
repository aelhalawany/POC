using Poc.Core.ActionResult;
using Poc.Core.Uow;
using Poc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Bll
{
    public class ProductService 
    {
        public ActionResult CreateProduct(Product product)
        {
            //using (UnitOfWork uow = Entities.UOW)
            //{
            //    uow.Repository<Product>().Add(product);
            //    uow.SaveChanges();
            //}
            ActionResult actionResult = new ActionResult();
            actionResult.ValidationErrorKeys.Add("Duplicate");
            actionResult.Result = false;
            return actionResult;
           
        }
    }
}

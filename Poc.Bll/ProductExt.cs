using Poc.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Bll
{
    public class ProductExt
    {
        public Product GetFullDetails(Guid landId)
        {
            return Entities.UOW.Repository<Product>().GetByQuery(l => l.ProductId == landId)
                .Include(r => r.Category)
                .FirstOrDefault();
        }
    }
}

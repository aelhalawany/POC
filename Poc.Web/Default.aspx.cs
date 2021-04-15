using Poc.Bll;
using Poc.Model;
using Poc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Poc.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
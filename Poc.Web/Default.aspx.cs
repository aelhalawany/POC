using Poc.Bll;
using Poc.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Poc.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Product product = NewMethod();

            ProductService productService = new ProductService();
            var result = productService.CreateProduct(product);
            if (result.Result)
            {
                Response.Write("Succuess");
            }
            else
            {
                var errors = result.ReplaceValidationKeys("POCRes");
                string message = "";
                foreach (var error in errors)
                {
                    message += "<br>" + error;
                }

                Response.Write(message);
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
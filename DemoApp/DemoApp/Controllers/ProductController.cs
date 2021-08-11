using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductEntity;

namespace DemoApp.Controllers
{
    public class ProductController : ApiController
    {
        public IEnumerable<Product> Get()
        {
            using (ProductEntities entity = new ProductEntities())
            {
                return entity.Products.ToList();
            }
        }
    }
}

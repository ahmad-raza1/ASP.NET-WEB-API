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

        public HttpResponseMessage Get(int id)
        {
            using (ProductEntities entity = new ProductEntities())
            {
                Product item = entity.Products.FirstOrDefault(x => x.ProductId == id);
                if (item == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Product with id {id} does not exist!");
                    
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
        }

        public HttpResponseMessage Post([FromBody] Product item)
        {
            try
            {
                using (ProductEntities entity = new ProductEntities())
                {
                    entity.Products.Add(item);
                    entity.SaveChanges();

                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, item);
                    message.Headers.Location = new Uri(Request.RequestUri + item.ProductId.ToString());
                    
                    return message;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Product newItem)
        {
            try
            {
                using (ProductEntities entity = new ProductEntities())
                {
                    Product existingItem = entity.Products.FirstOrDefault(x => x.ProductId == id);
                    if (existingItem == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Product with id {id} does not exist!");

                    existingItem.Name = newItem.Name;
                    existingItem.Quantity = newItem.Quantity;
                    existingItem.BoxSize = newItem.BoxSize;
                    existingItem.Price = newItem.Price;
                    entity.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, existingItem);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (ProductEntities entity = new ProductEntities())
                {
                    Product itemToBeDeleted = entity.Products.FirstOrDefault(x => x.ProductId == id);
                    if (itemToBeDeleted == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Product with id {id} does not exist!");

                    entity.Products.Remove(itemToBeDeleted);
                    entity.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, itemToBeDeleted);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}

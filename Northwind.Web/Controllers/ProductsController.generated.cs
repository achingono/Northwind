namespace Northwind.Web.Controllers
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
	using Northwind.Data;
	using Northwind.Web.Models;
    using Northwind.Web.WebEvents;

    /// <summary>
    /// TODO: Update summary
    /// </summary>	
    public partial class ProductsController : BaseApiController
    {
        /// <summary>
        /// TODO: Update summary
        /// </summary>
		Func<Product, ProductModel> selector = x => x.TransformTo<ProductModel>();
        
		/// <summary>
        /// TODO: Update summary
        /// </summary>
        public ProductsController()
		{
		}

        /// <summary>
        /// GET /api/products
        /// </summary>
        /// <returns></returns>
		[Queryable]
        public IEnumerable<ProductModel> Get()
        {
			return this.DbContext.Products.AccessibleTo(this.User).Select(selector);
        }

        /// <summary>
        /// GET /api/products/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductModel Get(int id)
        {
            var entity = this.DbContext.Products.Find(id);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            
            if (!entity.IsAccessibleTo(this.User)) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            return selector(entity);
        }

        /// <summary>
        /// POST /api/products
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(ProductModel model)
        {
			var context = this.DbContext;
            
            if (!this.User.CanCreate<Product>()) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

			// transform the ProductModel to Product
			var entity = model.TransformTo<Product>();
            
			// update Supplier property
            if (entity.Supplier == null 
                || entity.Supplier.Id != model.SupplierId)
            {
		        entity.Supplier = context.Suppliers.Find(model.SupplierId);
            }
            
			// update Category property
            if (entity.Category == null 
                || entity.Category.Id != model.CategoryId)
            {
		        entity.Category = context.Categories.Find(model.CategoryId);
            }
		
            // add the entity
            context.Products.Add(entity);

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new ProductCreatedEvent(entity).Raise();

			// create response
			var response = Request.CreateResponse<ProductModel>(HttpStatusCode.Created, selector(entity));
			string uri = Url.Link("Api", new { id = entity.Id });
			response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// PUT /api/products/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ProductModel Put(int id, ProductModel model)
        {
			var context = this.DbContext;
            var entity = context.Products.Find(id);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            
            if (!this.User.CanUpdate(entity)) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

			// update the entity
			entity.UpdateFrom(model);
            
			// update Supplier property
			if (entity.Supplier == null 
                || entity.Supplier.Id != model.SupplierId)
            {
		        entity.Supplier = context.Suppliers.Find(model.SupplierId);
            }
            
			// update Category property
			if (entity.Category == null 
                || entity.Category.Id != model.CategoryId)
            {
		        entity.Category = context.Categories.Find(model.CategoryId);
            }
		

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new ProductUpdatedEvent(entity).Raise();

            return selector(entity);
        }

        /// <summary>
        /// DELETE /api/products/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, ProductModel model)
        {
			var context = this.DbContext;
            var entity = context.Products.Find(id);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            
            if (!this.User.CanDelete(entity)) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
         
            // create the web event
            var webEvent = new ProductDeletedEvent(entity);

			// delete the entity
			context.Products.Remove(entity);

            // persist changes to the database
            context.SaveChanges();

            // fire the web event
            webEvent.Raise();

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}
	}
}
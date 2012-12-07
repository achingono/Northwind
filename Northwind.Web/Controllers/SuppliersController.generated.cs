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
    public partial class SuppliersController : BaseApiController
    {
        /// <summary>
        /// TODO: Update summary
        /// </summary>
		Func<Supplier, SupplierModel> selector = x => x.TransformTo<SupplierModel>();
        
		/// <summary>
        /// TODO: Update summary
        /// </summary>
        public SuppliersController()
		{
		}

        /// <summary>
        /// GET /api/suppliers
        /// </summary>
        /// <returns></returns>
		[Queryable]
        public IEnumerable<SupplierModel> Get()
        {
			return this.DbContext.Suppliers.AccessibleTo(this.User).Select(selector);
        }

        /// <summary>
        /// GET /api/suppliers/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SupplierModel Get(int id)
        {
            var entity = this.DbContext.Suppliers.Find(id);

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
        /// POST /api/suppliers
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(SupplierModel model)
        {
			var context = this.DbContext;
            
            if (!this.User.CanCreate<Supplier>()) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

			// transform the SupplierModel to Supplier
			var entity = model.TransformTo<Supplier>();
		
            // add the entity
            context.Suppliers.Add(entity);

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new SupplierCreatedEvent(entity).Raise();

			// create response
			var response = Request.CreateResponse<SupplierModel>(HttpStatusCode.Created, selector(entity));
			string uri = Url.Link("Api", new { id = entity.Id });
			response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// PUT /api/suppliers/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public SupplierModel Put(int id, SupplierModel model)
        {
			var context = this.DbContext;
            var entity = context.Suppliers.Find(id);

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
		

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new SupplierUpdatedEvent(entity).Raise();

            return selector(entity);
        }

        /// <summary>
        /// DELETE /api/suppliers/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, SupplierModel model)
        {
			var context = this.DbContext;
            var entity = context.Suppliers.Find(id);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            
            if (!this.User.CanDelete(entity)) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
         
            // create the web event
            var webEvent = new SupplierDeletedEvent(entity);

			// delete the entity
			context.Suppliers.Remove(entity);

            // persist changes to the database
            context.SaveChanges();

            // fire the web event
            webEvent.Raise();

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}
	}
}
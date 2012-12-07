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
    public partial class CustomersController : BaseApiController
    {
        /// <summary>
        /// TODO: Update summary
        /// </summary>
		Func<Customer, CustomerModel> selector = x => x.TransformTo<CustomerModel>();
        
		/// <summary>
        /// TODO: Update summary
        /// </summary>
        public CustomersController()
		{
		}

        /// <summary>
        /// GET /api/customers
        /// </summary>
        /// <returns></returns>
		[Queryable]
        public IEnumerable<CustomerModel> Get()
        {
			return this.DbContext.Customers.AccessibleTo(this.User).Select(selector);
        }

        /// <summary>
        /// GET /api/customers/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustomerModel Get(int id)
        {
            var entity = this.DbContext.Customers.Find(id);

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
        /// POST /api/customers
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(CustomerModel model)
        {
			var context = this.DbContext;
            
            if (!this.User.CanCreate<Customer>()) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

			// transform the CustomerModel to Customer
			var entity = model.TransformTo<Customer>();
		
            // generate a new customer Id
            entity.Id = model.GenerateId();
          
            // add the entity
            context.Customers.Add(entity);

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new CustomerCreatedEvent(entity).Raise();

			// create response
			var response = Request.CreateResponse<CustomerModel>(HttpStatusCode.Created, selector(entity));
			string uri = Url.Link("Api", new { id = entity.Id });
			response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// PUT /api/customers/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public CustomerModel Put(int id, CustomerModel model)
        {
			var context = this.DbContext;
            var entity = context.Customers.Find(id);

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
            new CustomerUpdatedEvent(entity).Raise();

            return selector(entity);
        }

        /// <summary>
        /// DELETE /api/customers/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, CustomerModel model)
        {
			var context = this.DbContext;
            var entity = context.Customers.Find(id);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            
            if (!this.User.CanDelete(entity)) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
         
            // create the web event
            var webEvent = new CustomerDeletedEvent(entity);

			// delete the entity
			context.Customers.Remove(entity);

            // persist changes to the database
            context.SaveChanges();

            // fire the web event
            webEvent.Raise();

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}
	}
}
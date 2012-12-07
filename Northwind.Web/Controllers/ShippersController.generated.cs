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
    public partial class ShippersController : BaseApiController
    {
        /// <summary>
        /// TODO: Update summary
        /// </summary>
		Func<Shipper, ShipperModel> selector = x => x.TransformTo<ShipperModel>();
        
		/// <summary>
        /// TODO: Update summary
        /// </summary>
        public ShippersController()
		{
		}

        /// <summary>
        /// GET /api/shippers
        /// </summary>
        /// <returns></returns>
		[Queryable]
        public IEnumerable<ShipperModel> Get()
        {
			return this.DbContext.Shippers.AccessibleTo(this.User).Select(selector);
        }

        /// <summary>
        /// GET /api/shippers/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ShipperModel Get(int id)
        {
            var entity = this.DbContext.Shippers.Find(id);

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
        /// POST /api/shippers
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(ShipperModel model)
        {
			var context = this.DbContext;
            
            if (!this.User.CanCreate<Shipper>()) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

			// transform the ShipperModel to Shipper
			var entity = model.TransformTo<Shipper>();
		
            // add the entity
            context.Shippers.Add(entity);

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new ShipperCreatedEvent(entity).Raise();

			// create response
			var response = Request.CreateResponse<ShipperModel>(HttpStatusCode.Created, selector(entity));
			string uri = Url.Link("Api", new { id = entity.Id });
			response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// PUT /api/shippers/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ShipperModel Put(int id, ShipperModel model)
        {
			var context = this.DbContext;
            var entity = context.Shippers.Find(id);

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
            new ShipperUpdatedEvent(entity).Raise();

            return selector(entity);
        }

        /// <summary>
        /// DELETE /api/shippers/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, ShipperModel model)
        {
			var context = this.DbContext;
            var entity = context.Shippers.Find(id);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            
            if (!this.User.CanDelete(entity)) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
         
            // create the web event
            var webEvent = new ShipperDeletedEvent(entity);

			// delete the entity
			context.Shippers.Remove(entity);

            // persist changes to the database
            context.SaveChanges();

            // fire the web event
            webEvent.Raise();

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}
	}
}
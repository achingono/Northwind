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
    public partial class OrderDetailsController : BaseApiController
    {
        /// <summary>
        /// TODO: Update summary
        /// </summary>
		Func<OrderDetail, OrderDetailModel> selector = x => x.TransformTo<OrderDetailModel>();
        
		/// <summary>
        /// TODO: Update summary
        /// </summary>
        public OrderDetailsController()
		{
		}

        /// <summary>
        /// GET /api/orders
        /// </summary>
        /// <returns></returns>
        [Queryable]
        public IEnumerable<OrderDetailModel> Get()
        {
			return this.DbContext.OrderDetails.AccessibleTo(this.User).Select(selector);
        }

        /// <summary>
        /// GET /api/orders/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderDetailModel Get(int id)
        {
            var entity = this.DbContext.OrderDetails.Find(id);

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
        /// POST /api/orders
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(OrderDetailModel model)
        {
			var context = this.DbContext;
            
            if (!this.User.CanCreate<OrderDetail>()) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

			// transform the OrderDetailModel to OrderDetail
			var entity = model.TransformTo<OrderDetail>();
            
			// update Order property
            if (entity.Order == null 
                || entity.Order.Id != model.OrderId)
            {
		        entity.Order = context.Orders.Find(model.OrderId);
            }
            
			// update Product property
            if (entity.Product == null 
                || entity.Product.Id != model.ProductId)
            {
		        entity.Product = context.Products.Find(model.ProductId);
            }
		
            // add the entity
            context.OrderDetails.Add(entity);

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new OrderDetailCreatedEvent(entity).Raise();

			// create response
			var response = Request.CreateResponse<OrderDetailModel>(HttpStatusCode.Created, selector(entity));
			string uri = Url.Link("Api", new { id = model.Id });
			response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// PUT /api/orders/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public OrderDetailModel Put(int id, OrderDetailModel model)
        {
			var context = this.DbContext;
            var entity = context.OrderDetails.Find(id);

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
            
			// update Order property
			if (entity.Order == null 
                || entity.Order.Id != model.OrderId)
            {
		        entity.Order = context.Orders.Find(model.OrderId);
            }
            
			// update Product property
			if (entity.Product == null 
                || entity.Product.Id != model.ProductId)
            {
		        entity.Product = context.Products.Find(model.ProductId);
            }
		

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new OrderDetailUpdatedEvent(entity).Raise();

            return selector(entity);
        }

        /// <summary>
        /// DELETE /api/orders/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, OrderDetailModel model)
        {
			var context = this.DbContext;
            var entity = context.OrderDetails.Find(id);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            
            if (!this.User.CanDelete(entity)) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
         
            // create the web event
            var webEvent = new OrderDetailDeletedEvent(entity);

			// delete the entity
			context.OrderDetails.Remove(entity);

            // persist changes to the database
            context.SaveChanges();

            // fire the web event
            webEvent.Raise();

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}
	}
}
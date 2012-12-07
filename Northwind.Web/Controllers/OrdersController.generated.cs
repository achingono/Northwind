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
    public partial class OrdersController : BaseApiController
    {
        /// <summary>
        /// TODO: Update summary
        /// </summary>
        Func<Order, OrderModel> selector = x => x.TransformTo<OrderModel>();

        /// <summary>
        /// TODO: Update summary
        /// </summary>
        public OrdersController()
        {
        }

        /// <summary>
        /// GET /api/orders
        /// </summary>
        /// <returns></returns>
        [Queryable]
        public IEnumerable<OrderModel> Get()
        {
            return this.DbContext.Orders.AccessibleTo(this.User).Select(selector);
        }

        /// <summary>
        /// GET /api/orders/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderModel Get(int id)
        {
            var entity = this.DbContext.Orders.Find(id);

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
        public HttpResponseMessage Post(OrderModel model)
        {
            var context = this.DbContext;

            if (!this.User.CanCreate<Order>())
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

            // transform the OrderModel to Order
            var entity = model.TransformTo<Order>();

            // update Customer property
            if (entity.Customer == null
                || entity.Customer.Id != model.CustomerId)
            {
                entity.Customer = context.Customers.Find(model.CustomerId);
            }

            // update Employee property
            if (entity.Employee == null
                || entity.Employee.Id != model.EmployeeId)
            {
                entity.Employee = context.Employees.Find(model.EmployeeId);
            }

            // update Shipper property
            if (entity.Shipper == null
                || entity.Shipper.Id != model.ShipperId)
            {
                entity.Shipper = context.Shippers.Find(model.ShipperId);
            }

            // update order details from cookie
            UpdateOrderDetailsFromCookie(entity, context, "OrderDetails");

            // add the entity
            context.Orders.Add(entity);

            // persist changes to the database
            context.SaveChanges();

            // fire the web event
            new OrderCreatedEvent(entity).Raise();

            // create response
            var response = Request.CreateResponse<OrderModel>(HttpStatusCode.Created, selector(entity));
            string uri = Url.Link("Api", new { id = entity.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// PUT /api/orders/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public OrderModel Put(int id, OrderModel model)
        {
            var context = this.DbContext;
            var entity = context.Orders.Find(id);

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

            // update Customer property
            if (entity.Customer == null
                || entity.Customer.Id != model.CustomerId)
            {
                entity.Customer = context.Customers.Find(model.CustomerId);
            }

            // update Employee property
            if (entity.Employee == null
                || entity.Employee.Id != model.EmployeeId)
            {
                entity.Employee = context.Employees.Find(model.EmployeeId);
            }

            // update Shipper property
            if (entity.Shipper == null
                || entity.Shipper.Id != model.ShipperId)
            {
                entity.Shipper = context.Shippers.Find(model.ShipperId);
            }

            // update order details from cookie
            UpdateOrderDetailsFromCookie(entity, context, "OrderDetails");

            // persist changes to the database
            context.SaveChanges();

            // fire the web event
            new OrderUpdatedEvent(entity).Raise();

            return selector(entity);
        }

        /// <summary>
        /// DELETE /api/orders/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, OrderModel model)
        {
            var context = this.DbContext;
            var entity = context.Orders.Find(id);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            if (!this.User.CanDelete(entity))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

            // create the web event
            var webEvent = new OrderDeletedEvent(entity);

            // delete the entity
            context.Orders.Remove(entity);

            // persist changes to the database
            context.SaveChanges();

            // fire the web event
            webEvent.Raise();

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
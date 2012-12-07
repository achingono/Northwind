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
    public partial class EmployeesController : BaseApiController
    {
        /// <summary>
        /// TODO: Update summary
        /// </summary>
		Func<Employee, EmployeeModel> selector = x => x.TransformTo<EmployeeModel>();
        
		/// <summary>
        /// TODO: Update summary
        /// </summary>
        public EmployeesController()
		{
		}

        /// <summary>
        /// GET /api/employees
        /// </summary>
        /// <returns></returns>
		[Queryable]
        public IEnumerable<EmployeeModel> Get()
        {
			return this.DbContext.Employees.AccessibleTo(this.User).Select(selector);
        }

        /// <summary>
        /// GET /api/employees/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeModel Get(int id)
        {
            var entity = this.DbContext.Employees.Find(id);

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
        /// POST /api/employees
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(EmployeeModel model)
        {
			var context = this.DbContext;
            
            if (!this.User.CanCreate<Employee>()) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

			// transform the EmployeeModel to Employee
			var entity = model.TransformTo<Employee>();
            
			// update Manager property
            if (entity.Manager == null 
                || entity.Manager.Id != model.ManagerId)
            {
		        entity.Manager = context.Employees.Find(model.ManagerId);
            }
		
            // add the entity
            context.Employees.Add(entity);

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new EmployeeCreatedEvent(entity).Raise();

			// create response
			var response = Request.CreateResponse<EmployeeModel>(HttpStatusCode.Created, selector(entity));
			string uri = Url.Link("Api", new { id = entity.Id });
			response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// PUT /api/employees/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public EmployeeModel Put(int id, EmployeeModel model)
        {
			var context = this.DbContext;
            var entity = context.Employees.Find(id);

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
            
			// update Manager property
			if (entity.Manager == null 
                || entity.Manager.Id != model.ManagerId)
            {
		        entity.Manager = context.Employees.Find(model.ManagerId);
            }
		

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new EmployeeUpdatedEvent(entity).Raise();

            return selector(entity);
        }

        /// <summary>
        /// DELETE /api/employees/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, EmployeeModel model)
        {
			var context = this.DbContext;
            var entity = context.Employees.Find(id);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            
            if (!this.User.CanDelete(entity)) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
         
            // create the web event
            var webEvent = new EmployeeDeletedEvent(entity);

			// delete the entity
			context.Employees.Remove(entity);

            // persist changes to the database
            context.SaveChanges();

            // fire the web event
            webEvent.Raise();

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}
	}
}
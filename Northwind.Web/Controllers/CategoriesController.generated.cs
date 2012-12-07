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
    public partial class CategoriesController : BaseApiController
    {
        /// <summary>
        /// TODO: Update summary
        /// </summary>
		Func<Category, CategoryModel> selector = x => x.TransformTo<CategoryModel>();
        
		/// <summary>
        /// TODO: Update summary
        /// </summary>
        public CategoriesController()
		{
		}

        /// <summary>
        /// GET /api/categories
        /// </summary>
        /// <returns></returns>
		[Queryable]
        public IEnumerable<CategoryModel> Get()
        {
			return this.DbContext.Categories.AccessibleTo(this.User).Select(selector);
        }

        /// <summary>
        /// GET /api/categories/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryModel Get(int id)
        {
            var entity = this.DbContext.Categories.Find(id);

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
        /// POST /api/categories
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(CategoryModel model)
        {
			var context = this.DbContext;
            
            if (!this.User.CanCreate<Category>()) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

			// transform the CategoryModel to Category
			var entity = model.TransformTo<Category>();
		
            // add the entity
            context.Categories.Add(entity);

            // persist changes to the database
            context.SaveChanges();
            
            // fire the web event
            new CategoryCreatedEvent(entity).Raise();

			// create response
			var response = Request.CreateResponse<CategoryModel>(HttpStatusCode.Created, selector(entity));
			string uri = Url.Link("Api", new { id = entity.Id });
			response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// PUT /api/categories/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public CategoryModel Put(int id, CategoryModel model)
        {
			var context = this.DbContext;
            var entity = context.Categories.Find(id);

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
            new CategoryUpdatedEvent(entity).Raise();

            return selector(entity);
        }

        /// <summary>
        /// DELETE /api/categories/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id, CategoryModel model)
        {
			var context = this.DbContext;
            var entity = context.Categories.Find(id);

            if (entity == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            
            if (!this.User.CanDelete(entity)) 
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
         
            // create the web event
            var webEvent = new CategoryDeletedEvent(entity);

			// delete the entity
			context.Categories.Remove(entity);

            // persist changes to the database
            context.SaveChanges();

            // fire the web event
            webEvent.Raise();

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}
	}
}
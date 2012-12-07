namespace Northwind.Web.Controllers
{
    using System.Collections.Specialized;
    using System.Net.Http;
    using System.Web.Http;

     /// <summary>
     /// TODO: Update summary
     /// </summary>
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// TODO: Update summary
        /// </summary>
        public Northwind.Data.NorthwindContext DbContext { get; private set; }

        /// <summary>
        /// TODO: Update summary
        /// </summary>
        public BaseApiController()
        {
            this.DbContext = new Northwind.Data.NorthwindContext();
        }

        /// <summary>
        /// TODO: Update Summary
        /// </summary>
        protected NameValueCollection ParseQueryString()
        {
            return this.Request.RequestUri.ParseQueryString();
        }
    }
}

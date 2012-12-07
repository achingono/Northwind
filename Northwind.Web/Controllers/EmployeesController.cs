namespace Northwind.Web.Controllers
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;

    public partial class EmployeesController : BaseApiController
    {
        [HttpGet]
        public HttpResponseMessage Photo(int id)
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

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(entity.Photo);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return result;
        }
    }
}

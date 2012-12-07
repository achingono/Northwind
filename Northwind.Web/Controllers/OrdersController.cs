namespace Northwind.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Northwind.Data;
    using Northwind.Web.Models;

    /// <summary>
    /// TODO: Update summary
    /// </summary>
    public partial class OrdersController : BaseApiController
    {
        /// <summary>
        /// Retrieve order details stored in a cookie and update order
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="context"></param>
        /// <param name="cookieName"></param>
        private void UpdateOrderDetailsFromCookie(Order entity, NorthwindContext context, string cookieName)
        {
            var headers = this.Request.Headers.GetCookies(cookieName);
            var value = headers.SingleOrDefault(h => h.Cookies.Any(c => c.Name.Equals(cookieName)))
                               .Cookies.SingleOrDefault(c => c.Name.Equals(cookieName)).Value;
            var detailModels = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<OrderDetailModel>>(value);

            if (entity.Details == null)
            {
                entity.Details = new List<OrderDetail>();
            }

            foreach (var detailModel in detailModels)
            {
                OrderDetail detail = entity.Details.SingleOrDefault(d => d.ProductId == detailModel.ProductId);
                if (detailModel.Quantity <= 0 )
                {
                    if (detail != null)
                    {
                        entity.Details.Remove(detail);
                    }
                    continue;
                }

                var product = context.Products.Find(detailModel.ProductId);
                if (detail == null)
                {
                    detail = detailModel.TransformTo<OrderDetail>();
                    entity.Details.Add(detail);
                    detail.Product = product;
                }
                else
                {
                    detail.UpdateFrom(detailModel);
                }
            }
        }
    }
}

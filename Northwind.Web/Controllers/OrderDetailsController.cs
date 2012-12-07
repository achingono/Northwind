namespace Northwind.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Northwind.Web.Models;
using System.Web.Http;

    public partial class OrderDetailsController : BaseApiController
    {
        /// <summary>
        /// GET /api/orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<OrderDetailModel> Order(int id)
        {
            return this.DbContext.OrderDetails
                .Where(d => d.OrderId == id)
                .AccessibleTo(this.User).Select(selector);
        }
    }
}

namespace Northwind.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class OrderDetailModel
    {
        public string Id
        {
            get
            {
                return string.Format("{0}-{1}", this.OrderId, this.ProductId);
            }
        }

        [DataType(DataType.Currency)]
        public decimal Total
        {
            get
            {
                return (this.UnitPrice * this.Quantity) * Convert.ToDecimal(1 - this.Discount);
            }
        }
    }
}

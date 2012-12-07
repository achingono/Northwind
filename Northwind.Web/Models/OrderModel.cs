namespace Northwind.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class OrderModel
    {
        [DataType(DataType.Currency)]
        [Editable(false)]
        public decimal Total { get; set; }

        public OrderModel()
        {
            this.Ship = new Data.Ship();
        }
    }
}

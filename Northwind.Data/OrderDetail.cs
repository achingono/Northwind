using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Order Details")]
    public class OrderDetail
    {
        [Key]
        [Column("Order ID", Order = 0)]
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [Key]
        [Column("Product ID", Order = 1)]
        [ScaffoldColumn(false)]
        public int ProductId { get; set; }

        [Column("Unit Price")]
        [DataType("number")]
        public decimal UnitPrice { get; set; }

        [DataType("number")]
        public short Quantity { get; set; }

        [DataType("number")]
        public float Discount { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}

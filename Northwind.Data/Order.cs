using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Northwind.Data.Validation;

namespace Northwind.Data
{
    [Table("Orders")]
    [DisplayColumn("Name")]
    [CodeGenerated]
    public class Order
    {
        [Key]
        [Column("Order ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [NotMapped]
        public string Name
        {
            get
            {
                return string.Format("{0:0000}-{1:yyyy-MM-dd}", this.Id, this.OrderDate);
            }
        }

        [Required]
        [Column("Customer ID")]
        [MaxLength(5)]
        [ScaffoldColumn(false)]
        public string CustomerId { get; set; }

        [Column("Employee ID")]
        [ScaffoldColumn(false)]
        public int? EmployeeId { get; set; }

        public virtual Ship Ship { get; set; }

        [Column("Order Date")]
        [Display(Name = "Ordered")]
        [DataType(DataType.DateTime)]
        public DateTime? OrderDate { get; set; }

        [Column("Required Date")]
        [Display(Name = "Required")]
        [DataType(DataType.DateTime)]
        public DateTime? RequiredDate { get; set; }

        [Column("Shipped Date")]
        [Display(Name = "Shipped")]
        [DataType(DataType.DateTime)]
        public DateTime? ShippedDate { get; set; }

        [DataType("number")]
        public decimal? Freight { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [Column("Ship Via")]
        [ScaffoldColumn(false)]
        public int? Via { get; set; }

        [ForeignKey("Via")]
        public virtual Shipper Shipper { get; set; }

        public virtual ICollection<OrderDetail> Details { get; set; }
    }
}

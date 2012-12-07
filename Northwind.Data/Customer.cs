namespace Northwind.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
using Northwind.Data.Validation;

    [Table("Customers")]
    [DisplayColumn("Name")]
    public class Customer
    {
        [Key]
        [Column("Customer ID")]
        [MaxLength(5)]
        public string Id { get; set; }

        [Required]
        [Column("Company Name")]
        [MaxLength(40)]
        public string Name { get; set; }

        public virtual Contact Contact { get; set; }
    }
}

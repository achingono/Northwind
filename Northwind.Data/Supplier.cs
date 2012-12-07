using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Suppliers")]
    [DisplayColumn("Name")]
    public class Supplier
    {
        [Key]
        [Column("Supplier ID")]
        public int Id { get; set; }

        [Required]
        [Column("Company Name")]
        [MaxLength(40)]
        public string Name { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

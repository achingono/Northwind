using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Shippers")]
    [DisplayColumn("Name")]
    public class Shipper
    {
        [Key]
        [Column("Shipper ID")]
        public int Id { get; set; }
        
        [Required]
        [Column("Company Name")]
        [MaxLength(40)]
        public string Name { get; set; }
    }
}

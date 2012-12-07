using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Products")]
    [DisplayColumn("Name")]
    public class Product
    {
        [Key]
        [Column("Product ID")]
        public int Id { get; set; }

        [Column("Supplier ID")]
        [ScaffoldColumn(false)]
        public int? SupplierId { get; set; }

        [Column("Category ID")]
        [ScaffoldColumn(false)]
        public int? CategoryId { get; set; }

        [Column("Product Name")]
        [MaxLength(40)]
        public string Name { get; set; }

        [Column("English Name")]
        [MaxLength(40)]
        public string EnglishName { get; set; }

        [Column("Quantity Per Unit")]
        [MaxLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column("Unit Price")]
        [DataType("number")]
        public decimal? UnitPrice { get; set; }

        [Column("Units In Stock")]
        [DataType("number")]
        public short? UnitsInStock { get; set; }

        [Column("Units On Order")]
        [DataType("number")]
        public short? UnitsOnOrder { get; set; }

        [Column("Reorder Level")]
        [DataType("number")]
        public short? ReorderLevel { get; set; }

        [DataType("checkbox")]
        public bool Discontinued { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}

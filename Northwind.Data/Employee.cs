using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [Table("Employees")]
    [DisplayColumn("FirstName")]
    public class Employee
    {
        [Key]
        [Column("Employee ID")]
        public int Id { get; set; }

        [Required]
        [Column("Last Name")]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [Column("First Name")]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string Title { get; set; }

        [Column("Birth Date")]
        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }

        [Column("Hire Date")]
        [DataType(DataType.DateTime)]
        public DateTime? HireDate { get; set; }

        public virtual Address Address { get; set; }

        [Column("Home Phone")]
        [MaxLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string HomePhone { get; set; }

        [MaxLength(4)]
        public string Extension { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Photo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Column("Reports To")]
        [ScaffoldColumn(false)]
        public int? ReportsTo { get; set; }

        [ForeignKey("ReportsTo")]
        public Employee Manager { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}

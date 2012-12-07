namespace Northwind.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// TODO: Update Summary
    /// </summary>
    [ComplexType]
    [DisplayColumn("Name")]
    public class Contact
    {
        [Column("Contact Name")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column("Contact Title")]
        [MaxLength(30)]
        public string Title { get; set; }

        public virtual Address Address { get; set; }

        [Column("Phone")]
        [MaxLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Column("Fax")]
        [MaxLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }
    }
}
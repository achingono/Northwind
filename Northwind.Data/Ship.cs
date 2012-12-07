using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Data
{
    [ComplexType]
    public class Ship
    {
        [Column("Ship Name")]
        [MaxLength(40)]
        public string Name { get; set; }
        
        [Column("Ship Address")]
        [MaxLength(60)]
        public string Address { get; set; }
        
        [Column("Ship City")]
        [MaxLength(15)]
        public string City { get; set; }
        
        [Column("Ship Region")]
        [MaxLength(15)]
        public string Region { get; set; }
        
        [Column("Ship Postal Code")]
        [MaxLength(10)]
        public string PostalCode { get; set; }
        
        [Column("Ship Country")]
        [MaxLength(15)]
        public string Country { get; set; }
    }
}

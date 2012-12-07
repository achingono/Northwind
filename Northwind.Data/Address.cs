using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data
{
    [ComplexType]
    [DisplayColumn("Street")]
    public class Address
    {
        [Column("Address")]
        [MaxLength(60)]
        public string Street { get; set; }

        [Column("City")]
        [MaxLength(15)]
        public string City { get; set; }

        [Column("Region")]
        [MaxLength(15)]
        public string Region { get; set; }

        [Column("Postal Code")]
        [MaxLength(60)]
        public string PostalCode { get; set; }

        [Column("Country")]
        [MaxLength(15)]
        public string Country { get; set; }
    }
}

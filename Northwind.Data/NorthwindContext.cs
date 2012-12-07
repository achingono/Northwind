using System.Data.Entity;
using System.Data.Entity.Infrastructure;
//using Northwind.Data.Mapping;

namespace Northwind.Data
{
    public class NorthwindContext : DbContext
    {
        static NorthwindContext()
        {
            Database.SetInitializer<NorthwindContext>(null);
        }

		public NorthwindContext()
			: base("Name=Northwind")
		{
		}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}

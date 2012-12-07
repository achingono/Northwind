namespace Northwind.Web
{ 
    using Models;
	using Northwind.Data;
    using System.Linq;
    using System;

    /// <summary>
    /// TODO: Update summary
    /// </summary>
    public static partial class OrderExtensions
    {
        /// <summary>
        /// Create a new instance of <see cref="OrderModel" /> from a <see cref="Order" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static OrderModel TransformTo<T>(this Order source)
            where T : OrderModel
        {
            // create a new OrderModel
            var target = new OrderModel();

            // update the OrderModel with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }
		
        /// <summary>
        /// Create a new instance of <see cref="Order" /> from a <see cref="OrderModel" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static Order TransformTo<T>(this OrderModel source)
            where T : Order
        {
            // create a new Order
            var target = new Order();

            // update the Order with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }

        /// <summary>
        /// Update <see cref="Order" /> properties with data from <see cref="OrderModel" />
        /// </summary>
        /// <param name="target">The <see cref="Order" /> to be updated</param>
        /// <param name="source">The <see cref="OrderModel" /> with values to be used to update</param>
        public static void UpdateFrom(this Order target, OrderModel source)
        {
            if (source.Ship != null)
            {
		        target.Ship = source.Ship;
            }
            else if (target.Ship == null)
            {
                target.Ship = new Northwind.Data.Ship();
            }
		    target.OrderDate = source.OrderDate;
		    target.RequiredDate = source.RequiredDate;
		    target.ShippedDate = source.ShippedDate;
		    target.Freight = source.Freight;
        }

        /// <summary>
        /// Update <see cref="OrderModel" /> properties with data from <see cref="Order" />
        /// </summary>
        /// <param name="target">The <see cref="OrderModel" /> to be updated</param>
        /// <param name="source">The <see cref="Order" /> with values to be used to update</param>
        public static void UpdateFrom(this OrderModel target, Order source)
        {
		    target.Id = source.Id;
		    target.Name = source.Name;
		    target.Ship = source.Ship;
		    target.OrderDate = source.OrderDate;
		    target.RequiredDate = source.RequiredDate;
		    target.ShippedDate = source.ShippedDate;
		    target.Freight = source.Freight;
			if (source.Customer != null)
			{
				target.CustomerId = source.Customer.Id;
				target.CustomerName = source.Customer.Name;
			}
			if (source.Employee != null)
			{
				target.EmployeeId = source.Employee.Id;
				target.EmployeeFirstName = source.Employee.FirstName;
			}
			if (source.Shipper != null)
			{
				target.ShipperId = source.Shipper.Id;
				target.ShipperName = source.Shipper.Name;
			}
            target.Total = source.Details.Sum(d =>
            {
                var total = Convert.ToDouble(d.UnitPrice * d.Quantity);
                return Convert.ToDecimal(total - (total * d.Discount));
            });
        }
	}
}

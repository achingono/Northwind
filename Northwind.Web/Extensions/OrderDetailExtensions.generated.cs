namespace Northwind.Web
{ 
    using Models;
	using Northwind.Data;
    
    /// <summary>
    /// TODO: Update summary
    /// </summary>
    public static partial class OrderDetailExtensions
    {
        /// <summary>
        /// Create a new instance of <see cref="OrderDetailModel" /> from a <see cref="OrderDetail" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static OrderDetailModel TransformTo<T>(this OrderDetail source)
            where T : OrderDetailModel
        {
            // create a new OrderDetailModel
            var target = new OrderDetailModel();

            // update the OrderDetailModel with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }
		
        /// <summary>
        /// Create a new instance of <see cref="OrderDetail" /> from a <see cref="OrderDetailModel" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static OrderDetail TransformTo<T>(this OrderDetailModel source)
            where T : OrderDetail
        {
            // create a new OrderDetail
            var target = new OrderDetail();

            // update the OrderDetail with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }

        /// <summary>
        /// Update <see cref="OrderDetail" /> properties with data from <see cref="OrderDetailModel" />
        /// </summary>
        /// <param name="target">The <see cref="OrderDetail" /> to be updated</param>
        /// <param name="source">The <see cref="OrderDetailModel" /> with values to be used to update</param>
        public static void UpdateFrom(this OrderDetail target, OrderDetailModel source)
        {
		    target.UnitPrice = source.UnitPrice;
		    target.Quantity = source.Quantity;
		    target.Discount = source.Discount;
        }

        /// <summary>
        /// Update <see cref="OrderDetailModel" /> properties with data from <see cref="OrderDetail" />
        /// </summary>
        /// <param name="target">The <see cref="OrderDetailModel" /> to be updated</param>
        /// <param name="source">The <see cref="OrderDetail" /> with values to be used to update</param>
        public static void UpdateFrom(this OrderDetailModel target, OrderDetail source)
        {
		    target.UnitPrice = source.UnitPrice;
		    target.Quantity = source.Quantity;
		    target.Discount = source.Discount;
			if (source.Order != null)
			{
				target.OrderId = source.Order.Id;
				target.OrderName = source.Order.Name;
			}
			if (source.Product != null)
			{
				target.ProductId = source.Product.Id;
				target.ProductName = source.Product.Name;
			}
        }
	}
}

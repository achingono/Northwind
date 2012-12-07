namespace Northwind.Web
{ 
    using Models;
	using Northwind.Data;
    
    /// <summary>
    /// TODO: Update summary
    /// </summary>
    public static partial class ProductExtensions
    {
        /// <summary>
        /// Create a new instance of <see cref="ProductModel" /> from a <see cref="Product" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static ProductModel TransformTo<T>(this Product source)
            where T : ProductModel
        {
            // create a new ProductModel
            var target = new ProductModel();

            // update the ProductModel with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }
		
        /// <summary>
        /// Create a new instance of <see cref="Product" /> from a <see cref="ProductModel" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static Product TransformTo<T>(this ProductModel source)
            where T : Product
        {
            // create a new Product
            var target = new Product();

            // update the Product with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }

        /// <summary>
        /// Update <see cref="Product" /> properties with data from <see cref="ProductModel" />
        /// </summary>
        /// <param name="target">The <see cref="Product" /> to be updated</param>
        /// <param name="source">The <see cref="ProductModel" /> with values to be used to update</param>
        public static void UpdateFrom(this Product target, ProductModel source)
        {
		    target.Name = source.Name;
		    target.EnglishName = source.EnglishName;
		    target.QuantityPerUnit = source.QuantityPerUnit;
		    target.UnitPrice = source.UnitPrice;
		    target.UnitsInStock = source.UnitsInStock;
		    target.UnitsOnOrder = source.UnitsOnOrder;
		    target.ReorderLevel = source.ReorderLevel;
		    target.Discontinued = source.Discontinued;
        }

        /// <summary>
        /// Update <see cref="ProductModel" /> properties with data from <see cref="Product" />
        /// </summary>
        /// <param name="target">The <see cref="ProductModel" /> to be updated</param>
        /// <param name="source">The <see cref="Product" /> with values to be used to update</param>
        public static void UpdateFrom(this ProductModel target, Product source)
        {
		    target.Id = source.Id;
		    target.Name = source.Name;
		    target.EnglishName = source.EnglishName;
		    target.QuantityPerUnit = source.QuantityPerUnit;
		    target.UnitPrice = source.UnitPrice;
		    target.UnitsInStock = source.UnitsInStock;
		    target.UnitsOnOrder = source.UnitsOnOrder;
		    target.ReorderLevel = source.ReorderLevel;
		    target.Discontinued = source.Discontinued;
			if (source.Supplier != null)
			{
				target.SupplierId = source.Supplier.Id;
				target.SupplierName = source.Supplier.Name;
			}
			if (source.Category != null)
			{
				target.CategoryId = source.Category.Id;
				target.CategoryName = source.Category.Name;
			}
        }
	}
}

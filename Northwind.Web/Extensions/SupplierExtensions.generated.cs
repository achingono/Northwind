namespace Northwind.Web
{ 
    using Models;
	using Northwind.Data;
    
    /// <summary>
    /// TODO: Update summary
    /// </summary>
    public static partial class SupplierExtensions
    {
        /// <summary>
        /// Create a new instance of <see cref="SupplierModel" /> from a <see cref="Supplier" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static SupplierModel TransformTo<T>(this Supplier source)
            where T : SupplierModel
        {
            // create a new SupplierModel
            var target = new SupplierModel();

            // update the SupplierModel with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }
		
        /// <summary>
        /// Create a new instance of <see cref="Supplier" /> from a <see cref="SupplierModel" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static Supplier TransformTo<T>(this SupplierModel source)
            where T : Supplier
        {
            // create a new Supplier
            var target = new Supplier();

            // update the Supplier with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }

        /// <summary>
        /// Update <see cref="Supplier" /> properties with data from <see cref="SupplierModel" />
        /// </summary>
        /// <param name="target">The <see cref="Supplier" /> to be updated</param>
        /// <param name="source">The <see cref="SupplierModel" /> with values to be used to update</param>
        public static void UpdateFrom(this Supplier target, SupplierModel source)
        {
		    target.Name = source.Name;
            if (source.Contact != null)
            {
		        target.Contact = source.Contact;
            }
            else if (target.Contact == null)
            {
                target.Contact = new Northwind.Data.Contact();
            }
        }

        /// <summary>
        /// Update <see cref="SupplierModel" /> properties with data from <see cref="Supplier" />
        /// </summary>
        /// <param name="target">The <see cref="SupplierModel" /> to be updated</param>
        /// <param name="source">The <see cref="Supplier" /> with values to be used to update</param>
        public static void UpdateFrom(this SupplierModel target, Supplier source)
        {
		    target.Id = source.Id;
		    target.Name = source.Name;
		    target.Contact = source.Contact;
        }
	}
}

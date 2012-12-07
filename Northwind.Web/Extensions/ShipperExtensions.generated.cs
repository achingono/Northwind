namespace Northwind.Web
{ 
    using Models;
	using Northwind.Data;
    
    /// <summary>
    /// TODO: Update summary
    /// </summary>
    public static partial class ShipperExtensions
    {
        /// <summary>
        /// Create a new instance of <see cref="ShipperModel" /> from a <see cref="Shipper" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static ShipperModel TransformTo<T>(this Shipper source)
            where T : ShipperModel
        {
            // create a new ShipperModel
            var target = new ShipperModel();

            // update the ShipperModel with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }
		
        /// <summary>
        /// Create a new instance of <see cref="Shipper" /> from a <see cref="ShipperModel" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static Shipper TransformTo<T>(this ShipperModel source)
            where T : Shipper
        {
            // create a new Shipper
            var target = new Shipper();

            // update the Shipper with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }

        /// <summary>
        /// Update <see cref="Shipper" /> properties with data from <see cref="ShipperModel" />
        /// </summary>
        /// <param name="target">The <see cref="Shipper" /> to be updated</param>
        /// <param name="source">The <see cref="ShipperModel" /> with values to be used to update</param>
        public static void UpdateFrom(this Shipper target, ShipperModel source)
        {
		    target.Name = source.Name;
        }

        /// <summary>
        /// Update <see cref="ShipperModel" /> properties with data from <see cref="Shipper" />
        /// </summary>
        /// <param name="target">The <see cref="ShipperModel" /> to be updated</param>
        /// <param name="source">The <see cref="Shipper" /> with values to be used to update</param>
        public static void UpdateFrom(this ShipperModel target, Shipper source)
        {
		    target.Id = source.Id;
		    target.Name = source.Name;
        }
	}
}

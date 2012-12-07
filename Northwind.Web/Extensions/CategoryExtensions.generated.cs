namespace Northwind.Web
{ 
    using Models;
	using Northwind.Data;
    
    /// <summary>
    /// TODO: Update summary
    /// </summary>
    public static partial class CategoryExtensions
    {
        /// <summary>
        /// Create a new instance of <see cref="CategoryModel" /> from a <see cref="Category" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static CategoryModel TransformTo<T>(this Category source)
            where T : CategoryModel
        {
            // create a new CategoryModel
            var target = new CategoryModel();

            // update the CategoryModel with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }
		
        /// <summary>
        /// Create a new instance of <see cref="Category" /> from a <see cref="CategoryModel" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static Category TransformTo<T>(this CategoryModel source)
            where T : Category
        {
            // create a new Category
            var target = new Category();

            // update the Category with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }

        /// <summary>
        /// Update <see cref="Category" /> properties with data from <see cref="CategoryModel" />
        /// </summary>
        /// <param name="target">The <see cref="Category" /> to be updated</param>
        /// <param name="source">The <see cref="CategoryModel" /> with values to be used to update</param>
        public static void UpdateFrom(this Category target, CategoryModel source)
        {
		    target.Name = source.Name;
		    target.Description = source.Description;
		    target.Picture = source.Picture;
        }

        /// <summary>
        /// Update <see cref="CategoryModel" /> properties with data from <see cref="Category" />
        /// </summary>
        /// <param name="target">The <see cref="CategoryModel" /> to be updated</param>
        /// <param name="source">The <see cref="Category" /> with values to be used to update</param>
        public static void UpdateFrom(this CategoryModel target, Category source)
        {
		    target.Id = source.Id;
		    target.Name = source.Name;
		    target.Description = source.Description;
		    target.Picture = source.Picture;
        }
	}
}

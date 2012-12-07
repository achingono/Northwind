namespace Northwind.Web
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;

    /// <summary>
    /// TODO: Update summary
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Create a new instance of T from the source
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static Tout TransformTo<Tin, Tout>(this Tin source)
            where Tin : class
            where Tout : class, new()
        {
            // create a new instance of type T
            var target = new Tout();

            // update the instance with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }

        /// <summary>
        /// Update object properties through reflection
        /// </summary>
        /// <param name="target">The object to be updated</param>
        /// <param name="source">The object with values to be used to update</param>
        /// <example>
        /// <code>
        ///     var po = GetPurchaseOrder(purchaseOrderId);
        ///     var supplier = GetSupplier(supplierId);
        ///     var model = new PurchaseOrderDetailModel();
        ///     
        ///     model.UpdateFrom(po);
        ///     model.UpdateFrom(supplier);
        /// </code>
        /// </example>
        public static void UpdateFrom<T1, T2>(this T1 target, T2 source)
            where T1 : class
            where T2 : class
        {
            // get the corresponding types
            var sourceType = source.GetType();
            var targetType = target.GetType();

            // get all the properties that are writable
            var properties = targetType.GetProperties()
                                       .Where(p => p.CanWrite);

            // this allows case-insensitive searches for propeties/methods
            var bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

            foreach (var targetProperty in properties)
            {
                var targetPropertyName = targetProperty.Name;
                // ensure the source has matching property
                var sourceProperty = sourceType.GetProperty(targetPropertyName, bindingFlags);
                if (sourceProperty != null)
                {
                    // get the value from source
                    object sourceValue = sourceProperty.GetValue(source, null);

                    // set the value on target
                    targetProperty.SetValue(target, sourceValue, bindingFlags, null, null, null);
                }
                else
                {
                    sourceProperty = sourceType.GetProperties()
                                               .Where(p => targetPropertyName.StartsWith(p.Name))
                                               .FirstOrDefault();
                    if (sourceProperty != null)
                    {
                        // get the value from source
                        object sourceValue = sourceProperty.GetValue(source, null);
                        var sourceValuePropertyName = targetPropertyName.Replace(sourceProperty.Name, string.Empty);
                        var sourceValueProperty = sourceValue.GetType().GetProperty(sourceValuePropertyName);

                        if (sourceValueProperty != null)
                        {
                            targetProperty.SetValue(target, sourceValueProperty.GetValue(sourceValue, null),
                                bindingFlags, null, null, null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Verifies that an object is accessible to the specified user
        /// </summary>
        /// <typeparam name="T">The type of object to be verified</typeparam>
        /// <param name="item">The item to be verified</param>
        /// <param name="user">The user to verify against</param>
        /// <returns>True if user has access to the item</returns>
        public static bool IsAccessibleTo<T>(this T item, IPrincipal user)
            where T : class
        {
            return true;
        }

        /// <summary>
        /// Filters items accessible to the specified user
        /// </summary>
        /// <typeparam name="T">The type of object to be verified</typeparam>
        /// <param name="set">The set of objects to filter</param>
        /// <param name="user">The user to verify against</param>
        /// <returns>An <see cref="IEnumerable<T>"/> that contains elements accessible to the specified user</returns>
        public static IEnumerable<T> AccessibleTo<T>(this IEnumerable<T> set, IPrincipal user)
            where T : class
        {
            return set.Where(x => x.IsAccessibleTo(user));
        }

        /// <summary>
        /// Determines if the specified user can create an object of type T
        /// </summary>
        /// <typeparam name="T">The type of object to be verified</typeparam>
        /// <param name="user">The user to verify against</param>
        /// <returns>True if user is allowed to create objects of type T</returns>
        public static bool CanCreate<T>(this IPrincipal user)
            where T : class
        {
            return true;
        }

        /// <summary>
        /// Determines if the specified user can update the specified entity
        /// </summary>
        /// <typeparam name="T">The type of object to be verified</typeparam>
        /// <param name="user">The user to verify against</param>
        /// <param name="entity">The entity to be validated</param>
        /// <returns>True if user is allowed to update the object of type T</returns>
        public static bool CanUpdate<T>(this IPrincipal user, T entity)
            where T : class
        {
            return true;
        }

        /// <summary>
        /// Determines if the specified user can delete the specified entity
        /// </summary>
        /// <typeparam name="T">The type of object to be verified</typeparam>
        /// <param name="user">The user to verify against</param>
        /// <param name="entity">The entity to be validated</param>
        /// <returns>True if user is allowed to delete the object of type T</returns>
        public static bool CanDelete<T>(this IPrincipal user, T entity)
            where T : class
        {
            return true;
        }
    }
}

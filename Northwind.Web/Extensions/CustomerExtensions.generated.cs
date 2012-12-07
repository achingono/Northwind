namespace Northwind.Web
{ 
    using Models;
	using Northwind.Data;
    using System;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary
    /// </summary>
    public static partial class CustomerExtensions
    {
        /// <summary>
        /// Create a new instance of <see cref="CustomerModel" /> from a <see cref="Customer" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static CustomerModel TransformTo<T>(this Customer source)
            where T : CustomerModel
        {
            // create a new CustomerModel
            var target = new CustomerModel();

            // update the CustomerModel with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }
		
        /// <summary>
        /// Create a new instance of <see cref="Customer" /> from a <see cref="CustomerModel" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static Customer TransformTo<T>(this CustomerModel source)
            where T : Customer
        {
            // create a new Customer
            var target = new Customer();

            // update the Customer with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }

        /// <summary>
        /// Update <see cref="Customer" /> properties with data from <see cref="CustomerModel" />
        /// </summary>
        /// <param name="target">The <see cref="Customer" /> to be updated</param>
        /// <param name="source">The <see cref="CustomerModel" /> with values to be used to update</param>
        public static void UpdateFrom(this Customer target, CustomerModel source)
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
        /// Update <see cref="CustomerModel" /> properties with data from <see cref="Customer" />
        /// </summary>
        /// <param name="target">The <see cref="CustomerModel" /> to be updated</param>
        /// <param name="source">The <see cref="Customer" /> with values to be used to update</param>
        public static void UpdateFrom(this CustomerModel target, Customer source)
        {
		    target.Id = source.Id;
		    target.Name = source.Name;
		    target.Contact = source.Contact;
        }

        /// <summary>
        /// A VERY ROUGH approach to generating unique customer Ids
        /// </summary>
        /// <param name="model">The model with details to generate new Id</param>
        /// <returns></returns>
        public static string GenerateId(this CustomerModel model)
        {
            var context = new NorthwindContext();
            string generatedId = String.Join("", model.Name.Split(' ')
                                                      .Select(t => t.Substring(0, t.Length > 3 ? 3 : t.Length)))
                                       .ToUpperInvariant();
            var customerId = generatedId.Substring(0, generatedId.Length > 4 ? 4 : generatedId.Length);
            int i = 4;

            while (i - 4 < 10)
            {
                var tempId = string.Empty;

                if (generatedId.Length > i - 1)
                {
                    tempId = customerId + generatedId[i];
                }
                else
                {
                    tempId = customerId + (i - 4).ToString();
                }

                if (!context.Customers.Any(c => c.Id.Equals(tempId)))
                {
                    customerId = tempId;
                    break;
                }
            }
            return customerId;
        }

	}
}

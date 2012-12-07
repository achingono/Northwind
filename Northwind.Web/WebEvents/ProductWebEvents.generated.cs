namespace Northwind.Web.WebEvents
{ 
    using System.Web.Management;
	using Northwind.Data;
	
	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Product"/> is created.
	/// </summary>
    public class ProductCreatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ProductCreatedEvent" /> class
		/// </summary>
		/// <param name="product">The <see cref="Product" /> which the event relates to</param>
        public ProductCreatedEvent(Product product)
            : base(string.Format("Product: '{0}' was created.", product.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Product"/> is updated.
	/// </summary>
    public class ProductUpdatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ProductUpdatedEvent" /> class
		/// </summary>
		/// <param name="product">The <see cref="Product" /> which the event relates to</param>
        public ProductUpdatedEvent(Product product)
            : base(string.Format("Product: '{0}' was updated.", product.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Product"/> is deleted.
	/// </summary>
    public class ProductDeletedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ProductDeletedEvent" /> class
		/// </summary>
		/// <param name="product">The <see cref="Product" /> which the event relates to</param>
        public ProductDeletedEvent(Product product)
            : base(string.Format("Product: '{0}' was deleted.", product.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }
}
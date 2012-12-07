namespace Northwind.Web.WebEvents
{ 
    using System.Web.Management;
	using Northwind.Data;
	
	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Order"/> is created.
	/// </summary>
    public class OrderCreatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="OrderCreatedEvent" /> class
		/// </summary>
		/// <param name="order">The <see cref="Order" /> which the event relates to</param>
        public OrderCreatedEvent(Order order)
            : base(string.Format("Order: '{0}' was created.", order.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Order"/> is updated.
	/// </summary>
    public class OrderUpdatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="OrderUpdatedEvent" /> class
		/// </summary>
		/// <param name="order">The <see cref="Order" /> which the event relates to</param>
        public OrderUpdatedEvent(Order order)
            : base(string.Format("Order: '{0}' was updated.", order.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Order"/> is deleted.
	/// </summary>
    public class OrderDeletedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="OrderDeletedEvent" /> class
		/// </summary>
		/// <param name="order">The <see cref="Order" /> which the event relates to</param>
        public OrderDeletedEvent(Order order)
            : base(string.Format("Order: '{0}' was deleted.", order.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }
}
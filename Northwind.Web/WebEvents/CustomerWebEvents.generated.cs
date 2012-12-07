namespace Northwind.Web.WebEvents
{ 
    using System.Web.Management;
	using Northwind.Data;
	
	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Customer"/> is created.
	/// </summary>
    public class CustomerCreatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerCreatedEvent" /> class
		/// </summary>
		/// <param name="customer">The <see cref="Customer" /> which the event relates to</param>
        public CustomerCreatedEvent(Customer customer)
            : base(string.Format("Customer: '{0}' was created.", customer.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Customer"/> is updated.
	/// </summary>
    public class CustomerUpdatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerUpdatedEvent" /> class
		/// </summary>
		/// <param name="customer">The <see cref="Customer" /> which the event relates to</param>
        public CustomerUpdatedEvent(Customer customer)
            : base(string.Format("Customer: '{0}' was updated.", customer.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Customer"/> is deleted.
	/// </summary>
    public class CustomerDeletedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerDeletedEvent" /> class
		/// </summary>
		/// <param name="customer">The <see cref="Customer" /> which the event relates to</param>
        public CustomerDeletedEvent(Customer customer)
            : base(string.Format("Customer: '{0}' was deleted.", customer.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }
}
namespace Northwind.Web.WebEvents
{ 
    using System.Web.Management;
	using Northwind.Data;
	
	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Supplier"/> is created.
	/// </summary>
    public class SupplierCreatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="SupplierCreatedEvent" /> class
		/// </summary>
		/// <param name="supplier">The <see cref="Supplier" /> which the event relates to</param>
        public SupplierCreatedEvent(Supplier supplier)
            : base(string.Format("Supplier: '{0}' was created.", supplier.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Supplier"/> is updated.
	/// </summary>
    public class SupplierUpdatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="SupplierUpdatedEvent" /> class
		/// </summary>
		/// <param name="supplier">The <see cref="Supplier" /> which the event relates to</param>
        public SupplierUpdatedEvent(Supplier supplier)
            : base(string.Format("Supplier: '{0}' was updated.", supplier.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Supplier"/> is deleted.
	/// </summary>
    public class SupplierDeletedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="SupplierDeletedEvent" /> class
		/// </summary>
		/// <param name="supplier">The <see cref="Supplier" /> which the event relates to</param>
        public SupplierDeletedEvent(Supplier supplier)
            : base(string.Format("Supplier: '{0}' was deleted.", supplier.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }
}
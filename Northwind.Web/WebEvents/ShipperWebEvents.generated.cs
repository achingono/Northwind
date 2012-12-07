namespace Northwind.Web.WebEvents
{ 
    using System.Web.Management;
	using Northwind.Data;
	
	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Shipper"/> is created.
	/// </summary>
    public class ShipperCreatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ShipperCreatedEvent" /> class
		/// </summary>
		/// <param name="shipper">The <see cref="Shipper" /> which the event relates to</param>
        public ShipperCreatedEvent(Shipper shipper)
            : base(string.Format("Shipper: '{0}' was created.", shipper.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Shipper"/> is updated.
	/// </summary>
    public class ShipperUpdatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ShipperUpdatedEvent" /> class
		/// </summary>
		/// <param name="shipper">The <see cref="Shipper" /> which the event relates to</param>
        public ShipperUpdatedEvent(Shipper shipper)
            : base(string.Format("Shipper: '{0}' was updated.", shipper.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Shipper"/> is deleted.
	/// </summary>
    public class ShipperDeletedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ShipperDeletedEvent" /> class
		/// </summary>
		/// <param name="shipper">The <see cref="Shipper" /> which the event relates to</param>
        public ShipperDeletedEvent(Shipper shipper)
            : base(string.Format("Shipper: '{0}' was deleted.", shipper.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }
}
namespace Northwind.Web.WebEvents
{ 
    using System.Web.Management;
	using Northwind.Data;
	
	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Employee"/> is created.
	/// </summary>
    public class EmployeeCreatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="EmployeeCreatedEvent" /> class
		/// </summary>
		/// <param name="employee">The <see cref="Employee" /> which the event relates to</param>
        public EmployeeCreatedEvent(Employee employee)
            : base(string.Format("Employee: '{0}' was created.", employee.FirstName), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Employee"/> is updated.
	/// </summary>
    public class EmployeeUpdatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="EmployeeUpdatedEvent" /> class
		/// </summary>
		/// <param name="employee">The <see cref="Employee" /> which the event relates to</param>
        public EmployeeUpdatedEvent(Employee employee)
            : base(string.Format("Employee: '{0}' was updated.", employee.FirstName), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Employee"/> is deleted.
	/// </summary>
    public class EmployeeDeletedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="EmployeeDeletedEvent" /> class
		/// </summary>
		/// <param name="employee">The <see cref="Employee" /> which the event relates to</param>
        public EmployeeDeletedEvent(Employee employee)
            : base(string.Format("Employee: '{0}' was deleted.", employee.FirstName), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }
}
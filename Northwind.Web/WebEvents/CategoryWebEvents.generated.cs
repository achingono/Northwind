namespace Northwind.Web.WebEvents
{ 
    using System.Web.Management;
	using Northwind.Data;
	
	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Category"/> is created.
	/// </summary>
    public class CategoryCreatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryCreatedEvent" /> class
		/// </summary>
		/// <param name="category">The <see cref="Category" /> which the event relates to</param>
        public CategoryCreatedEvent(Category category)
            : base(string.Format("Category: '{0}' was created.", category.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Category"/> is updated.
	/// </summary>
    public class CategoryUpdatedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryUpdatedEvent" /> class
		/// </summary>
		/// <param name="category">The <see cref="Category" /> which the event relates to</param>
        public CategoryUpdatedEvent(Category category)
            : base(string.Format("Category: '{0}' was updated.", category.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

	/// <summary>
	/// Defines the ASP.NET health-monitoring event that is raised when a new <see cref="Category"/> is deleted.
	/// </summary>
    public class CategoryDeletedEvent : WebBaseEvent
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryDeletedEvent" /> class
		/// </summary>
		/// <param name="category">The <see cref="Category" /> which the event relates to</param>
        public CategoryDeletedEvent(Category category)
            : base(string.Format("Category: '{0}' was deleted.", category.Name), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }
}
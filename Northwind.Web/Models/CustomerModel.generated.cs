namespace Northwind.Web.Models
{ 
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
    using System.ComponentModel.DataAnnotations;


	/// <summary>
	/// The model class corresponding to a <see cref="Customer" /> entity.
	/// </summary>
    public partial class CustomerModel
    {
		/// <summary>
		/// No metadata information available
		/// </summary>
		public string Id { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		public Northwind.Data.Contact Contact { get; set; }
	}
}
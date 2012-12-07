namespace Northwind.Web.Models
{ 
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
    using System.ComponentModel.DataAnnotations;


	/// <summary>
	/// The model class corresponding to a <see cref="Supplier" /> entity.
	/// </summary>
    public partial class SupplierModel
    {
		/// <summary>
		/// No metadata information available
		/// </summary>
		public int Id { get; set; }
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
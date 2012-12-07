namespace Northwind.Web.Models
{ 
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
    using System.ComponentModel.DataAnnotations;


	/// <summary>
	/// The model class corresponding to a <see cref="Order" /> entity.
	/// </summary>
    public partial class OrderModel
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
		public Northwind.Data.Ship Ship { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType(DataType.DateTime)]
		public System.DateTime? OrderDate { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType(DataType.DateTime)]
		public System.DateTime? RequiredDate { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType(DataType.DateTime)]
		public System.DateTime? ShippedDate { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("number")]
		public decimal? Freight { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
        [DataType("hidden")]
		public string CustomerId { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("typeahead")]
        [Display(Name ="Customer")]
		[UIHint("Typeahead", null, "HiddenField", "CustomerId", "Controller", "Customers")]
		public string CustomerName { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
        [DataType("hidden")]
		public int EmployeeId { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("typeahead")]
        [Display(Name ="Employee")]
		[UIHint("Typeahead", null, "HiddenField", "EmployeeId", "Controller", "Employees")]
		public string EmployeeFirstName { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
        [DataType("hidden")]
		public int ShipperId { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("typeahead")]
        [Display(Name ="Shipper")]
		[UIHint("Typeahead", null, "HiddenField", "ShipperId", "Controller", "Shippers")]
		public string ShipperName { get; set; }
	}
}
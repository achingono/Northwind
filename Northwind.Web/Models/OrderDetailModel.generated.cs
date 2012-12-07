namespace Northwind.Web.Models
{ 
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
    using System.ComponentModel.DataAnnotations;


	/// <summary>
	/// The model class corresponding to a <see cref="OrderDetail" /> entity.
	/// </summary>
    public partial class OrderDetailModel
    {
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("number")]
		public decimal UnitPrice { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("number")]
		public short Quantity { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("number")]
		public float Discount { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
        [DataType("hidden")]
		public int OrderId { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("typeahead")]
        [Display(Name ="Order")]
		[UIHint("Typeahead", null, "HiddenField", "OrderId", "Controller", "Orders")]
		public string OrderName { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
        [DataType("hidden")]
		public int ProductId { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("typeahead")]
        [Display(Name ="Product")]
		[UIHint("Typeahead", null, "HiddenField", "ProductId", "Controller", "Products")]
		public string ProductName { get; set; }
	}
}
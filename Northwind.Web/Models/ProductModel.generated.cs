namespace Northwind.Web.Models
{ 
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
    using System.ComponentModel.DataAnnotations;


	/// <summary>
	/// The model class corresponding to a <see cref="Product" /> entity.
	/// </summary>
    public partial class ProductModel
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
		public string EnglishName { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		public string QuantityPerUnit { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("number")]
		public decimal? UnitPrice { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("number")]
		public short? UnitsInStock { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("number")]
		public short? UnitsOnOrder { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("number")]
		public short? ReorderLevel { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("checkbox")]
		public bool Discontinued { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
        [DataType("hidden")]
		public int SupplierId { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("typeahead")]
        [Display(Name ="Supplier")]
		[UIHint("Typeahead", null, "HiddenField", "SupplierId", "Controller", "Suppliers")]
		public string SupplierName { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
        [DataType("hidden")]
		public int CategoryId { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("typeahead")]
        [Display(Name ="Category")]
		[UIHint("Typeahead", null, "HiddenField", "CategoryId", "Controller", "Categories")]
		public string CategoryName { get; set; }
	}
}
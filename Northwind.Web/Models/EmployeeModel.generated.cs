namespace Northwind.Web.Models
{ 
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
    using System.ComponentModel.DataAnnotations;


	/// <summary>
	/// The model class corresponding to a <see cref="Employee" /> entity.
	/// </summary>
    public partial class EmployeeModel
    {
		/// <summary>
		/// No metadata information available
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		public string LastName { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		public string FirstName { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType(DataType.DateTime)]
		public System.DateTime? BirthDate { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType(DataType.DateTime)]
		public System.DateTime? HireDate { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		public Northwind.Data.Address Address { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType(DataType.PhoneNumber)]
		public string HomePhone { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		public string Extension { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType(DataType.MultilineText)]
		public string Notes { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
        [DataType("hidden")]
		public int ManagerId { get; set; }
		/// <summary>
		/// No metadata information available
		/// </summary>
		[DataType("typeahead")]
        [Display(Name ="Manager")]
		[UIHint("Typeahead", null, "HiddenField", "ManagerId", "Controller", "Employees")]
		public string ManagerFirstName { get; set; }
	}
}
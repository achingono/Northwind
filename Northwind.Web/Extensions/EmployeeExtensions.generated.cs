namespace Northwind.Web
{ 
    using Models;
	using Northwind.Data;
    
    /// <summary>
    /// TODO: Update summary
    /// </summary>
    public static partial class EmployeeExtensions
    {
        /// <summary>
        /// Create a new instance of <see cref="EmployeeModel" /> from a <see cref="Employee" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static EmployeeModel TransformTo<T>(this Employee source)
            where T : EmployeeModel
        {
            // create a new EmployeeModel
            var target = new EmployeeModel();

            // update the EmployeeModel with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }
		
        /// <summary>
        /// Create a new instance of <see cref="Employee" /> from a <see cref="EmployeeModel" />
        /// </summary>
        /// <typeparam name="T">The type of instance to create</typeparam>
        /// <param name="source">The object to be transformed</param>
        /// <returns></returns>
        public static Employee TransformTo<T>(this EmployeeModel source)
            where T : Employee
        {
            // create a new Employee
            var target = new Employee();

            // update the Employee with source
            target.UpdateFrom(source);

            // return updated target
            return target;
        }

        /// <summary>
        /// Update <see cref="Employee" /> properties with data from <see cref="EmployeeModel" />
        /// </summary>
        /// <param name="target">The <see cref="Employee" /> to be updated</param>
        /// <param name="source">The <see cref="EmployeeModel" /> with values to be used to update</param>
        public static void UpdateFrom(this Employee target, EmployeeModel source)
        {
		    target.LastName = source.LastName;
		    target.FirstName = source.FirstName;
		    target.Title = source.Title;
		    target.BirthDate = source.BirthDate;
		    target.HireDate = source.HireDate;
            if (source.Address != null)
            {
		        target.Address = source.Address;
            }
            else if (target.Address == null)
            {
                target.Address = new Northwind.Data.Address();
            }
		    target.HomePhone = source.HomePhone;
		    target.Extension = source.Extension;
		    //target.Photo = source.Photo;
		    target.Notes = source.Notes;
        }

        /// <summary>
        /// Update <see cref="EmployeeModel" /> properties with data from <see cref="Employee" />
        /// </summary>
        /// <param name="target">The <see cref="EmployeeModel" /> to be updated</param>
        /// <param name="source">The <see cref="Employee" /> with values to be used to update</param>
        public static void UpdateFrom(this EmployeeModel target, Employee source)
        {
		    target.Id = source.Id;
		    target.LastName = source.LastName;
		    target.FirstName = source.FirstName;
		    target.Title = source.Title;
		    target.BirthDate = source.BirthDate;
		    target.HireDate = source.HireDate;
		    target.Address = source.Address;
		    target.HomePhone = source.HomePhone;
		    target.Extension = source.Extension;
            target.PhotoUrl = Url.ApiUrl("Employees", "Photo", source.Id);
		    target.Notes = source.Notes;
			if (source.Manager != null)
			{
				target.ManagerId = source.Manager.Id;
				target.ManagerFirstName = source.Manager.FirstName;
			}
        }
	}
}

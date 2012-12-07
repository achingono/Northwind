namespace Northwind.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class EmployeeModel
    {
        [DataType(DataType.ImageUrl)]
        [Editable(false)]
        public string PhotoUrl { get; set; }

        public EmployeeModel()
        {
            this.Address = new Data.Address();
        }
    }
}

namespace Northwind.Web.Models
{
    public partial class CustomerModel
    {
        public CustomerModel()
        {
            this.Contact = new Data.Contact();
            if (this.Contact.Address == null)
            {
                this.Contact.Address = new Data.Address();
            }
        }
    }
}

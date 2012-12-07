namespace Northwind.Data.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Generates a unique Id
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CodeGeneratedAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Order)
            {
                var order = value as Order;
                if (order.Id <= 0)
                {
                    order.Id = new NorthwindContext().Orders.Max(o => o.Id) + 1;
                }
            }
            else
            {
                throw new InvalidOperationException(
                    "Validation attribute only applies to objects of type 'Order'");
            }
            return ValidationResult.Success;
        }
    }
}

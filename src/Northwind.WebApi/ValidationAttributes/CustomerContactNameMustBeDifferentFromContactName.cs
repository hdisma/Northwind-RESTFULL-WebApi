using Northwind.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.WebApi.ValidationAttributes
{
    public class CustomerContactNameMustBeDifferentFromContactName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (CustomerForCreationDto)validationContext.ObjectInstance;

            if (customer.ContactName == customer.ContactTitle) return new ValidationResult(
                ErrorMessage,
                new[] { nameof(CustomerForCreationDto) });

            return ValidationResult.Success;
        }
    }
}

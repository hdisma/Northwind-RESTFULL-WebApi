using Northwind.WebApi.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.WebApi.Models
{
    [CustomerContactNameMustBeDifferentFromContactName(
        ErrorMessage = "The provided contactTitle should be different from the title.")]
    public class CustomerForCreationDto //: IValidatableObject
    {
        public string CustomerID { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public ICollection<OrderForCreationDto> Orders { get; set; } = new List<OrderForCreationDto>();

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (ContactName == ContactTitle) yield return new ValidationResult(
        //        "The provided contactTitle should be different from the title.",
        //        new[] { "CustomerForCreationDto" });
        //}
    }
}

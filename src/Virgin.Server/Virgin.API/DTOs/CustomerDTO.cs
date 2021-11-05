using System.ComponentModel.DataAnnotations;
using Virgin.API.Helpers.Attributes;
using Virgin.Core.Models;

namespace Virgin.API.DTOs
{
    public class CustomerDTO
    {
        [Email]
        [Required]
        public string Email { get; set; }

        [Required]
        public string GivenName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        [RegularExpression(Regex.OnlyDigits)]
        public string PostalCode { get; set; }

        internal static Customer ToDomain(CustomerDTO customer)
            => new()
            {
                Email = customer.Email,
                GivenName = customer.GivenName,
                Surname = customer.Surname,
                City = customer.City,
                StreetAddress = customer.StreetAddress,
                PostalCode = customer.PostalCode
            };
    }
}
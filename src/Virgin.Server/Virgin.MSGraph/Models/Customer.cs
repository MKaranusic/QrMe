using Microsoft.Graph;
using System;
using System.Linq.Expressions;

namespace Virgin.MSGraph.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string DisplayName { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }

        internal static Expression<Func<User, Customer>> Select = x =>
         new Customer
         {
             Id = x.Id,
             GivenName = x.GivenName,
             Surname = x.Surname,
             DisplayName = x.DisplayName,
             City = x.City,
             Email = x.Mail,
             PostalCode = x.PostalCode,
             StreetAddress = x.StreetAddress
         };

        internal static Customer ToModel(User user)
            => new()
            {
                Id = user.Id,
                Email = user.Mail,
                GivenName = user.GivenName,
                Surname = user.Surname,
                DisplayName = user.DisplayName,
                City = user.City,
                StreetAddress = user.StreetAddress,
                PostalCode = user.PostalCode
            };
    }
}
using Graph = Virgin.MSGraph.Models;

namespace Virgin.Core.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }

        internal static Graph.Customer ToGraph(Customer customer)
            => new()
            {
                Id = customer.Id,
                Email = customer.Email,
                GivenName = customer.GivenName,
                Surname = customer.Surname,
                DisplayName = customer.GivenName + " " + customer.Surname,
                City = customer.City,
                StreetAddress = customer.StreetAddress,
                PostalCode = customer.PostalCode
            };

        internal static Customer ToModel(Graph.Customer customer)
            => new()
            {
                Id = customer.Id,
                Email = customer.Email,
                GivenName = customer.GivenName,
                Surname = customer.Surname,
                City = customer.City,
                StreetAddress = customer.StreetAddress,
                PostalCode = customer.PostalCode
            };
    }
}
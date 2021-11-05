using Graph = Virgin.MSGraph.Models;

namespace Virgin.Core.Models
{
    public class QRDetails
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string QRAddress { get; set; }
        public Customer Customer { get; set; }

        internal static QRDetails ToModel(Graph.QRDetails details)
            => new()
            {
                UserName = details.UserName,
                Password = details.Password,
                Customer = Customer.ToModel(details.Customer),
            };
    }
}
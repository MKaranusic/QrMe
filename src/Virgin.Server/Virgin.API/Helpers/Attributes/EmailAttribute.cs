using System.ComponentModel.DataAnnotations;

namespace Virgin.API.Helpers.Attributes
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute() : base(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
        {
        }
    }
}
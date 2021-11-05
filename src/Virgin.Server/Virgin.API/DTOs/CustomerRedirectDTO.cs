using System.ComponentModel.DataAnnotations;
using Virgin.Core.Models;

namespace Virgin.API.DTOs
{
    public class CustomerRedirectDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(75)]
        public string Name { get; set; }
        public string CustomerId { get; set; }
        [Required]
        public string TargetUrl { get; set; }
        public bool IsActive { get; set; }
        public int TimesViewed { get; set; }

        internal static CustomerRedirect ToDomain(CustomerRedirectDTO customerRedirect)
            => new()
            {
                Id = customerRedirect.Id,
                Name = customerRedirect.Name,
                TargetUrl = customerRedirect.TargetUrl,
                CustomerId = customerRedirect.CustomerId,
                IsActive = customerRedirect.IsActive,
                TimesViewed = customerRedirect.TimesViewed
            };
    }
}
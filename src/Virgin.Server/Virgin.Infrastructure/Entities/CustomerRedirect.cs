using System.ComponentModel.DataAnnotations;
using Virgin.GenericRepository.Models;

namespace Virgin.Infrastructure.Entities
{
    public class CustomerRedirect : EntityBase<int>
    {
        public override int Id { get; set; }
        [Required]
        [StringLength(75)]
        public string Name { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string TargetUrl { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int TimesViewed { get; set; }
    }
}
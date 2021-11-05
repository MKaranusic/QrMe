using System;
using System.Linq.Expressions;
using Entity = Virgin.Infrastructure.Entities;

namespace Virgin.Core.Models
{
    public class CustomerRedirect
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerId { get; set; }
        public string TargetUrl { get; set; }
        public bool IsActive { get; set; }
        public int TimesViewed { get; set; }

        internal static Entity.CustomerRedirect ToEntity(CustomerRedirect customerRedirect)
            => new()
            {
                Id = customerRedirect.Id,
                Name = customerRedirect.Name,
                IsActive = customerRedirect.IsActive,
                TargetUrl = customerRedirect.TargetUrl,
                CustomerId = customerRedirect.CustomerId,
                TimesViewed = customerRedirect.TimesViewed
            };

        internal static Expression<Func<Entity.CustomerRedirect, CustomerRedirect>> Select => x =>
            new CustomerRedirect()
            {
                Id = x.Id,
                Name = x.Name,
                CustomerId = x.CustomerId,
                IsActive = x.IsActive,
                TargetUrl = x.TargetUrl,
                TimesViewed = x.TimesViewed
            };
    }
}
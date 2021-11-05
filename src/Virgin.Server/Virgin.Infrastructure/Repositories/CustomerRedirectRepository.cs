using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;
using System.Threading.Tasks;
using Virgin.GenericRepository.DAL;
using Virgin.GenericRepository.DAL.Interfaces;
using Virgin.Infrastructure.Entities;
using Virgin.Infrastructure.Repositories.Interfaces;

namespace Virgin.Infrastructure.Repositories
{
    public class CustomerRedirectRepository : EntityGenericRepository<CustomerRedirect, int>, ICustomerRedirectRepository
    {
        public CustomerRedirectRepository(IAppDbContext context) : base(context)
        {
        }

        public async Task DeactivateCustomerUrlsAsync(string customerId, int id = 0)
        {
            var activeCustomerRedirects = await DbSet.Where(x => x.CustomerId == customerId && x.IsActive == true).ToListAsync();

            if (activeCustomerRedirects.Count > 1)
                Log.Error("Customer with ID[{customerId}] has more than one active redirect!, activeCustomerRedirect Ids: {activeCustomerRedirects}", customerId, activeCustomerRedirects.Select(x => x.Id));

            foreach (var customerRedirect in activeCustomerRedirects)
            {
                if (customerRedirect.Id == id)
                    continue;

                customerRedirect.IsActive = false;
                UpdateBase(customerRedirect);
            }
        }

        public async Task<string> GetActiveCustomerUrlAsync(string customerId)
        {
            var activeCustomerRedirect = await DbSet.SingleOrDefaultAsync(x => x.CustomerId == customerId && x.IsActive == true && !x.IsDeleted).ConfigureAwait(false);
            return activeCustomerRedirect?.TargetUrl;
        }

        public int UpdateBase(CustomerRedirect entity)
        {
            if (IsDetached(entity))
                DbSet.Attach(entity);

            _context.SetEntityStateModified(entity);

            return entity.Id;
        }
    }
}
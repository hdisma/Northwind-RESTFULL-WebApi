using Northwind.Core.Entities.Northwind;
using Northwind.Core.Interfaces;
using Northwind.Core.ResourceParameters;
using Northwind.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> AddAsync(Customer entity)
        {
            return await _customerRepository.AddAsync(entity).ConfigureAwait(true);
        }

        public async Task<IReadOnlyList<Customer>> AddRangeAsync(IReadOnlyList<Customer> entityCollection)
        {
            return await _customerRepository.AddRangeAsync(entityCollection).ConfigureAwait(true);
        }

        public async Task<int> CountAsync()
        {
            var customerCountSpec = new CustomersCountSpecification();
            var count = await _customerRepository.CountAsync(customerCountSpec).ConfigureAwait(true);

            return count;
        }

        public async Task DeleteAsync(Customer entity)
        {
            await _customerRepository.DeleteAsync(entity).ConfigureAwait(true);
        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync().ConfigureAwait(true);
        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync(CustomerResourceParameters customerResourceParameters)
        {
            if (customerResourceParameters == null)
                throw new ArgumentNullException(nameof(customerResourceParameters));

            IQueryable<Customer> collection = (await _customerRepository.GetAllAsync().ConfigureAwait(true)).AsQueryable();

            if (string.IsNullOrEmpty(customerResourceParameters.CompanyNameFilter)
                && string.IsNullOrWhiteSpace(customerResourceParameters.CompanyNameFilter)
                && string.IsNullOrWhiteSpace(customerResourceParameters.SearchQuery)
                && string.IsNullOrEmpty(customerResourceParameters.SearchQuery))
                return collection.ToList();

            if (!string.IsNullOrEmpty(customerResourceParameters.CompanyNameFilter))
                collection = collection.Where(q => q.CompanyName.Contains(customerResourceParameters.CompanyNameFilter.Trim()));

            if (!string.IsNullOrEmpty(customerResourceParameters.SearchQuery))
                collection = collection.Where(q => q.CompanyName.Contains(customerResourceParameters.SearchQuery)
                                                   || q.Address.Contains(customerResourceParameters.SearchQuery)
                                                   || q.ContactName.Contains(customerResourceParameters.SearchQuery)
                                                   || q.Phone.Contains(customerResourceParameters.SearchQuery));

            return collection.ToList();
        }

        public Task<IReadOnlyList<Customer>> GetAsync(ISpecification<Customer> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id).ConfigureAwait(true);
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _customerRepository.GetByIdAsync(id).ConfigureAwait(true);
        }

        public async Task<IReadOnlyList<Customer>> GetByIdsAsync(IReadOnlyList<int> ids)
        {
            return await _customerRepository.GetByIdsAsync(ids).ConfigureAwait(true);
        }

        public async Task<IReadOnlyList<Customer>> GetByIdsAsync(IReadOnlyList<string> ids)
        {
            return await _customerRepository.GetByIdsAsync(ids).ConfigureAwait(true);
        }

        public async Task<bool> Exists(Customer entity)
        {
            return await _customerRepository.Exists(entity).ConfigureAwait(true);
        }

        public async Task UpdateAsync(Customer entity)
        {
            await _customerRepository.UpdateAsync(entity).ConfigureAwait(true);
        }
    }
}

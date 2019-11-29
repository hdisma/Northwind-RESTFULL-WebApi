using Northwind.Core.Entities.Northwind;
using Northwind.Core.Interfaces;
using Northwind.Core.Specification;
using System;
using System.Collections.Generic;
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

        public Task<IReadOnlyList<Customer>> GetAsync(ISpecification<Customer> spec)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _customerRepository.GetByIdAsync(id).ConfigureAwait(true);
        }

        public Task UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}

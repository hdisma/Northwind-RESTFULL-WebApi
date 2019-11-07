using Northwind.Core.Entities.Northwind;
using Northwind.Core.Interfaces;
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

        public async Task<Customer> GetCustomerById(string id)
        {
            return await _customerRepository.GetByIdAsync(id).ConfigureAwait(true);
        }
    }
}

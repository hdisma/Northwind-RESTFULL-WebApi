using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Core.Entities.Northwind;
using Northwind.Core.Interfaces;
using Northwind.Core.Specification;

namespace Northwind.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        
        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository
                               ?? throw new ArgumentNullException(nameof(orderRepository));
            _customerRepository = customerRepository
                                  ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<Order> AddAsync(Order entity)
        {
            return await _orderRepository.AddAsync(entity).ConfigureAwait(true);
        }

        public async Task<int> CountAsync()
        {
            var orderCountSpec = new OrderCountSpecification();
            return await _orderRepository.CountAsync(orderCountSpec).ConfigureAwait(true);
        }

        public async Task DeleteAsync(Order entity)
        {
            await _orderRepository.DeleteAsync(entity).ConfigureAwait(true);
        }

        public async Task<bool> Exists(Order entity)
        {
            return await _orderRepository.Exists(entity).ConfigureAwait(true);
        }

        public async Task<bool> CustomerExists(string customerId)
        {
            return await _customerRepository.Exists(customerId).ConfigureAwait(true);
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync().ConfigureAwait(true);
        }

        public Task<IReadOnlyList<Order>> GetAsync(ISpecification<Order> spec)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id).ConfigureAwait(true);
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            return await _orderRepository.GetByIdAsync(id).ConfigureAwait(true);
        }

        public async Task<IReadOnlyCollection<Order>> GetByCustomerId(string customerId)
        {
            var orderForCustomerSpecification = new OrderForCustomerSpecification(x => x.CustomerID == customerId);

            return await _orderRepository.GetAsync(orderForCustomerSpecification).ConfigureAwait(true);
        }

        public async Task UpdateAsync(Order entity)
        {
            await _orderRepository.UpdateAsync(entity).ConfigureAwait(true);
        }
    }
}
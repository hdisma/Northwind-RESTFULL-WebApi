using Northwind.Core.Entities.Northwind;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Core.Interfaces
{
    public interface IOrderService : IService<Order>
    {
        Task<IReadOnlyCollection<Order>> GetByCustomerId(string customerId);
        Task<Order> GetByCustomerIdAndOrderId(string customerId, int orderId);
        Task<bool> CustomerExists(string customerId);
    }
}
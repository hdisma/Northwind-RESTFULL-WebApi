using Northwind.Core.Entities.Northwind;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Core.Interfaces
{
    public interface IOrderService : IService<Order>
    {
        Task<IReadOnlyCollection<Order>> GetByCustomerId(string customerId);
        Task<bool> CustomerExists(string customerId);
    }
}
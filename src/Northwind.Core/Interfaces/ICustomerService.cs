using Northwind.Core.Entities.Northwind;
using Northwind.Core.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Core.Interfaces
{
    public interface ICustomerService : IService<Customer>
    {
        Task<IReadOnlyList<Customer>> GetAllAsync(CustomerResourceParameters customerResourceParameters);
        Task<bool> Exists(string customerId);

    }
}

using Northwind.Core.Entities.Northwind;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerById(string id);
    }
}

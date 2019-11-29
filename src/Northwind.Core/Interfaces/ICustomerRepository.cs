using Northwind.Core.Entities.Northwind;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Core.Interfaces
{
    public interface ICustomerRepository : IAsyncRepository<Customer>
    {
        
    }
}
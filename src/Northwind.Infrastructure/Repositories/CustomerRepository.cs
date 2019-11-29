using Northwind.Core.Entities.Northwind;
using Northwind.Core.Interfaces;
using Northwind.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Infrastructure.Repositories
{
    public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(NorthwindDbContext northwindDbContext) : base(northwindDbContext)
        {
        }
    }
}

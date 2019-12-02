using Northwind.Core.Entities.Northwind;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Northwind.Core.Specification
{
    public class CustomersCountSpecification : BaseSpecification<Customer>
    {
        public CustomersCountSpecification(Expression<Func<Customer, bool>> criteria) : base(criteria)
        {
        }

        public CustomersCountSpecification() : base(null)
        {
        }
    }
}

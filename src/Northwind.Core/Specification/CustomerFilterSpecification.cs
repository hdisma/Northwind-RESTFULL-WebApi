using Northwind.Core.Entities.Northwind;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Northwind.Core.Specification
{
    public class CustomerFilterSpecification : BaseSpecification<Customer>
    {
        public CustomerFilterSpecification() : base(null)
        {
        }

        public CustomerFilterSpecification(Expression<Func<Customer, bool>> criteria) : base(criteria)
        {
        }
    }
}

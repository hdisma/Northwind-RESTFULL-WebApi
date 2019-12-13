using Northwind.Core.Entities.Northwind;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Northwind.Core.Specification
{
    public class OrderForCustomerSpecification : BaseSpecification<Order>
    {
        public OrderForCustomerSpecification(Expression<Func<Order, bool>> criteria) : base(criteria)
        {
        }
        public OrderForCustomerSpecification() : base(null)
        {
        }
    }
}

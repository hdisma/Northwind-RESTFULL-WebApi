using Northwind.Core.Entities.Northwind;
using System.Linq.Expressions;
using System;

namespace Northwind.Core.Specification
{
    public class OrderCountSpecification : BaseSpecification<Order>
    {
        public OrderCountSpecification(Expression<Func<Order, bool>> criteria) : base(criteria)
        {
        }

        public OrderCountSpecification() : base(null)
        {
            
        }
    }
}
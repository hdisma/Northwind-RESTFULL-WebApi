using Northwind.Core.Entities.Northwind;
using Northwind.Core.Interfaces;
using Northwind.Infrastructure.DbContexts;

namespace Northwind.Infrastructure.Repositories
{
    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {
        public OrderRepository(NorthwindDbContext northwindDbContext) : base(northwindDbContext)
        {
        }
    }
}
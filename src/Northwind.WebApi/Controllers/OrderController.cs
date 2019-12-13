using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Core.Interfaces;
using Northwind.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/Orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService
                            ?? throw new ArgumentNullException(nameof(orderService));
            _mapper = mapper
                      ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersForCustomer(string customerId)
        {
            if (!await _orderService.CustomerExists(customerId))
                return NotFound("Customer Not Found!");

            var orders = _mapper.Map<IReadOnlyList<OrderDto>>(await _orderService.GetByCustomerId(customerId));

            if (orders.Count == 0 || orders == null) return NotFound("This customer has no orders");

            return Ok(orders);
        }

    }
}

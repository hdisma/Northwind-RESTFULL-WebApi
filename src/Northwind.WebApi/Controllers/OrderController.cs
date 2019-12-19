using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Core.Entities.Northwind;
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

        [HttpGet]
        [Route("{orderId}", Name = "GetOrderForCustomer")]
        public async Task<IActionResult> GetOrderForCustomer(string customerId, int orderId)
        {
            if (!await _orderService.CustomerExists(customerId))
                return NotFound("Customer Not Found!");

            var order = _mapper.Map<OrderDto>(await _orderService.GetByIdAsync(orderId));

            if (order == null) return NotFound("The order for this customer was not found!");

            return Ok(order);
        }

        public async Task<IActionResult> AddOrderForCustomer(string customerId, [FromBody] OrderForCreationDto model)
        {
            if (!await _orderService.CustomerExists(customerId))
                return NotFound("Customer Not Found!");

            var order = _mapper.Map<Order>(model);
            var orderCreated = _mapper.Map<OrderDto>(await _orderService.AddAsync(order));

            return CreatedAtRoute("GetOrderForCustomer",
                                  new { customerId, orderId = orderCreated.OrderID },
                                  orderCreated);
        }

    }
}

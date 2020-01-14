using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,
                               IMapper mapper,
                               ICustomerService customerService)
        {
            _orderService = orderService
                            ?? throw new ArgumentNullException(nameof(orderService));
            _customerService = customerService
                               ?? throw new ArgumentNullException(nameof(customerService));
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

        [HttpPost]
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
        
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrderForCustomer(string customerId, int orderId, OrderForUpdateDto model)
        {
            if (!await _customerService.Exists(customerId))
                return NotFound("Customer Not Found!");

            var order = await _orderService.GetByCustomerIdAndOrderId(customerId, orderId);

            // If the order doesn't exists then will be created (upserting)
            if (order == null)
            {
                order = await _orderService.AddAsync(_mapper.Map<Order>(model));
                var orderAdded = _mapper.Map<OrderDto>(order);

                return CreatedAtRoute("GetOrderForCustomer",
                                      new { customerId, orderId = orderAdded.OrderID },
                                      orderAdded);
            }

            _mapper.Map(model, order);
            await _orderService.UpdateAsync(order);

            return NoContent();
        }

        [HttpPatch("{orderId}")]
        public async Task<IActionResult> PartiallyUpdateOrderForCustomer(string customerId,
                                                                         int orderId,
                                                                         [FromBody]JsonPatchDocument<OrderForUpdateDto> patchDocument)
        {
            if (!await _customerService.Exists(customerId))
                return NotFound("Customer Not Found!");

            var order = await _orderService.GetByCustomerIdAndOrderId(customerId, orderId);

            if (order == null)
            {
                var orderDto = new OrderForUpdateDto();
                patchDocument.ApplyTo(orderDto, ModelState);

                if (!await TryUpdateModelAsync(orderDto))
                    return ValidationProblem(ModelState);

                var orderAdded = _mapper.Map<OrderDto>(await _orderService.AddAsync(_mapper.Map<Order>(orderDto)));

                return CreatedAtRoute("GetOrderForCustomer",
                                      new { customerId, orderId = orderAdded.OrderID },
                                      orderAdded);
            }

            var orderToPatch = _mapper.Map<OrderForUpdateDto>(order);

            patchDocument.ApplyTo(orderToPatch, ModelState);

            if (!await TryUpdateModelAsync(orderToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(orderToPatch, order);
            await _orderService.UpdateAsync(order);

            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(string customerId, int orderId)
        {
            if (!await _customerService.Exists(customerId))
                return NotFound("Customer Not Found!");

            var order = await _orderService.GetByIdAsync(orderId);

            if (order == null)
                return NotFound("Order Not Found!");

            await _orderService.DeleteAsync(order);

            return NoContent();
        }

        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                                     .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

    }
}

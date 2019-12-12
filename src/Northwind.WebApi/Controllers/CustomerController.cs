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
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = _mapper.Map<IReadOnlyList<CustomerDto>>(await _customerService.GetAllAsync());

            if (customers == null || customers.Count == 0) return NotFound();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(string id)
        {
            var customer = _mapper.Map<CustomerDto>(await _customerService.GetByIdAsync(id));

            if (customer == null) return NotFound();

            return Ok(customer);
        }

        [HttpGet]
        [Route("Count")]
        public async Task<IActionResult> GetCustomersCount()
        {
            var result = await _customerService.CountAsync();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody]CustomerDto model)
        {
            await _customerService.UpdateAsync(_mapper.Map<Customer>(model));
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer([FromBody]CustomerDto model)
        {
            var customer = _mapper.Map<Customer>(model);
            await _customerService.DeleteAsync(customer);

            return Ok();
        }

        [HttpGet]
        [Route("exists")]
        public async Task<IActionResult> Exists([FromBody]CustomerDto model)
        {
            var customer = _mapper.Map<Customer>(model);
            
            var result = await _customerService.Exists(customer);

            if(result) return Ok(result);

            return NotFound();
        }

    }
}

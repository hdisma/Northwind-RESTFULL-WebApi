using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Core.Entities.Northwind;
using Northwind.Core.Interfaces;
using Northwind.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = _mapper.Map<IReadOnlyList<CustomerViewModel>>(await _customerService.GetAllAsync());

            if (customers == null || customers.Count == 0) return NotFound();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = _mapper.Map<CustomerViewModel>(await _customerService.GetByIdAsync(id));

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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]CustomerViewModel model)
        {
            var customer = _mapper.Map<Customer>(model);
            await _customerService.DeleteAsync(customer);

            return Ok();
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = _mapper.Map<CustomerViewModel>(await _customerService.GetByIdAsync(id));

            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpGet]
        [Route("Count")]
        public async Task<IActionResult> GetCustomersCount()
        {
            var result = await _customerService.CountAsync();

            return Ok(result);
        }

    }
}

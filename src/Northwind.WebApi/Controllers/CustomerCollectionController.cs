using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Core.Entities.Northwind;
using Northwind.Core.Interfaces;
using Northwind.WebApi.Helpers;
using Northwind.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    [ApiController]
    [Route("api/customercollections")]
    public class CustomerCollectionController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerCollectionController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService
                               ?? throw new ArgumentNullException(nameof(customerService));
            _mapper = mapper
                      ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet("({ids})", Name = "GetCustomerCollection")]
        public async Task<IActionResult> GetCustomerCollections(
        [FromRoute]
        [ModelBinder(typeof(ArrayModelBinder))] 
        IEnumerable<string> ids)
        {
            if (ids == null) return BadRequest();

            var customers =  _mapper.Map<IReadOnlyList<CustomerDto>>(await _customerService.GetByIdsAsync(ids.ToList()));

            if (customers.Count == 0 || customers == null) return NotFound();

            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerCollection([FromBody] IEnumerable<CustomerForCreationDto> modelCollection)
        {
            var customers = _mapper.Map<IReadOnlyList<Customer>>(modelCollection);
            var customersAdded = _mapper.Map<IReadOnlyList<CustomerDto>>(await _customerService.AddRangeAsync(customers));
            var idsAsString = string.Join(",", customersAdded.Select(c => c.CustomerID));

            //return Ok(customersAdded);
            return CreatedAtRoute("GetCustomerCollection",
                                new { ids = idsAsString },
                                customersAdded);
        }
    }
}

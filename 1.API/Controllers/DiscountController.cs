using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.API.Request;
using _2.Domain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountData _discountData;

        private readonly IDiscountDomain _discountDomain;

        private readonly IMapper _mapper;
        
        public DiscountController(IDiscountData discountData, IDiscountDomain discountDomain, IMapper mapper)
        {
            _discountData = discountData;
            _discountDomain = discountDomain;
            _mapper = mapper;
        }
        // GET: api/Discount
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _discountData.getAllAsync();
            var result = _mapper.Map<List<Client>, List<Client>>(data);
            return Ok(result);
        }

        // GET: api/Discount/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _discountData.getByIdAsync(id);
            var result = _mapper.Map<Client, Client>(data);
            
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/Membership/status")]
        public async Task<IActionResult> GetMembershipStatus()
        {
            var membershipStatus = new List<string> { "regular", "silver", "gold" };
        return Ok(membershipStatus);
        }
        
        [HttpGet]
        [Route("api/Product/type")]
        public async Task<IActionResult> GetProductType()
        {
            var productType = new List<string> { "books", "electronics", "others" };
            return Ok(productType);
        }

        // POST: api/Discount
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientRequest data)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var client = _mapper.Map<ClientRequest, Client>(data);
                var result = await _discountDomain.SaveAsync(client);
                return Created("api/client", result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/Discount/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClientRequest data)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest();
                var client = _mapper.Map<ClientRequest, Client>(data);
                var result = await _discountDomain.UpdateAsync(client, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/Discount/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _discountDomain.DeleteAsync(id);
            return Ok(result);
        }
    }
}

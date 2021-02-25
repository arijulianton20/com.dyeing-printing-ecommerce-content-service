using com.dyeingprinting.service.content.business;
using com.dyeingprinting.service.content.data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace com.dyeingprinting.service.content.api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/customer-care")]
    public class CustomerCareController : ControllerBase
    {
        private readonly ILogger<CustomerCareController> _logger;
        private readonly IService<CustomerCare> _service;

        public CustomerCareController(IService<CustomerCare> service,ILogger<CustomerCareController> logger)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            try
            {
                var result = await _service.FindAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CustomerCare customerContent)
        {


            await _service.Create(customerContent);
            return CreatedAtRoute(
            "GetCustomerCare",
            new { Id = customerContent.Id },
            customerContent);

        }

        [HttpGet("{id}", Name = "GetCustomerCare")]
        public async Task<ActionResult> Get(int id)
        {
            var mobileContent = await _service.GetSingleById(id);
            return Ok(mobileContent);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CustomerCare customerCare)
        {
            try
            {
                CustomerCare customerCare1
                 = await _service.GetSingleById(id);

                await _service.Update(customerCare1, customerCare);
                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Data = customerCare });
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {

                await _service.Delete(id);
                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Data = id});
            }
            catch (Exception e)
            {

                return StatusCode(500);
            }
        }
    }
}

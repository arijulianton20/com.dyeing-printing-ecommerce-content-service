using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using com.dyeingprinting.service.content.business;
using com.dyeingprinting.service.content.data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace com.dyeingprinting.service.content.api.Controllers
{
    [ApiController]
    [Route("mobilecontent")]
    public class MobileContentController : ControllerBase
    {
        private readonly ILogger<MobileContentController> _logger;
        private readonly IService<MobileContent> _service;

        public MobileContentController(IService<MobileContent> service, ILogger<MobileContentController> logger)
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
        public async Task<ActionResult> Post([FromBody] MobileContent mobileContent)
        {


            await _service.Create(mobileContent);
            return CreatedAtRoute(
            "GetMobile",
            new { Id = mobileContent.Id },
            mobileContent);
            //var result = new ResultFormatter(API_VERSION, General.CREATED_STATUS_CODE, General.OK_MESSAGE)
            //  .Ok();
            //return Created(string.Concat(Request.Path, "/", 0), result);

        }

        [HttpGet("{id}", Name = "GetMobile")]
        public async Task<ActionResult> Get(int id)
        {
            var mobileContent = await _service.GetSingleById(id);
            return Ok(mobileContent);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] MobileContent mobileContent)
        {
            try {
                MobileContent mobileContent1
                 = await _service.GetSingleById(id);

                await _service.Update(mobileContent1, mobileContent);
                return NoContent();
            }
            catch (Exception e) {
                return StatusCode(500);
            }
           /* if (mobileContent == null)
            {
                return BadRequest("record is null.");
            }
         
            if (mobileContent1 == null)
            {
                return NotFound("The  record couldn't be found.");
            }*/
           
            /*WebContent webContentToUpdate = await _service.GetSingleById(id);
            await _service.Update(webContentToUpdate, webContent);
            return NoContent();*/



        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {

                await _service.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {

                return StatusCode(500);
            }
        }
    }
}

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
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/web-content")]
    public class WebContentController : ControllerBase
    {
        private readonly ILogger<WebContentController> _logger;
        private readonly IService<WebContent> _service;

        public WebContentController(IService<WebContent> service, ILogger<WebContentController> logger)
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
                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Data = result });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] WebContent webContent)
        {
            try
            {
                await _service.Create(webContent);
                //return CreatedAtRoute("Get", new { Id = webContent.Id }, webContent);

                var result = new { StatusCode = (int)HttpStatusCode.OK, Message = "Success", Data = new { } };
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
            //var result = new ResultFormatter(API_VERSION, General.CREATED_STATUS_CODE, General.OK_MESSAGE)
            //  .Ok();
            //return Created(string.Concat(Request.Path, "/", 0), result);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var webContent = await _service.GetSingleById(id);

                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Data = webContent });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] WebContent webContent)
        {
            try
            {
                /*if (webContent == null)
                {
                    return BadRequest("record is null.");
                }*/
                WebContent webContent1 = await _service.GetSingleById(id);

                /*    if (webContent1 == null)
                    {
                        return NotFound("The  record couldn't be found.");
                    }*/
                await _service.Update(webContent1, webContent);

                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Data = webContent });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

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

                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Data = new { }, Message = "Success" });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Retailr3.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackgroundSenderController : ControllerBase
    {
        // GET: api/BackgroundSender
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BackgroundSender/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BackgroundSender
        [HttpPost]
        public JsonResult Post([FromBody] BackgroundSenderRequest value)
        {
            return new JsonResult(value);
        }

        // PUT: api/BackgroundSender/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

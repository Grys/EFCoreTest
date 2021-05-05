using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuCitizens.Database;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RuCitizens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizensController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<CitizensController> _logger;

        public CitizensController(ILogger<CitizensController> logger, DatabaseContext databaseContex)
        {
            this._logger = logger;
            this._databaseContext = databaseContex;
        }

        // GET: api/<CitizensController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CitizensController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CitizensController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CitizensController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CitizensController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

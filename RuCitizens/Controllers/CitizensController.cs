using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuCitizens.Database;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<Citizen> Get()
        {
            return this._databaseContext.Citizens.Select(x => x);
        }

        // GET api/<CitizensController>/5
        [HttpGet("{id}")]
        public Citizen Get(int id)
        {
            return this._databaseContext.Citizens.Find(id);
        }

        // POST api/<CitizensController>
        [HttpPost]
        public void Post([FromBody] Citizen value)
        {
            this._databaseContext.Citizens.Add(value);
            this._databaseContext.SaveChanges();
        }

        // PUT api/<CitizensController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Citizen value)
        {
        }

        // DELETE api/<CitizensController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var toDelete = this._databaseContext.Citizens.Find(id);
            this._databaseContext.Citizens.Remove(toDelete);
            this._databaseContext.SaveChanges();
        }
    }
}

using System;
using RuCitizens.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;


namespace RuCitizens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizensController : ControllerBase
    {
        private readonly CitizensRepository _citizensRepository;
        private readonly ILogger<CitizensController> _logger;

        public CitizensController(ILogger<CitizensController> logger, DatabaseContext databaseContex)
        {
            this._logger = logger;
            this._citizensRepository = new CitizensRepository(databaseContex);
         }

        // GET: api/<CitizensController>
        [HttpGet]
        public IEnumerable<Citizen> Search(string fullName, DateTime? birthDate, DateTime? deathDate)
        {
            return this._citizensRepository.FindByFIOAndDates(fullName, birthDate, deathDate);
        }

        // GET api/<CitizensController>/5
        [HttpGet("{id}")]
        public Citizen Get(int id)
        {
            return this._citizensRepository.FindById(id);
        }

        // POST api/<CitizensController>
        [HttpPost]
        public void Post([FromBody] Citizen value)
        {
            try
            {
                value.Validate();
                this._citizensRepository.Create(value);
                this._citizensRepository.Save();
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        // PUT api/<CitizensController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Citizen value)
        {
            try
            {
                value.Validate();
                this._citizensRepository.Update(id, value);
                this._citizensRepository.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        // DELETE api/<CitizensController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                this._citizensRepository.Delete(id);
                this._citizensRepository.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}

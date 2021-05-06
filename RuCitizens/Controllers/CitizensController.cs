using System;
using RuCitizens.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

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

        [HttpGet("DownloadCSV")]
        public IActionResult DownloadCSV(string fullName, DateTime? birthDate, DateTime? deathDate)
        {
            var records = this._citizensRepository.FindByFIOAndDates(fullName, birthDate, deathDate);

            var memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            CsvWriter csvWriter = new CsvWriter(textWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteHeader<Citizen>();
            csvWriter.NextRecord();
            csvWriter.WriteRecords(records);
            csvWriter.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);

            var contentType = "text/csv";
            var fileName = "citizens.csv";
            return File(memoryStream, contentType, fileName);
        }

        [HttpPost("UploadCSV")]
        public IActionResult UploadCSV(IFormFile file)
        {

            TextReader textReader = new StreamReader(file.OpenReadStream());
            CsvReader csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);
            var records = csvReader.GetRecords<Citizen>();
            var arrData = records.ToArray();

            try
            {
                // TODO: Data validation
                this._citizensRepository.Create(arrData);
                this._citizensRepository.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.ToContentResult();
            }

            return new OkResult();
        }


        // GET api/<CitizensController>/5
        [HttpGet("{id}")]
        public Citizen Get(int id)
        {
            return this._citizensRepository.FindById(id);
        }

        // POST api/<CitizensController>
        [HttpPost]
        public IActionResult Post([FromBody] Citizen value)
        {
            try
            {
                value.Validate();
                this._citizensRepository.Create(value);
                this._citizensRepository.Save();
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.ToContentResult();
            }
            return new OkResult();
        }

        // PUT api/<CitizensController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Citizen value)
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
                return ex.ToContentResult();
            }
            return new OkResult();
        }

        // DELETE api/<CitizensController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this._citizensRepository.Delete(id);
                this._citizensRepository.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.ToContentResult();
            }
            return new OkResult();
        }
    }
}

using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLayer.Controllers
{
    [Route("/api/continent/")]
    [ApiController]
    public class GeographyController : ControllerBase
    {
        private IManager manager;
        private ILogger logger;
        private string hostURL;

        public GeographyController(IManager manager, IConfiguration config, ILogger<GeographyController> logger)
        {
            this.manager = manager;
            hostURL = config.GetValue<string>("profiles:RestLayer:applicationUrl");
            this.logger = logger;
        }



        #region Continent

        /// <summary>
        /// POST a continent to the database
        /// </summary>
        /// <param name="input">Continent to POST.</param>
        /// <returns>The continent added to the database.</returns>
        [HttpPost]
        public ActionResult<RContinentOutput> PostContinent([FromBody] RContinentInput input)
        {
            try
            {
                logger.LogInformation(0101, "PostContinent called.");
                if (input == null)
                    throw new RestException("Het ingegeven continent mag niet null zijn");

                int id = manager.AddContinent(Mapper.ToContinent(input));
                return CreatedAtAction(nameof(GetContinent), new { id }, Mapper.ToRContinentOutput(manager.GetContinent(id), hostURL));

            }
            catch (Exception ex)
            {
                logger.LogError(0101, "PostContinent failed.");

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// GET a continent from the database
        /// </summary>
        /// <param name="id">Id of the continent to GET</param>
        /// <returns>The continent from the database.</returns>
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public ActionResult<RContinentOutput> GetContinent(int id)
        {
            try
            {
                logger.LogInformation(0102, "GetContinent called.");
                return Ok(Mapper.ToRContinentOutput(manager.GetContinent(id), hostURL));

            }
            catch (Exception ex)
            {
                logger.LogError(0102, "GetContinent failed.");

                if (ex.Message == "Er is geen continent met het gegeven id.")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// PUT an updated continent in the database.
        /// </summary>
        /// <param name="id">Id of the continent.</param>
        /// <param name="input">Updated continent.</param>
        /// <returns>The updated continent.</returns>
        [HttpPut("{id}")]
        public ActionResult<RContinentOutput> PutContinent(int id, [FromBody] RContinentInput input)
        {
            try
            {
                logger.LogInformation(0103, "PutContinent called.");
                if (input == null)
                {
                    throw new RestException("Het ingegeven continent mag niet null zijn");
                }

                if (id != input.Id)
                {
                    throw new RestException("De id in de url en in de body komen niet overeen.");
                }
                manager.UpdateContinent(id, Mapper.ToContinent(input));

                return Ok(Mapper.ToRContinentOutput(manager.GetContinent(id), hostURL));

            }
            catch (Exception ex)
            {
                logger.LogError(0103, "PutContinent failed.");

                if (ex.Message == "Er is geen continent met het gegeven id.")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// DELETE a Continent from the database.
        /// </summary>
        /// <param name="id">Id of the Continent.</param>
        /// <returns>A NOCOntent response.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteContinent(int id)
        {
            try
            {
                logger.LogInformation(0104, "DeleteContinent called.");
                manager.DeleteContinent(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(0104, "DeleteContinent failed.");

                if (ex.Message == "Er is geen continent met het gegeven id.")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        #endregion


        #region Country

        /// <summary>
        /// POST a country to the database
        /// </summary>
        /// <param name="input">Country to POST.</param>
        /// <param name="continentId">Id to POST.</param>
        /// <returns>The country added to the database.</returns>
        [HttpPost("{continentId}/country/")]
        public ActionResult<RCountryOutput> PostCountry([FromBody] RCountryInput input, int continentId)
        {
            try
            {
                logger.LogInformation(0201, "PostCountry called.");

                if (input == null)
                    throw new RestException("Het ingegeven land mag niet null zijn");

                if (continentId != input.ContinentId)
                    throw new RestException("De id in de url en in de body komen niet overeen.");

                    int countryId = manager.AddCountry(continentId, input.Name, input.Population,input.Surface);
                return CreatedAtAction(nameof(GetCountry), new { continentId, countryId }, Mapper.ToRCountryOutput(manager.GetCountry(continentId,countryId), hostURL));

            }
            catch (Exception ex)
            {
                logger.LogError(0201, "PostCountry failed.");

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// POST a country from the database
        /// </summary>
        /// <param name="countryId">Id of the country to GET</param>
        /// <param name="continentId">Id of the continent the country belongs to</param>
        /// <returns>The continent from the database.</returns>
        [HttpGet("{continentId}/country/{countryId}")]
        [HttpHead("{continentId}/country/{countryId}")]
        public ActionResult<RCountryOutput> GetCountry(int continentId, int countryId)
        {
            try
            {
                logger.LogInformation(0202, "GetCountry called.");

                return Ok(Mapper.ToRCountryOutput(manager.GetCountry(continentId, countryId), hostURL));
            }
            catch (Exception ex)
            {
                logger.LogError(0202, "GetCountry failed.");

                if (ex.Message == "Er is geen continent met het gegeven id.")
                    return NotFound(ex.Message);

                if (ex.Message == "Er is geen land met het gegeven id.")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// PUT a country from the database
        /// </summary>
        /// <param name="countryId">Id of the country to GET</param>
        /// <param name="continentId">Id of the continent the country belongs to</param>
        /// <returns>The continent from the database.</returns>
        [HttpPut("{continentId}/country/{countryId}")]
        public ActionResult<RCountryOutput> PutCountry(int continentId, int countryId, [FromBody] RCountryInput input)
        {
            try
            {
                logger.LogInformation(0203, "PutCountry called.");

                if (input == null)
                {
                    throw new RestException("Er moet een land zijn om te putten.");
                }
                if (continentId != input.ContinentId)
                {
                    throw new RestException("Het id van het continent in de url en in de body komen niet overeen.");
                }
                if (countryId != input.Id)
                {
                    throw new RestException("Het id van het land in de url en in de body komen niet overeen.");
                }

                manager.UpdateCountry(countryId, continentId, input.Name, input.Population, input.Surface);

                return Ok(Mapper.ToRCountryOutput(manager.GetCountry(continentId, countryId), hostURL));
            }
            catch (Exception ex)
            {
                logger.LogError(0203, "PutCountry failed.");

                if (ex.Message == "Er is geen continent met het gegeven id.")
                    return NotFound(ex.Message);

                if (ex.Message == "Er is geen land met het gegeven id.")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// DELETE a country from the database
        /// </summary>
        /// <param name="countryId">Id of the country to GET</param>
        /// <param name="continentId">Id of the continent the country belongs to</param>
        /// <returns>The continent from the database.</returns>
        [HttpDelete("{continentId}/country/{countryId}")]
        public IActionResult DeleteCountry(int continentId, int countryId)
        {
            try
            {
                logger.LogInformation(0204, "DeleteCountry called.");

                manager.DeleteCountry(continentId, countryId);

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(0204, "DeleteCountry failed.");

                if (ex.Message == "Er is geen continent met het gegeven id.")
                    return NotFound(ex.Message);

                if (ex.Message == "Er is geen land met het gegeven id.")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}

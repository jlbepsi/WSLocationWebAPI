using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using LocationLibrary.BusinessLogic;
using LocationLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSLocationWebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private static ObjectResult ServerError503 = new ObjectResult("Erreur générale") { StatusCode = StatusCodes.Status503ServiceUnavailable };

        private readonly ILocationService _locationService;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(ILocationService locationService, ILogger<LocationsController> logger)
        {
            // Mise en oeuvre de l'Injection de Dépendance (voir Program.cs)
            _locationService = locationService;
            // Gestion des logs
            _logger = logger;
        }

        // GET: api/v1/location
        /// <summary>
        /// Retourne la liste des locations <code>Location</code>
        /// </summary>
        /// <returns>Une liste d'objet Location</returns>
        /// <see cref="Location"/>
        /// <example>
        /// http://serveur/api/v1/locations
        /// </example>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Location>> GetLocations()
        {
            try
            {
                return _locationService.GetLocations();
            }
            catch (LocationException locationException)
            {
                _logger.LogError(locationException, locationException.Message);
                return StatusCode(locationException.StatusCode);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        // GET api/<LocationController>/5
        /// <summary>
        /// Retourne la location <code>Location</code> identifié par <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un objet Location</returns>
        /// <see cref="Location"/>
        /// <response code="404">La location d'id id n'existe pas</response>     
        /// <example>
        /// http://serveur/api/v1/locations/3
        /// </example>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Location> GetLocation(int id)
        {
            try
            {
                Location location = _locationService.GetLocation(id);
                if (location == null)
                {
                    return NotFound();
                }
                return location;
            }
            catch (LocationException locationException)
            {
                _logger.LogError(locationException, locationException.Message);
                return StatusCode(locationException.StatusCode);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        // POST api/v1/locations
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Location> AddLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Location locationCreated = null;
            try
            {
                locationCreated = _locationService.AddLocation(location);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return BadRequest(exception.Message);
            }

            return CreatedAtAction(nameof(GetLocation), new { id = locationCreated.Id }, locationCreated);
        }

        // PUT api/v1/locations/5
        /// <summary>
        /// Cette méthode n'est pas implémentée
        /// </summary>
        /// <param name="id"></param>
        /// <param name="location"></param>
        /// <returns>Toujours BadRequest</returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public IActionResult UpdateLocation(int id, Location location)
        {
            return BadRequest("Non implémenté, vous devez supprimer puis créer une location");
        }

        // DELETE api/<LocationController>/5
        /// <summary>
        /// Supprime une location d'après son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <param name="id"></param>
        /// <returns>L'objet Location supprimé</returns>
        /// <see cref="Location"/>
        /// <response code="404">La location d'id n'existe pas</response>     
        /// <example>
        /// http://serveur/api/v1/locations/3
        /// </example>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteLocation(int id)
        {
            Location location = null;
            try
            {
                location = _locationService.DeleteLocation(id);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return BadRequest(exception.Message);
            }

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }
    }
}

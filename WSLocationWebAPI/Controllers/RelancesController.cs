using System;
using System.Collections.Generic;
using LocationLibrary.BusinessLogic;
using LocationLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace WSLocationWebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RelancesController : ControllerBase
    {
        private readonly IRelanceService _relanceService;
        private readonly ILogger<LocationsController> _logger;

        public RelancesController(IRelanceService relanceService, ILogger<LocationsController> logger)
        {
            // Mise en oeuvre de l'Injection de Dépendance (voir Program.cs)
            _relanceService = relanceService;
            // Gestion des logs
            _logger = logger;
        }

        // GET: api/<RelancesController>
        /// <summary>
        /// Retourne la liste des relances
        /// </summary>
        /// <returns>Une liste d'objet Relance</returns>
        /// <see cref="Relance"/>
        /// <example>
        /// http://serveur/api/v1/relances
        /// </example>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Relance>> Get()
        {
            try
            {
                return _relanceService.GetRelances();
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

        // GET api/<RelancesController>/5

        /// <summary>
        /// Retourne la relance identifiée par <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un objet Relance</returns>
        /// <see cref="Relance"/>
        /// <response code="404">La relance d'id id n'existe pas</response>     
        /// <example>
        /// http://serveur/api/v1/relances/3
        /// </example>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Relance> GetRelance(int id)
        {
            try
            {
                Relance relance = _relanceService.GetRelance(id);
                if (relance == null)
                {
                    return NotFound();
                }
                return relance;
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

    }
}

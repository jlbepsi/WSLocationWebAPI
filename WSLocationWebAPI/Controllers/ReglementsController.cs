using LocationLibrary.BusinessLogic;
using LocationLibrary.Models;
using Microsoft.AspNetCore.Mvc;


namespace WSLocationWebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReglementsController : ControllerBase
    {
        private readonly IReglementService _reglementService;
        private readonly ILogger<LocationsController> _logger;

        public ReglementsController(IReglementService ReglementService, ILogger<LocationsController> logger)
        {
            // Mise en oeuvre de l'Injection de Dépendance (voir Program.cs)
            _reglementService = ReglementService;
            // Gestion des logs
            _logger = logger;
        }

        // GET: api/<ReglementsController>
        /// <summary>
        /// Retourne la liste des règlements
        /// </summary>
        /// <returns>Une liste d'objet Reglement</returns>
        /// <see cref="Reglement"/>
        /// <example>
        /// http://serveur/api/v1/reglements
        /// </example>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Reglement>> Get()
        {
            try
            {
                return _reglementService.GetReglements();
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

        // GET api/<ReglementsController>/5

        /// <summary>
        /// Retourne le règlement identifié par <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un objet Reglement</returns>
        /// <see cref="Reglement"/>
        /// <response code="404">Le règlement d'id id n'existe pas</response>     
        /// <example>
        /// http://serveur/api/v1/reglements/3
        /// </example>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Reglement> GetReglement(int id)
        {
            try
            {
                Reglement Reglement = _reglementService.GetReglement(id);
                if (Reglement == null)
                {
                    return NotFound();
                }
                return Reglement;
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

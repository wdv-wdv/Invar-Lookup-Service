
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LookupServiceAPI.Controllers
{
    /// <summary>
    /// CreditDataController
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="CreditDataMergeService"></param>
    [ApiController]
    [Route("credit-data")]
    public class CreditDataController : ControllerBase
    {
        private readonly ILogger<CreditDataController> logger;

        public CreditDataController(ILogger<CreditDataController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Return aggregated credit data.
        /// </summary>
        /// <param name="ssn">Social security number</param>
        /// <response code="200">Aggregated credit data for given ssn.</response>
        /// <response code="404">Credit data not found for given ssn.</response>
        [Route("{ssn}")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync(string ssn)
        {
            try
            {
                var dataMergeService = MergeService.GetInstance();
                var creditData = await dataMergeService.MergeCreditData(ssn);
                if (creditData != null)
                {
                    return Ok(creditData);
                }
                else
                {
                    return StatusCode(404);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500);

            }
        }
    }
}

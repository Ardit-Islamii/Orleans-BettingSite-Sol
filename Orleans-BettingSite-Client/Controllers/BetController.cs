using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans_BettingSite.Requests;
using Orleans_BettingSite_Common.GrainInterfaces;
using Orleans_BettingSite_Common.Requests;
using Orleans_BettingSite_Common.Responses;

namespace Orleans_BettingSite_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly IClusterClient _client;

        public BetController(IClusterClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets a bet grain based on a guid id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BetReadResponse>> GetBetGrainAsync(Guid id)
        {
            var betGrain = _client.GetGrain<IIntermediateGrain>(id);
            var test = await betGrain.GetBetAsync(id);
            return Ok(test);
        }

        /// <summary>
        /// Initiates a grain and sets a bet amount state to it. If grain is already initialized, simply changes the bet amount state.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="betRequest"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<ActionResult<BetCreateResponse>> CreateBetAsync(Guid id, [FromBody] BetCreateRequest betRequest)
        {
            var intermediateGrain = _client.GetGrain<IIntermediateGrain>(id);
            var result = await intermediateGrain.SetBetAmountAsync(betRequest.Amount);
            return Ok(result);
        }
    }
}

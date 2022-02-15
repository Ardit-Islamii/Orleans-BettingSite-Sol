using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans_BettingSite.ObserverClasses;
using Orleans_BettingSite.Requests;
using Orleans_BettingSite_Common.GrainInterfaces;
using Orleans_BettingSite_Common.Requests;
using Orleans_BettingSite_Common.Responses;

namespace Orleans_BettingSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly IGrainFactory _factory;
        private Test test = new Test();

        public BetController(IGrainFactory factory)
        {
            _factory = factory;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BetReadResponse>> GetBetAsync(Guid id)
        {
            var intermediateGrain = _factory.GetGrain<IIntermediateGrain>(id);
            var result = await intermediateGrain.GetBetAsync(id);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<BetCreateResponse>> CreateBetAsync(Guid id, [FromBody] BetCreateRequest betRequest)
        {
            var intermediateGrain = _factory.GetGrain<IIntermediateGrain>(id);
            var result = await intermediateGrain.SetBetAmountAsync(betRequest.Amount);
            return Ok(result);
        }
    }
}

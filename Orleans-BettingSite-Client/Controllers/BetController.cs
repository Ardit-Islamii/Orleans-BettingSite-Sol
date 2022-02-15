using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans_BettingSite.ObserverClasses;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<BetReadResponse>> GetBetGrainAsync(Guid id)
        {
            var betGrain = _client.GetGrain<IIntermediateGrain>(id);
            var test = await betGrain.GetBetAsync(id);
            return Ok(test);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<BetCreateResponse>> CreateBetAsync(Guid id, [FromBody] BetCreateRequest betRequest)
        {
            var intermediateGrain = _client.GetGrain<IIntermediateGrain>(id);
            var result = await intermediateGrain.SetBetAmountAsync(betRequest.Amount);
            return Ok(result);
        }

        [HttpPost("subscribe/{id}")]
        public async Task SubscribeToTestObserverAsync(Guid id)
        {
            var testGrain = _client.GetGrain<ITestGrain>(id);
            Test test = new Test();
            var obj = await _client.CreateObjectReference<ITest>(test);

            await testGrain.Subscribe(obj);
        }

        [HttpPost("unsubscribe/{id}")]
        public async Task UnsubscribeToTestObserverAsync(Guid id)
        {
            var testGrain = _client.GetGrain<ITestGrain>(id);
            await testGrain.UnSubscribe();
        }

        [HttpPost("sendmessage/{id}")]
        public async Task SendMessageAsync(Guid id, [FromBody] string message)
        {
            var testGrain = _client.GetGrain<ITestGrain>(id);
            await testGrain.SendMessage(message);
        }
    }
}

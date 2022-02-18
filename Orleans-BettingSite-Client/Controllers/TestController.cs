using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans_BettingSite.ObserverClasses;
using Orleans_BettingSite_Common.GrainInterfaces;

namespace Orleans_BettingSite_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IClusterClient _client;

        public TestController(IClusterClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Subscribes to a test observer where you consume random messages.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("subscribe/{id}")]
        public async Task SubscribeToTestObserverAsync(Guid id)
        {
            var testGrain = _client.GetGrain<ITestGrain>(id);
            Test test = new Test();
            var obj = await _client.CreateObjectReference<ITest>(test);

            await testGrain.Subscribe(obj);
        }

        /// <summary>
        /// Unsubscribes to the test observer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("unsubscribe/{id}")]
        public async Task UnsubscribeToTestObserverAsync(Guid id)
        {
            var testGrain = _client.GetGrain<ITestGrain>(id);
            await testGrain.UnSubscribe();
        }

        /// <summary>
        /// Sends a message to all test grains who are subscribed to the observer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("sendmessage/{id}")]
        public async Task SendMessageAsync(Guid id, [FromBody] string message)
        {
            var testGrain = _client.GetGrain<ITestGrain>(id);
            await testGrain.SendMessage(message);
        }

        [HttpPost("getcount/{id}")]
        public async Task<int> SendMessageAsync(Guid id)
        {
            var testGrain = _client.GetGrain<ITestGrain>(id);
            return await testGrain.GetObserversCount();
        }
    }
}

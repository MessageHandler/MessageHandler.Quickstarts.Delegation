using MessageHandler.Quickstart.Contract;
using MessageHandler.Runtime.AtomicProcessing;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("emails")]
    public class CommandController : ControllerBase
    {
        private IDispatchMessages dispatcher;

        public CommandController(IDispatchMessages dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [HttpPost()]
        [Route("notifybuyer")]
        //[Authorize]
        public async Task<IActionResult> Notify([FromBody] NotifyBuyer cmd)
        {
            await dispatcher.Dispatch(cmd);

            return Ok();
        }

    }
}
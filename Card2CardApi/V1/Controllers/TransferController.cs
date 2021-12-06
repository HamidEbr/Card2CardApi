using Application.Command;
using Card2CardApi.Service.V1.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace Card2CardApi.Service.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/TransferRequests")]
    [ApiController]
    //[Authorize]
    public class TransferController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation("Transfer card to card request")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, "Accepted", typeof(long))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Source bank pan not founded")]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, "Source bank is disable")]
        public async Task<IActionResult> PostAsync([FromBody] Card2CardRequestModel request)
        {
            var command = new Card2CardCommand()
            {
                Amount = request.Amount,
                CVV2 = request.CVV2,
                DestinationPan = request.DestinationPan,
                ExpMonth = request.ExpMonth,
                ExpYear = request.ExpYear,
                MobileNumber = request.MobileNumber,
                OTP = request.OTP,
                SourcePan = request.SourcePan,
            };

            var response = await _mediator.Send(command);

            return new OkObjectResult(response);
        }
    }
}

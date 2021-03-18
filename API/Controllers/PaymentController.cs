using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Queries.PaymentStatus;
using Common.Dto;
using Grpc.Net.Client;
using Infrastructure.Models.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace NewPlace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("donate")]
        public async Task<IActionResult> Donate(DonateRequest request)
        {
            var donationConfirmation = await _mediator.Send(new DonateCommand(Guid.Parse(request.UserId), request.Amount, request.Currency));
            return Ok(donationConfirmation);
        }

        [HttpGet("status")]
        public async Task<IActionResult> CheckStatus(string paymentId)
        {
            var donationConfirmation = await _mediator.Send(new PaymentStatusQuery(Guid.Parse(paymentId)));
            return Ok(donationConfirmation);
        }
    }
}
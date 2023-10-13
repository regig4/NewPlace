using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApplicationCore.Application.Commands;
using ApplicationCore.Application.Queries;
using Common.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace NewPlace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("donate")]
        public async Task<IActionResult> Donate(DonateRequest request)
        {
            DonationConfirmation donationConfirmation = await _mediator.Send(new DonateCommand(Guid.Parse(request.UserId), request.Amount, request.Currency));
            return Ok(donationConfirmation);
        }

        [HttpGet("top-donations")]

        [ProducesResponseType(typeof(List<PaymentDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> TopDonations(uint count = 10)
        {
            List<PaymentDto> donations = await _mediator.Send(new TopDonationsQuery(count));
            return Ok(donations);
        }

        [HttpGet("status")]
        [ProducesResponseType(typeof(PaymentStatusResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CheckStatus(string paymentId)
        {
            PaymentStatusResponse donationConfirmation = await _mediator.Send(new PaymentStatusQuery(Guid.Parse(paymentId)));
            return Ok(donationConfirmation);
        }
    }
}
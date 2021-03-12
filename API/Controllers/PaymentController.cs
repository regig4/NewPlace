using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Donate(ulong userId, ulong amount, string currency)
        {
            var donationConfirmation = await _mediator.Send(new DonateCommand(userId, amount, currency));
            return Ok(donationConfirmation);
        }
    }
}
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
    public class PaymentController : Controller
    {
        IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Donate(int userId, decimal amount, string currency)
        {
            var donationConfirmation = await _mediator.Send(new DonateCommand(userId, amount, currency));
            return Ok(donationConfirmation);
        }
    }
}
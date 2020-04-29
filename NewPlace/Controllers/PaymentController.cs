using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PaymentService;


namespace NewPlace.Controllers
{
    public class PaymentController : Controller
    {
        public async Task<string> Donate()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");     // todo: inject or service discovery?
            var client = new Greeter.GreeterClient(channel);
            var response = await client.SayHelloAsync(new HelloRequest { Name = "world!" });
            return response.Message;
        }
    }
}
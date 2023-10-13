using System.Net.Http;
using Grpc.Net.Client;

namespace Infrastructure.Factories
{
    public class GrpcChannelFactory
    {
        public static GrpcChannelFactory Instance = new GrpcChannelFactory();

        private GrpcChannelFactory() { }

        public GrpcChannel Create()
        {
            HttpClientHandler? httpHandler = new HttpClientHandler
            {
                // Return `true` to allow certificates that are untrusted/invalid
                ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            GrpcChannel? channel = GrpcChannel.ForAddress("https://localhost:5003",
                new GrpcChannelOptions { HttpClient = new HttpClient(httpHandler) });
            return channel;
        }
    }
}

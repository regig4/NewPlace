using Grpc.Net.Client;
using System.Net.Http;

namespace Infrastructure.Factories
{
    public class GrpcChannelFactory
    {
        public static GrpcChannelFactory Instance = new GrpcChannelFactory();

        private GrpcChannelFactory() { }

        public GrpcChannel Create()
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5003", 
                new GrpcChannelOptions { HttpClient = new HttpClient(httpHandler) });
            return channel;
        }
    }
}

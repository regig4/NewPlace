using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Application.Services;
using ApplicationCore.DTOs;

namespace Infrastructure.Services
{
    public class UserServiceProxy : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AdvertisementDto>> GetObservedAdvertisements(Guid userId)
        {
            string? response = await _httpClient.GetStringAsync("observed/" + userId.ToString());
            return JsonSerializer.Deserialize<List<AdvertisementDto>>(response);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.DTOs;
using System.Threading.Tasks;
using Common.Dto;

namespace ApplicationCore.Services
{
    public interface IAuthService
    {
        Task<UserDto> AuthorizeAsync(UserDto user, string password);
    }
}

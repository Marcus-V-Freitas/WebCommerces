using BLL.Models.DTOs;
using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommerce.Client.Services.HttpRepository
{
    public interface IAuthenticationService
    {
        Task<AuthResponseDto> Login(AuthResponseDto authResult);
        Task Logout();

        Task<string> RefreshToken();
    }
}

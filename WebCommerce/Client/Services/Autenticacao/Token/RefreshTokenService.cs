using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HttpRepository;

namespace WebCommerce.Client.Services.Autenticacao.Token
{

    public class RefreshTokenService
    {
        private readonly AuthenticationStateProvider _authProvider;
        private readonly IAuthenticationService _authService;
        private readonly ILocalStorageService _localStorage;

        public RefreshTokenService(AuthenticationStateProvider authProvider, IAuthenticationService authService,
             ILocalStorageService localStorage)
        {
            _authProvider = authProvider;
            _authService = authService;
            _localStorage = localStorage;
        }

        public async Task<string> TryRefreshToken()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            if (string.IsNullOrEmpty(authState.User.Identity.Name))
            {
                return string.Empty;
            }
            var user = authState.User;

            var exp = user.FindFirst(c => c.Type.Equals("exp")).Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));

            var timeUTC = DateTime.UtcNow;

            var diff = expTime - timeUTC;
            if (diff.TotalMinutes <= 2)
                return await _authService.RefreshToken();

            return string.Empty;
        }
    }
}

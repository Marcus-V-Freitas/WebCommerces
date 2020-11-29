using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using BLL.Models.DTOs;
using WebCommerce.Client.Services.AuthProviders;
using BLL.Models.ViewModels;
using WebCommerce.Client.Services.HTTP;
using WebCommerce.Client.Services.HTTP.Interface;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Services.HttpRepository
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public AuthenticationService(IHttpService client, HttpClient http, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _http = http;
        }

        public async Task<AuthResponseDto> Login(AuthResponseDto authResult)
        {

            await _localStorage.SetItemAsync("authToken", authResult.Token);
            await _localStorage.SetItemAsync("refreshToken", authResult.RefreshToken);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResult.Token);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResult.Token);

            return new AuthResponseDto { IsAuthSuccessful = true };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _http.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
            if (token != null && refreshToken != null)
            {
                var refreshDTO = new RefreshTokenDto { Token = token, RefreshToken = refreshToken };
                var refreshResult = await _client.PUT<AuthResponseDto>($"{Api.Usuario}/refresh", refreshDTO);

                await _localStorage.SetItemAsync("authToken", refreshResult.Token);
                await _localStorage.SetItemAsync("refreshToken", refreshResult.RefreshToken);

                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", refreshResult.Token);
                return refreshResult.Token;
            }
            return "";
        }
    }
}

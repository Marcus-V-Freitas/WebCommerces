using BLL.Models;
using DAL.Repositories.EntityFramework.Context;
using DAL.Repositories.EntityFramework.Interfaces;
using DAL.Repositories.EntityFramework.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Server.Services.HTTP.Interface;

namespace WebCommerce.Server.Services.HTTP.Repository
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly IUsuarioRepository _usuarioRepository;

        public TokenService(IConfiguration configuration, UserManager<User> userManager, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Email,user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            await Task.Delay(1);

            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value)),
                ValidateLifetime = false,
                ValidIssuer = _jwtSettings.GetSection("validIssuer").Value,
                ValidAudience = _jwtSettings.GetSection("validAudience").Value,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<Usuario> ExtractUsuarioToken(string authorization)
        {
            if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("bearer", StringComparison.OrdinalIgnoreCase)) { return null; }

            var principal = GetPrincipalFromExpiredToken(authorization.Substring("bearer".Length).Trim());
            var username = principal.Identity.Name;
            return await _usuarioRepository.UsuarioPorEmail(username, true);
        }
    }
}

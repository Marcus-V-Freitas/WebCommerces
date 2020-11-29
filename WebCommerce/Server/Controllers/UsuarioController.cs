using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BLL.Exceptions;
using BLL.Library.Cookies;
using BLL.Library.Criptografia;
using BLL.Models;
using BLL.Models.DTOs;
using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework.Context;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebCommerce.Server.Services.HTTP.Interface;
using resources = BLL.Library.Resources.Models.Validation;

namespace WebCommerce.Server.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;
        private readonly IStringLocalizer<resources> _localizer;
        private readonly UserManager<User> _userManager;
        private readonly IEnderecoEntregaRepository _entregaRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository,
                                ITokenService tokenService,
                                IStringLocalizer<resources> localizer,
                                UserManager<User> userManager,
                                IEnderecoEntregaRepository entregaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _localizer = localizer;
            _userManager = userManager;
            _entregaRepository = entregaRepository;
        }

        /// <summary>
        /// Adiciona um novo usuário na base de dados
        /// </summary>
        /// <param name="usuario"> Objeto Usuário </param>
        /// <returns> Status da Operação </returns>
        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioCadastroViewModel usuario)
        {
            IdentityResult result = null;
            if (ModelState.IsValid)
            {
                var user = new User { UserName = usuario.Usuario.Email, Email = usuario.Usuario.Email };
                result = await _userManager.CreateAsync(user, usuario.Usuario.Senha);
                if (!result.Succeeded)
                {
                    await _usuarioRepository.Adicionar(usuario);
                    var errors = result.Errors.Select(e => e.Description);
                    return BadRequest(errors);
                }
                await _userManager.AddToRoleAsync(user, "Administrator");
            }
            return Ok(result.Succeeded);
        }

        /// <summary>
        /// Faz o Login do Usuário no sistema e gera um token para validar o
        /// período de permanência no sistema
        /// </summary>
        /// <param name="usuario"> Objeto Usuário </param>
        /// <returns> Token Gerado </returns>
        [HttpPost("Token")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AutorizacaoToken([FromBody] LoginViewModel usuario)
        {
            User user = await _userManager.FindByNameAsync(usuario.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, usuario.Senha))
                return BadRequest(_localizer[resources.UsuarioAutenticacao].Value);

            var signingCredentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(user);
            var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            user.RefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, RefreshToken = user.RefreshToken });
        }


        /// <summary>
        /// Atualiza o Token Gerado quando o mesmo expirar
        /// </summary>
        /// <param name="tokenDto"> Token do Usuário </param>
        /// <returns> Token Novo Gerado </returns>
        [HttpPut("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto tokenDto)
        {
            if (tokenDto is null)
            {
                return BadRequest(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = _localizer[resources.UsuarioAutenticacao].Value });
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(tokenDto.Token);
            var username = principal.Identity.Name;
            var usuarioIdentity = await _userManager.FindByNameAsync(username);

            var signingCredentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(usuarioIdentity);
            var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            usuarioIdentity.RefreshToken = _tokenService.GenerateRefreshToken();
            usuarioIdentity.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(usuarioIdentity);

            return Ok(new AuthResponseDto { Token = token, RefreshToken = usuarioIdentity.RefreshToken, IsAuthSuccessful = true });
        }

        /// <summary>
        /// Atualiza a Senha do Usuário
        /// </summary>
        /// <param name="senha"> Nova Senha </param>
        /// <param name="authorization"> Token de Autorização </param>
        /// <returns> Indicativo se a senha foi trocada com sucesso </returns>
        [HttpPut("TrocarSenha")]
        public async Task<IActionResult> TrocarSenha([FromBody] SenhaViewModel senha, [FromHeader] string authorization)
        {
            var usuario = await _tokenService.ExtractUsuarioToken(authorization);

            User user = await _userManager.FindByNameAsync(usuario.Email);
            if (user is null)
                return BadRequest("");

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, senha.Senha);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _usuarioRepository.TrocarSenha(usuario.Id, senha);
            }
            return Ok(result.Succeeded);

        }

        /// <summary>
        /// Obter o usuário e suas informações pelo seu token
        /// </summary>
        /// <param name="authorization"> token de autorização </param>
        /// <returns> Dados do usuário </returns>
        [HttpGet("")]
        public async Task<IActionResult> ObterUsuario([FromHeader] string authorization)
        {
            return Ok(await _tokenService.ExtractUsuarioToken(authorization));
        }

        /// <summary>
        /// Atualiza os dados do usuário passados por parâmetros
        /// </summary>
        /// <param name="usuario"> objeto do usuário </param>
        /// <param name="authorization"> token de autorização </param>
        /// <returns> Indicativo se os dados do usuário foram atualizado </returns>
        [HttpPut("")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] Usuario usuario, [FromHeader] string authorization)
        {
            var usuarioAtual = await _tokenService.ExtractUsuarioToken(authorization);
            await _usuarioRepository.Atualizar(usuarioAtual.Id, usuario);
            return Ok(true);
        }
    }
}
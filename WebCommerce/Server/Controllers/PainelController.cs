
using BLL.Models;
using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Server.Dependency;
using WebCommerce.Server.Services.HTTP.Interface;

namespace WebCommerce.Server.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class PainelController : Controller
    {
        private readonly IPainelRepository _painelRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public PainelController(IPainelRepository painelRepository, IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _painelRepository = painelRepository;
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Retorna todos os dados referentes as compras feitas pelo cliente no período informado
        /// </summary>
        /// <param name="filtro"> filtro de períodos </param>
        /// <param name="authorization"> token de autorização </param>
        /// <returns> todos os daos referentes as feitas pelo client no período </returns>
        [HttpPost("")]
        public async Task<IActionResult> RetornarDadosCliente([FromBody] FiltroDataViewModel filtro, [FromHeader] string authorization)
        {
            var usuario = await _tokenService.ExtractUsuarioToken(authorization);

            if (usuario is null)
                return BadRequest("");

            return Ok(await _painelRepository.RetornarDadosUsuario(usuario.Id, filtro));
        }
    }
}

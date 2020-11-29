using BLL.Models;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Server.Services.HTTP.Interface;

namespace WebCommerce.Server.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class EnderecoEntregaController : Controller
    {
        private readonly IEnderecoEntregaRepository _entregaRepository;
        private readonly ITokenService _tokenService;

        public EnderecoEntregaController(IEnderecoEntregaRepository entregaRepository, ITokenService tokenService)
        {
            _entregaRepository = entregaRepository;
            _tokenService = tokenService;
        }

        [HttpPut("")]
        public async Task<IActionResult> AtualizarEndereco([FromBody] EnderecoEntrega enderecoEntrega, [FromHeader] string authorization)
        {
            enderecoEntrega.Usuario = await _tokenService.ExtractUsuarioToken(authorization);
            await _entregaRepository.Atualizar(enderecoEntrega.Id, enderecoEntrega);
            return Ok(true);
        }

        [HttpGet("")]
        public async Task<IActionResult> ObterEnderecoEntregaUsuario([FromHeader] string authorization)
        {
            var usuario = await _tokenService.ExtractUsuarioToken(authorization);
            return Ok(await _entregaRepository.EnderecoEntregaUsuario(usuario.Id));
        }
    }
}

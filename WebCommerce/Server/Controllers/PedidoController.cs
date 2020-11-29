using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCommerce.Server.Services.HTTP.Interface;

namespace WebCommerce.Server.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ITokenService _tokenService;
        private readonly IUsuarioRepository _usuarioRepository;

        public PedidoController(IPedidoRepository pedidoRepository, ITokenService tokenService, IUsuarioRepository usuarioRepository)
        {
            _pedidoRepository = pedidoRepository;
            _tokenService = tokenService;
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Retorna todos os pedidos feitos durante o período do filtro pelo usuário
        /// </summary>
        /// <param name="filtro"> filtro do período escolhido </param>
        /// <param name="authorization"> token de Autorização </param>
        /// <returns> Todos os pedidos feitos pelo usuário </returns>
        [HttpPost("")]
        public async Task<IActionResult> RetornarPedidosData([FromBody] FiltroDataViewModel filtro, [FromHeader] string authorization)
        {
            var usuario = await _tokenService.ExtractUsuarioToken(authorization);

            if (usuario is null)
                return BadRequest("");

            return Ok(await _pedidoRepository.RetornarPedidosData(usuario.Id, filtro));
        }
    }
}

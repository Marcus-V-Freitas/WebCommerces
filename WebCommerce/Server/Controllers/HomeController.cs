
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebCommerce.Server.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public HomeController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        /// <summary>
        /// Retorna todos os produtos que vão aparecer na página principal
        /// </summary>
        /// <returns> Lista de Produtos </returns>
        //[ResponseCache(CacheProfileName = "CachePrincipal")]
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                ProdutoAVenda = await _produtoRepository.ObterTodos()
            };
            return Ok(homeViewModel);
        }
    }
}

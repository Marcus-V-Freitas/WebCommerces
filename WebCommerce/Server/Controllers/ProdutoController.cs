
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BLL.Models;
using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework;
using DAL.Repositories.EntityFramework.Interfaces;
using DAL.Repositories.EntityFramework.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCommerce.Server.Services.HTTP.Interface;

namespace WebCommerce.Server.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueReppository _estoqueReppository;
        private readonly ITokenService _tokenService;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProdutoController(IProdutoRepository produtoRepository,
                                 IEstoqueReppository estoqueRepository,
                                 ITokenService tokenService,
                                 IUsuarioRepository usuarioRepository)
        {
            _produtoRepository = produtoRepository;
            _estoqueReppository = estoqueRepository;
            _tokenService = tokenService;
            _usuarioRepository = usuarioRepository;
        }


        /// <summary>
        /// Retorna todos os produtos cadastrados
        /// baseados ou não no filtro
        /// </summary>
        /// <param name="data"> Filtro de busca por nome do produto </param>
        /// <returns> Lista de Produtos </returns>
        [ResponseCache(CacheProfileName = "CachePrincipal")]
        [HttpGet("")]
        public async Task<IActionResult> ObterTodos([FromQuery] string data = "")
        {
            return Ok(await _produtoRepository.ObterProdutosFiltro(data));
        }

        /// <summary>
        /// Retorna todos os produtos baseados na categoria passada
        /// </summary>
        /// <param name="id"> Código da Categoria </param>
        /// <returns> Lista de Produtos </returns>
        [HttpGet("Categoria/{id}")]
        public async Task<IActionResult> ObterTodosCategoria([FromRoute] int id)
        {

            return Ok(await _produtoRepository.ObterProdutosCategoria(id));
        }

        /// <summary>
        /// Adiciona um novo produto na base de dados
        /// </summary>
        /// <param name="produto"> Objeto Produto </param>
        /// <returns> Status da Operação </returns>
        [HttpPost("")]
        public async Task<IActionResult> Cadastrar([FromBody] Produto produto)
        {
            await _produtoRepository.Adicionar(produto);
            return Ok(true);
        }

        /// <summary>
        /// Atualiza um produto já existente
        /// </summary>
        /// <param name="id"> Código do Produto </param>
        /// <param name="produto"> Objeto Produto </param>
        /// <returns> Status da Operação </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Produto produto)
        {
            if (id != produto.ID)
                return BadRequest();

            await _produtoRepository.Atualizar(id, produto);
            return Ok(true);
        }

        /// <summary>
        /// Obtém o produto referente ao código passado
        /// </summary>
        /// <param name="id"> Código do Produto </param>
        /// <param name="viewModel"> (Opcional) Indica se é uma ViewModel do produto ou não, por padrão é true  </param>
        /// <returns> Produto </returns>
        [ResponseCache(CacheProfileName = "CacheMinimo")]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterId([FromRoute] int id, bool viewModel = true)
        {
            var produto = await _produtoRepository.ObterId(id);
            if (!viewModel)
                return Ok(produto);

            var quantidade = await _estoqueReppository.RetornarQuantidadeDisponivel(produto);

            var produtoViewModel = new ProdutoViewModel
            {
                Produto = produto,
                QuantidadeDisponivel = quantidade
            };
            return Ok(produtoViewModel);
        }

        /// <summary>
        /// Remove o produto da base de dados
        /// </summary>
        /// <param name="id"> Código do produto </param>
        /// <returns> Status da Operação </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            await _produtoRepository.Remover(id);

            return Ok(true);
        }

        /// <summary>
        /// Adiociona uma nova classificação do usuário para o produto informado
        /// </summary>
        /// <param name="produto"> informações referentes ao produto e a classificação do usuário </param>
        /// <param name="authorization"> token de autorização </param>
        /// <returns> Informatitavo se os dados foram inseridos com sucesso </returns>
        [HttpPost("Classificacao")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Classificar([FromBody] UsuarioClassificacaoProduto produto, [FromHeader] string authorization)
        {
            produto.Usuario = await _tokenService.ExtractUsuarioToken(authorization);

            produto.Produto = await _produtoRepository.ObterId(produto.ProdutoClassificacaoId);
            var media = await _produtoRepository.ClassificacaoRegistrar(produto);

            return Ok(await _produtoRepository.MediaProduto(produto.ProdutoClassificacaoId, media));

        }

    }
}

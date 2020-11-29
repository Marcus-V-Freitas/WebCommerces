using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCommerce.Server.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        /// <summary>
        /// Retorna todas as categorias cadastradas
        /// baseadas ou não no filtro
        /// </summary>
        /// <param name="data"> Filtro de busca por nome da categoria </param>
        /// <returns> Lista de Categorias </returns>
        [HttpGet("")]
        public async Task<IActionResult> ObterTodos([FromQuery] string data)
        {
            return Ok(await _categoriaRepository.ObterCategoriasFiltro(data));
        }

        /// <summary>
        /// Adiciona uma nova categoria na base de dados
        /// </summary>
        /// <param name="categoria"> Objeto Categoria </param>
        /// <returns> Status da Operação </returns>
        [HttpPost("")]
        public async Task<IActionResult> Cadastrar([FromBody] Categoria categoria)
        {

            if (ModelState.IsValid)
            {
                await _categoriaRepository.Adicionar(categoria);
            }
            return Ok(true);
        }

        /// <summary>
        /// Atualiza uma categoria já existente
        /// </summary>
        /// <param name="id"> Código da Categoria </param>
        /// <param name="categoria"> Objeto Categoria </param>
        /// <returns> Status da Operação </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _categoriaRepository.Atualizar(id, categoria);
            }
            return Ok(true);

        }

        /// <summary>
        /// Obtém a categoria referente ao código passado
        /// </summary>
        /// <param name="id"> Código da Categoria </param>
        /// <returns> Categoria </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterId([FromRoute] int id)
        {

            return Ok(await _categoriaRepository.ObterId(id));
        }

        /// <summary>
        /// Remove a categoria da base de dados
        /// </summary>
        /// <param name="id"> Código da Categoria </param>
        /// <returns> Status da Operação </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar([FromRoute] int id)
        {
            await _categoriaRepository.Remover(id);
            return Ok(true);
        }


    }
}

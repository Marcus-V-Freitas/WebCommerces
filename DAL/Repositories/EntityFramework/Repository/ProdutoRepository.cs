using BLL.Library.ExtensionsMethods;
using BLL.Models;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Repository
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(WebContext context) : base(context)
        {

        }

        public async Task<List<Produto>> ObterProdutosFiltro(string data)
        {
            var Entities = _entity.AsQueryable();

            if (!string.IsNullOrEmpty(data))
            {
                Entities = Entities.Where(x => EF.Functions.Like(x.Nome.ToUpper().Trim(), $"%{data.Trim()}%"));

            }
            return await Entities.ToListAsync();
        }

        public async Task<List<Produto>> ObterProdutosCategoria(int id)
        {

            var Entities = _entity.AsQueryable();

            if (id != 0)
            {
                Entities = Entities.Where(x => x.CategoriaId == id);
            }
            return await Entities.ToListAsync();
        }

        new public async Task<Produto> ObterId(int id)
        {
            return await _context.Produtos.Where(x => x.ID == id).Include(x => x.ClassificacaoProdutos).FirstAsync();
        }

        new public async Task<List<Produto>> ObterTodos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<int> ClassificacaoRegistrar(UsuarioClassificacaoProduto produtoClassificacao)
        {
            _context.UsuarioClassificacaoProduto.Add(produtoClassificacao);
            _context.SaveChanges();
            var context = _context.UsuarioClassificacaoProduto.AsQueryable().Where(x => x.ProdutoClassificacaoId == produtoClassificacao.ProdutoClassificacaoId);
            var quantidade = await context.CountAsync();
            var classificacoes = await context.SumAsync(x => x.Classificacao);
            return Convert.ToInt32(classificacoes / quantidade);
        }

        public async Task<bool> MediaProduto(int id, int Media)
        {
            var produto = await _context.Produtos.Where(x => x.ID == id).FirstOrDefaultAsync();
            produto.MediaProduto = Media;
            _context.SaveChanges();
            return true;
        }
    }
}

using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<List<Produto>> ObterProdutosFiltro(string data);

        Task<List<Produto>> ObterProdutosCategoria(int id);

        Task<int> ClassificacaoRegistrar(UsuarioClassificacaoProduto produtoClassificacao);

        Task<bool> MediaProduto(int id, int Media);
    }
}

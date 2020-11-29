using BLL.Models;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DAL.Repositories.EntityFramework.Repository
{
    public class EstoqueRepository : BaseRepository<Estoque>, IEstoqueReppository
    {
        public EstoqueRepository(WebContext context) : base(context)
        {

        }

        public async Task<int> RetornarQuantidadeDisponivel(Produto produto)
        {
            return await (from prod in _context.Estoques
                          where prod.ProdutoEstoqueId == produto.ID
                          select prod.Disponivel).FirstOrDefaultAsync();
        }

    }
}

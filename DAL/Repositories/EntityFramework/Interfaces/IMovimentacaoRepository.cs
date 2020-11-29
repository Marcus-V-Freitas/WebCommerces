using BLL.Enums;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface IMovimentacaoRepository : IBaseRepository<MovimentacaoEstoque>
    {
        Task MovimentarEstoqueVenda(Venda venda, StatusMovimentacao status, TipoMovimentacao tipo);
    }
}

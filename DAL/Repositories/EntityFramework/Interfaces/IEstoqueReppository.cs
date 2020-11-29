using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface IEstoqueReppository : IBaseRepository<Estoque>
    {
        Task<int> RetornarQuantidadeDisponivel(Produto produto);
    }
}

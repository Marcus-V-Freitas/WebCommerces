using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task Adicionar(T Entity);

        Task Atualizar(int id, T Entity);

        Task<T> ObterId(int id);

        Task Remover(int id);

        Task<List<T>> ObterTodos();
    }
}

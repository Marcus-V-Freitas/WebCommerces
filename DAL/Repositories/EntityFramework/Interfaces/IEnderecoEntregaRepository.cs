using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface IEnderecoEntregaRepository : IBaseRepository<EnderecoEntrega>
    {
        Task<EnderecoEntrega> EnderecoEntregaUsuario(int id);
    }
}

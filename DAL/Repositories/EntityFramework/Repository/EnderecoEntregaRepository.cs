using BLL.Models;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Repository
{
    public class EnderecoEntregaRepository : BaseRepository<EnderecoEntrega>, IEnderecoEntregaRepository
    {
        public EnderecoEntregaRepository(WebContext context) : base(context)
        {
        }


        public async Task<EnderecoEntrega> EnderecoEntregaUsuario(int id)
        {
            return await _context.EnderecoEntrega.Where(x => x.UsuarioEnderecoId == id).FirstOrDefaultAsync();
        }
    }
}

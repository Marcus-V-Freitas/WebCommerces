using BLL.Models;
using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(WebContext context) : base(context) { }


        public async Task<Usuario> Login(string email, string senha)
        {
            return await _context.Usuarios.Where(m => m.Email == email && m.Senha == senha).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> TrocarSenha(int id, SenhaViewModel senha)
        {
            bool resultado = senha.Senha.Equals(senha.SenhaRepetida);
            if (resultado)
            {
                var usuario = await _context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
                usuario.Senha = senha.Senha;
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            return resultado;
        }

        public async Task<Usuario> UsuarioPorEmail(string email, bool tracking = false)
        {
            if (tracking)
            {
                return await _context.Usuarios.Where(m => m.Email == email).FirstOrDefaultAsync();
            }
            return await _context.Usuarios.Where(m => m.Email == email).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task Adicionar(UsuarioCadastroViewModel usuarioCadastro)
        {
            _context.Usuarios.Add(usuarioCadastro.Usuario);
            await _context.SaveChangesAsync();
            usuarioCadastro.Endereco.Usuario = await UsuarioPorEmail(usuarioCadastro.Usuario.Email, true);
            _context.EnderecoEntrega.Add(usuarioCadastro.Endereco);
            await _context.SaveChangesAsync();
        }
    }
}

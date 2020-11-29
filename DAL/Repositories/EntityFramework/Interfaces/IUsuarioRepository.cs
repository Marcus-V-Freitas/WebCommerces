using BLL.Models;
using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> Login(string email, string senha);

        Task<Usuario> UsuarioPorEmail(string email, bool tracking = false);

        Task<bool> TrocarSenha(int id, SenhaViewModel senha);

        Task Adicionar(UsuarioCadastroViewModel usuarioCadastro);
    }
}

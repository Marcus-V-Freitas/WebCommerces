using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class UsuarioCadastroViewModel
    {
        [ValidateComplexType]
        public Usuario Usuario { get; set; }

        [ValidateComplexType]
        public EnderecoEntrega Endereco { get; set; }

        public UsuarioCadastroViewModel()
        {
            Usuario = new Usuario();
            Endereco = new EnderecoEntrega();
        }
    }
}

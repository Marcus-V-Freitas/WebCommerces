using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface IPainelRepository
    {
        public Task<PainelViewModel> RetornarDadosUsuario(int id, FiltroDataViewModel filtro = null);
    }
}

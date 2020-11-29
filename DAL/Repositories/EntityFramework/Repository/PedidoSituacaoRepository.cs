using BLL.Models;
using DAL.Repositories.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.EntityFramework.Repository
{
    public class PedidoSituacaoRepository : BaseRepository<PedidoSituacao>, IPedidoSituacaoRepository
    {
        public PedidoSituacaoRepository(WebContext context) : base(context)
        {
        }
    }
}

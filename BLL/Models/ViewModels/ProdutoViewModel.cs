using BLL.Models;
using System.Collections.Generic;

namespace BLL.Models.ViewModels
{
    public class ProdutoViewModel
    {
        public Produto Produto { get; set; }

        public int QuantidadeDisponivel { get; set; }
    }
}

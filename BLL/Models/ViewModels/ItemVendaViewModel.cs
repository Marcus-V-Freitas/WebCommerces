using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.ViewModels
{
    public class ItemVendaViewModel
    {
        public int ProdutoId { get; set; }

        public Produto Produto { get; set; }

        public int Quantidade { get; set; }

        public double Preço { get; set; }

    }
}

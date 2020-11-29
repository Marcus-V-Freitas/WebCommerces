using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class TransactionProduto
    {
        public TransacaoPagarMe Transaction { get; set; }
        public List<ItemVendaViewModel> Produtos { get; set; }
    }
}

using BLL.Models;
using System.Collections.Generic;

namespace BLL.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Produto> ProdutoAVenda { get; set; }
    }
}

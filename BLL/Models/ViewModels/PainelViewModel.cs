using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class PainelViewModel
    {
        public int QuantidadeCompras { get; set; }

        public decimal TotalDespesas { get; set; }

        public List<DespesasViewModel> ListaDespesas { get; set; }

        public List<DespesasViewModel> ComparativoAnual { get; set; }

        public List<DespesasViewModel> ComparativoMensal { get; set; }

        public List<DespesasViewModel> FormasPagamento { get; set; }
    }
}

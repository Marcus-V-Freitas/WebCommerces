using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.ViewModels
{
    public class VendaViewModel
    {
        public List<Parcelamento> Parcelas { get; set; }

        public List<string> NomesCartoesDeCredito { get; set; }
    }
}

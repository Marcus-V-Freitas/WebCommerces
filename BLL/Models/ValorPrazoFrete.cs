using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class ValorPrazoFrete
    {
        public string TipoFrete { get; set; }
        public string CodTipoFrete { get; set; }
        public double Valor { get; set; }
        public int Prazo { get; set; }
    }
}

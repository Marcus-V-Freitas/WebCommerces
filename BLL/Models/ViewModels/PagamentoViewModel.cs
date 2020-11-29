using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation = BLL.Library.Resources.Models.Validation;


namespace BLL.Models.ViewModels
{
    public class PagamentoViewModel
    {
        [ValidateComplexType]
        [Display(Name = "Cartão de Crédito")]
        public CartaoCredito CartaoDeCredito { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [Display(Name = "Frete")]
        public ValorPrazoFrete FreteValor { get; set; }

        public PagamentoViewModel()
        {
            CartaoDeCredito = new CartaoCredito();
        }
    }
}

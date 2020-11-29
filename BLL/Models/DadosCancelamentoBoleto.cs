using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Validation = BLL.Library.Resources.Models.Validation;

namespace BLL.Models
{
    public class DadosCancelamentoBoleto
    {
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Motivo { get; set; }

        public string FormaPagamento { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [MaxLength(3, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MaximoValor))]
        [MinLength(3, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        public string BancoCodigo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Agencia { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string AgenciaDV { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Conta { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string ContaDV { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        //[CPF(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E004")]
        public string CPF { get; set; }

        [MinLength(4, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Nome { get; set; }
    }
}

using BLL.Library.ExtensionsMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BLL.Library.Validacoes;
using Resources = BLL.Library.Resources.Models.Models;
using Validation = BLL.Library.Resources.Models.Validation;

namespace BLL.Models
{
    public class CartaoCredito
    {
        [LocalizedDisplayName(nameof(Resources.NumeroCartao), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [CreditCard]
        [MinLength(15, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        [MaxLength(16, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MaximoValor))]
        public string NumeroCartao { get; set; }


        [LocalizedDisplayName(nameof(Resources.NomeNoCartao), typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [MinLength(1, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        public string NomeNoCartao { get; set; }


        [LocalizedDisplayName(nameof(Resources.MesVencimentoCartao), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string VecimentoMM { get; set; }


        [LocalizedDisplayName(nameof(Resources.AnoVencimentoCartao), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string VecimentoYY { get; set; }


        [LocalizedDisplayName(nameof(Resources.codSegurancaCartao), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string CodigoSeguranca { get; set; }


        [LocalizedDisplayName(nameof(Resources.QuantidadeParcelas), typeof(Resources))]
        [Range(1, 12, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.EntreValores))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int QuantidadeParcelas { get; set; }
    }
}

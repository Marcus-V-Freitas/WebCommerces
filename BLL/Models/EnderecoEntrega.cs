using BLL.Library.Validacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Resources = BLL.Library.Resources.Models.Models;
using Validation = BLL.Library.Resources.Models.Validation;

namespace BLL.Models
{
    public class EnderecoEntrega
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [LocalizedDisplayName(nameof(Resources.Nome), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Nome { get; set; }


        [LocalizedDisplayName(nameof(Resources.CEP), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [MinLength(10, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        [MaxLength(10, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MaximoValor))]
        public string CEP { get; set; }


        [LocalizedDisplayName(nameof(Resources.Estado), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Estado { get; set; }


        [LocalizedDisplayName(nameof(Resources.Cidade), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Cidade { get; set; }


        [LocalizedDisplayName(nameof(Resources.Bairro), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Bairro { get; set; }


        [LocalizedDisplayName(nameof(Resources.Endereco), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Endereco { get; set; }


        [LocalizedDisplayName(nameof(Resources.Complemento), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Complemento { get; set; }


        [LocalizedDisplayName(nameof(Resources.Numero), typeof(Resources))]
        public string Numero { get; set; }

        [Required]
        public int UsuarioEnderecoId { get; set; }

        [ForeignKey(nameof(UsuarioEnderecoId))]
        public virtual Usuario Usuario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.Enums;
using Validation = BLL.Library.Resources.Models.Validation;
using Newtonsoft.Json;
using BLL.Library.Validacoes;
using Resources = BLL.Library.Resources.Models.Models;

namespace BLL.Models
{
    public class Usuario
    {
        [Key]
        [LocalizedDisplayName(nameof(Resources.Codigo), typeof(Resources))]
        public int Id { get; set; }


        [LocalizedDisplayName(nameof(Resources.Nome), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Nome { get; set; }


        [LocalizedDisplayName(nameof(Resources.Senha), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [MinLength(8, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        public string Senha { get; set; }


        [LocalizedDisplayName(nameof(Resources.Email), typeof(Resources))]
        [EmailAddress(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.EmailValido))]
        public string Email { get; set; }


        [LocalizedDisplayName(nameof(Resources.Nascimento), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public DateTime Nascimento { get; set; }


        [LocalizedDisplayName(nameof(Resources.Documento), typeof(Resources))]
        public string CPFCNPJ { get; set; }


        [LocalizedDisplayName(nameof(Resources.Sexo), typeof(Resources))]
        public Sexo Sexo { get; set; }


        [LocalizedDisplayName(nameof(Resources.Telefone), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Telefone { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Validation = BLL.Library.Resources.Models.Validation;

namespace BLL.Models
{
    public class MarcaCartaoCredito
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public bool Ativa { get; set; }
    }
}

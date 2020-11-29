using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Validation = BLL.Library.Resources.Models.Validation;

namespace BLL.Models
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]

        public string Descricao { get; set; }
    }
}

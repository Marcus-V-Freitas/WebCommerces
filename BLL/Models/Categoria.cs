using BLL.Library.Validacoes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Validation = BLL.Library.Resources.Models.Validation;
using Resources = BLL.Library.Resources.Models.Models;

namespace BLL.Models
{
    public class Categoria
    {
        [Key]
        [LocalizedDisplayName(nameof(Resources.Codigo), typeof(Resources))]
        public int Id { get; set; }


        [LocalizedDisplayName(nameof(Resources.Nome), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [MinLength(1, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        public string Nome { get; set; }


        [LocalizedDisplayName(nameof(Resources.Descricao), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [MinLength(1, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        public string Descricao { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Validation = BLL.Library.Resources.Models.Validation;

namespace BLL.Models
{
    public class Estoque
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int Disponivel { get; set; }

        [ForeignKey("Produto")]
        public int ProdutoEstoqueId { get; set; }

        public virtual Produto Produto { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int Transitorio { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public DateTime DataAtualizacao { get; set; }

        public int? QuantidadeMinima { get; set; }

    }
}

using BLL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Validation = BLL.Library.Resources.Models.Validation;

namespace BLL.Models
{
    public class MovimentacaoEstoque
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Produto")]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int ProdutoMovId { get; set; }
        public virtual Produto Produto { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int MovUsuarioID { get; set; }

        public virtual Usuario Usuario { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int Quantidade { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public DateTime DataMovimentacao { get; set; }

        [ForeignKey("Venda")]
        public int? VendaMovID { get; set; }

        public virtual Venda Venda { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public StatusMovimentacao StatusMovimetacao { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public TipoMovimentacao TipoMovimentacao { get; set; }


    }
}

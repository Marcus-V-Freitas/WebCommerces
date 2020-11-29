using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Validation = BLL.Library.Resources.Models.Validation;

namespace BLL.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioPedidoId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public string TransactionId { get; set; } //PagarMe - Transaction -> ID.

        //Frete
        public string FreteEmpresa { get; set; } //ECT - Correios
        public string FreteCodRastreamento { get; set; }

        public string FormaPagamento { get; set; } //Boleto-Cartão Credito
        public decimal ValorTotal { get; set; }
        public string DadosTransaction { get; set; } //Transaction - JSON
        public string DadosProdutos { get; set; } //ProdutoItem - JSON

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public DateTime DataRegistro { get; set; }
        public string Situacao { get; set; }

        //URL - Com Site da Receita - Nota Fiscal
        public string NFE { get; set; }

        public virtual Usuario Usuario { get; set; }

        [ForeignKey("PedidoId")]
        public virtual ICollection<PedidoSituacao> PedidoSituacoes { get; set; }
    }
}

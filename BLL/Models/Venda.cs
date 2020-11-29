using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Validation = BLL.Library.Resources.Models.Validation;
using Resources = BLL.Library.Resources.Models.Models;
using BLL.Library.Validacoes;

namespace BLL.Models
{
    public class Venda
    {
        [Key]
        [LocalizedDisplayName(nameof(Resources.Codigo), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int Id { get; set; }


        [LocalizedDisplayName(nameof(Resources.Total), typeof(Resources))]
        [ScaffoldColumn(false)]
        public double Total { get; set; }


        [LocalizedDisplayName(nameof(Resources.Horario), typeof(Resources))]
        [ScaffoldColumn(false)]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public DateTime Horario { get; set; }


        [LocalizedDisplayName(nameof(Resources.Usuario), typeof(Resources))]
        [ForeignKey("Usuario")]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int usuarioVendaID { get; set; }


        public virtual Usuario Usuario { get; set; }


        [LocalizedDisplayName(nameof(Resources.Pedido), typeof(Resources))]
        [ForeignKey("Pedido")]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int IDPedidoVenda { get; set; }


        public virtual Pedido Pedido { get; set; }
    }
}

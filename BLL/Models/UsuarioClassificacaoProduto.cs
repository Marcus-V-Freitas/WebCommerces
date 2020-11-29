using BLL.Library.Validacoes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources = BLL.Library.Resources.Models.Models;
using Validation = BLL.Library.Resources.Models.Validation;

namespace BLL.Models
{
    public class UsuarioClassificacaoProduto
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int UsuarioClassificacaoId { get; set; }


        [ForeignKey(nameof(UsuarioClassificacaoId))]
        public virtual Usuario Usuario { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [Range(0, 5, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.EntreValores))]
        [LocalizedDisplayName(nameof(Resources.Classificacao), typeof(Resources))]
        public int Classificacao { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [LocalizedDisplayName(nameof(Resources.Comentario), typeof(Resources))]
        public string Comentario { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public DateTime DataRegistro { get; set; } = DateTime.Now;


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public bool PermitirVisualizar { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int ProdutoClassificacaoId { get; set; }


        [ForeignKey(nameof(ProdutoClassificacaoId))]
        public virtual Produto Produto { get; set; }

    }
}

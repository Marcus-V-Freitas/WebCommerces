using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Validation = BLL.Library.Resources.Models.Validation;
using Resources = BLL.Library.Resources.Models.Models;
using BLL.Library.Validacoes;

namespace BLL.Models
{
    public class Produto
    {
        [Key]
        [LocalizedDisplayName(nameof(Resources.Codigo), typeof(Resources))]
        public int ID { get; set; }


        [LocalizedDisplayName(nameof(Resources.Nome), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [MaxLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MaximoValor))]
        [MinLength(5, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        public string Nome { get; set; }


        [LocalizedDisplayName(nameof(Resources.Valor), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public double Valor { get; set; }


        [LocalizedDisplayName(nameof(Resources.Descricao), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        [MinLength(10, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MinimoValor))]
        [MaxLength(500, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.MaximoValor))]
        public string Descricao { get; set; }


        [LocalizedDisplayName(nameof(Resources.Imagem), typeof(Resources))]
        public string ImageThumbnailUrl { get; set; }


        [LocalizedDisplayName(nameof(Resources.Categoria), typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int CategoriaId { get; set; }


        [ForeignKey(nameof(CategoriaId))]
        public virtual Categoria Categoria { get; set; }


        [LocalizedDisplayName(nameof(Resources.Peso), typeof(Resources))]
        [Range(0.001, 30, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.EntreValores))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public double Peso { get; set; }


        [LocalizedDisplayName(nameof(Resources.Largura), typeof(Resources))]
        [Range(11, 105, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.EntreValores))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int Largura { get; set; }


        [LocalizedDisplayName(nameof(Resources.Altura), typeof(Resources))]
        [Range(2, 105, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.EntreValores))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int Altura { get; set; }


        [LocalizedDisplayName(nameof(Resources.Comprimento), typeof(Resources))]
        [Range(16, 105, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.EntreValores))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = nameof(Validation.Obrigatorio))]
        public int Comprimento { get; set; }


        [LocalizedDisplayName(nameof(Resources.MediaAvaliacoes), typeof(Resources))]
        public int MediaProduto { get; set; } = 0;


        public virtual ICollection<UsuarioClassificacaoProduto> ClassificacaoProdutos { get; set; }

    }
}

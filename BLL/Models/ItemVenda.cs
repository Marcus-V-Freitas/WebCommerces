using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Models
{
    public class ItemVenda
    {
        [Key]
        public int ID { get; set; }

        public int ProdutoId { get; set; }

        [ForeignKey(nameof(ProdutoId))]
        public virtual Produto Produto { get; set; }

        public int Quantidade { get; set; }

        public double Preço { get; set; }

        public int VendaId { get; set; }

        [ForeignKey(nameof(VendaId))]
        public virtual Venda Venda { get; set; }
    }
}

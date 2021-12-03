using System.ComponentModel.DataAnnotations;

namespace DragonSushiSystem.Models
{
    public class Pedido
    {
        public int PedidoID { get; set; }

        public int FK_ComandaId { get; set; }

        public int FK_ProdutoId { get; set; }

        public int FK_PratoId { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        public int PedidoQuantidade { get; set; }

        [Display(Name = "Subtotal")]
        public decimal PedidoSubtotal { get; set; }
    }
}
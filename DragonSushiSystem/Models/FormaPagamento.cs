using System.ComponentModel.DataAnnotations;

namespace DragonSushiSystem.Models
{
    public class FormaPagamento
    {
        [Key]
        public int FormaPagamentoID { get; set; }

        public string FormaPagamentoNome { get; set; }
    }
}
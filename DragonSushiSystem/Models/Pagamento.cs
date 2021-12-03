using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DragonSushiSystem.Models
{
    public class Pagamento
    {
        public string FormaPagamento { get; set; }

        public string CPF { get; set; }

        public decimal ValorPago { get; set; }

        public int Comanda { get; private set; }

        public DateTime Data { get; set; }
    }
}
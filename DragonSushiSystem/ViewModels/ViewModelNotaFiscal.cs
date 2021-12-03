using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonSushiSystem.Models
{
    public class ViewModelNotaFiscal
    {
        public FormaPagamento FormaPagamento { get; set; }

        public Comanda Comanda { get; set; }

        public Pagamento Pagamento { get; set; }

        public string CPF { get; set; }

        public decimal ValorPago { get; set; }

        //public ViewModelNotaFiscal ValorTotal(int id)
        //{
        //    Conexao conexao = new Conexao();
        //    string valortotalQuery = String.Format("SELECT * FROM vwSomarSubtotal WHERE ComandaID = {0}", id);
        //    MySqlCommand command = new MySqlCommand(valortotalQuery, conexao.ConectarBD());
        //    var dados = command.ExecuteReader();
        //    return dadosReaderParaList(dados).FirstOrDefault();
        //}

        //public decimal ValorRestante()
        //{
        //    decimal valortotal = ValorTotal();
        //    decimal valorrestante = valortotal - ValorPago;
        //    return valorrestante;
        //}

        public void gerarNotaFiscal(ViewModelNotaFiscal vwNotaFiscal)
        {
            Conexao conexao = new Conexao();

            string insertQuery = String.Format("CALL spNotaFiscal(@ValorPago,@Data,@CPF,@FormaPagamento,@Comanda)");
            MySqlCommand command = new MySqlCommand(insertQuery, conexao.ConectarBD());
            command.Parameters.Add("@ValorPago", MySqlDbType.VarChar).Value = vwNotaFiscal.Pagamento.ValorPago;
            command.Parameters.Add("@Data", MySqlDbType.VarChar).Value = vwNotaFiscal.Pagamento.Data;
            command.Parameters.Add("@CPF", MySqlDbType.Decimal).Value = vwNotaFiscal.Pagamento.CPF;
            command.Parameters.Add("@QuantidadeProduto", MySqlDbType.Int32).Value = vwNotaFiscal.FormaPagamento.FormaPagamentoNome;
            command.Parameters.Add("@Comanda", MySqlDbType.Int32).Value = vwNotaFiscal.Comanda.ComandaID;

            command.ExecuteNonQuery();
            conexao.DesconectarBD();
        }

        public List<ViewModelNotaFiscal> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaPagamentos = new List<ViewModelNotaFiscal>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {

                    var pagamento = new ViewModelNotaFiscal()
                    {
                        Pagamento = new Pagamento()
                        {
                            ValorPago = Conexao.ConverteDecimal(dados["ValorPago"]),
                            Data = DateTime.Now,
                            CPF = Conexao.ConverteString(dados["Cpf"])
                        },

                        FormaPagamento = new FormaPagamento()
                        {
                            FormaPagamentoNome = Conexao.ConverteString(dados["FormaPagamento"])
                        },

                        Comanda = new Comanda()
                        {
                            ComandaID = Conexao.ConverteInt32(dados["ComandaID"])
                        }
                    };
                    listaPagamentos.Add(pagamento);
                }
            }
            dados.Close();
            return listaPagamentos;
        }
    }
}
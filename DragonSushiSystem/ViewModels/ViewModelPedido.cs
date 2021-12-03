using DragonSushiSystem.Banco;
using DragonSushiSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushiSystem.ViewModels
{
    public class ViewModelPedido
    {
        public Pedido Pedido { get; set; }

        public Comanda Comanda { get; set; }
        
        public Produto Produto { get; set; }

        public Prato Prato { get; set; }

        public void cadastrarPedido(ViewModelPedido vmPedido)
        {
            Conexao conexao = new Conexao();
            string insertQuery = String.Format("CALL spInsertPedidoComanda(@Comanda,@Produto,@Prato,@Quantidade)");

            MySqlCommand command = new MySqlCommand(insertQuery, conexao.ConectarBD());
            command.Parameters.Add("@Comanda", MySqlDbType.Int32).Value = vmPedido.Comanda.ComandaID;
            command.Parameters.Add("@Produto", MySqlDbType.Int32).Value = vmPedido.Produto.ProdutoID;
            command.Parameters.Add("@Prato", MySqlDbType.Int32).Value = vmPedido.Prato.PratoID;
            command.Parameters.Add("@Quantidade", MySqlDbType.Int32).Value = vmPedido.Pedido.PedidoQuantidade;

            command.ExecuteNonQuery();
            conexao.DesconectarBD();
        }

        public void editarPedido(ViewModelPedido vmPedido)
        {
            Conexao conexao = new Conexao();
            string updateQuery = String.Format("CALL spInsertPedidoComanda(@Comanda,@Produto,@Prato,@Quantidade)");

            MySqlCommand command = new MySqlCommand(updateQuery, conexao.ConectarBD());
            command.Parameters.Add("@Comanda", MySqlDbType.Int32).Value = vmPedido.Comanda.ComandaID;
            command.Parameters.Add("@Produto", MySqlDbType.Int32).Value = vmPedido.Produto.ProdutoID;
            command.Parameters.Add("@Prato", MySqlDbType.Int32).Value = vmPedido.Prato.PratoID;
            command.Parameters.Add("@Quantidade", MySqlDbType.Int32).Value = vmPedido.Pedido.PedidoQuantidade;

            command.ExecuteNonQuery();
            conexao.DesconectarBD();

        }

        public List<ViewModelPedido> listarTodosPedidos(int id)
        {
            Conexao conexao = new Conexao();
            string selectQuery = String.Format("SELECT * FROM vwExibirPedidoComanda WHERE FK_ComandaID = {0}", id);
            MySqlCommand selectCommand = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();

            return dadosReaderParaList(dados);
        }

        public ViewModelPedido listarPedidoPorID(int id)
        {
            Conexao conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM vwExibirPedidoComanda WHERE PedidoID = {0}", id);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<ViewModelPedido> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaPedidos = new List<ViewModelPedido>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {

                    var vmPedido = new ViewModelPedido()
                    {
                        Pedido = new Pedido()
                        {
                            PedidoID = Conexao.ConverteInt32(dados["PedidoID"]),
                            PedidoSubtotal = Conexao.ConverteDecimal(dados["PedidoSubtotal"]),
                            PedidoQuantidade = Conexao.ConverteInt32(dados["PedidoQuantidade"])
                        },

                        Comanda = new Comanda()
                        {
                            ComandaID = Conexao.ConverteInt32(dados["FK_ComandaID"])
                        },

                        Prato = new Prato()
                        {
                            PratoID = Conexao.ConverteInt32(dados["FK_PratoID"]),
                            PratoNome = Conexao.ConverteString(dados["PratoNome"]),
                            PratoPreco = Conexao.ConverteDecimal(dados["PratoPreco"])
                        },

                        Produto = new Produto()
                        {
                            ProdutoID = Conexao.ConverteInt32(dados["FK_ProdutoID"]),
                            ProdutoNome = Conexao.ConverteString(dados["ProdutoNome"]),
                            ProdutoPreco = Conexao.ConverteDecimal(dados["ProdutoPreco"])
                        }
                    };
                    listaPedidos.Add(vmPedido);
                }
            }
            dados.Close();
            return listaPedidos;
        }
    }
}
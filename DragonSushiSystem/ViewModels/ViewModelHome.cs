using DragonSushiSystem.Banco;
using DragonSushiSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace DragonSushiSystem.ViewModels
{
    public class ViewModelHome
    {
        public List<Produto> listaProduto { get; set; }

        public Reserva Reserva { get; set; }

        public Produto Produto { get; set; }


        public List<ViewModelHome> listarEstoque()
        {
            Conexao conexao = new Conexao();

            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM vwEstoque", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();

            return dadosReaderParaList(dados);
        }

        private List<ViewModelHome> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaProdutos = new List<ViewModelHome>();
            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var vwProdutos = new ViewModelHome()
                    {
                        Produto = new Produto()
                        {
                            ProdutoID = Conexao.ConverteInt32(dados["ProdutoID"]),
                            ProdutoNome = Conexao.ConverteString(dados["ProdutoNome"]),
                            ProdutoQuantidade = Conexao.ConverteInt32(dados["ProdutoQuantidade"])
                        }
                    };
                    listaProdutos.Add(vwProdutos);
                }
            }
            dados.Close();
            return listaProdutos;
        }
    }
}
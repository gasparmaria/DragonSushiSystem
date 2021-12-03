using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonSushiSystem.Models
{
    public class Endereco
    {
        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public int FK_EstadoID { get; set; }

        public int FK_CidadeID { get; set; }

        public int FK_BairroID { get; set; }

        public List<Endereco> mostrarTodosEnderecos()
        {
            Conexao conexao = new Conexao();
            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM tbEndereco", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();
            return dadosReaderParaList(dados);
        }

        public Endereco mostrarEnderecoPorID(int code)
        {
            var conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM tbEndereco WHERE Cep = {0}", code);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<Endereco> dadosReaderParaList(MySqlDataReader dados)
        {
            var enderecos = new List<Endereco>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var endereco = new Endereco()
                    {
                        Cep = dados["Cep"].ToString(),
                        Logradouro = dados["Logradouro"].ToString(),
                        FK_EstadoID = ushort.Parse(dados["FK_EstadoID"].ToString()),
                        FK_CidadeID = ushort.Parse(dados["FK_CidadeID"].ToString()),
                        FK_BairroID = ushort.Parse(dados["FK_BairroID"].ToString())
                    };

                    enderecos.Add(endereco);
                }
                dados.Close();
            }
            return enderecos;
        }
    }
}
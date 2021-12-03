using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DragonSushiSystem.Models
{
    public class Cidade
    {

        public int CidadeID { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string CidadeNome { get; set; }

        public List<Cidade> mostrarTodasCidades()
        {
            Conexao conexao = new Conexao();
            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM tbCidade", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();
            return dadosReaderParaList(dados);
        }

        public Cidade mostrarCidadePorID(int code)
        {
            var conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM tbCidade WHERE CidadeID = {0}", code);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<Cidade> dadosReaderParaList(MySqlDataReader dados)
        {
            var Cidades = new List<Cidade>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var Cidade = new Cidade()
                    {
                        CidadeID = ushort.Parse(dados["CidadeID"].ToString()),
                        CidadeNome = dados["CidadeNome"].ToString()
                    };

                    Cidades.Add(Cidade);
                }
                dados.Close();
            }
            return Cidades;
        }
    }
}
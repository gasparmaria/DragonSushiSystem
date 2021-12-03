using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DragonSushiSystem.Models
{
    public class Bairro
    {
        public int BairroID { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "o bairro é obrigatório.")]
        public string BairroNome { get; set; }

        public List<Bairro> mostrarTodosBairros()
        {
            Conexao conexao = new Conexao();
            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM tbBairro", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();
            return dadosReaderParaList(dados);
        }

        public Bairro mostrarBairroPorID(int code)
        {
            var conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM tbBairro WHERE BairroID = {0}", code);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<Bairro> dadosReaderParaList(MySqlDataReader dados)
        {
            var bairros = new List<Bairro>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var bairro = new Bairro()
                    {
                        BairroID = ushort.Parse(dados["BairroID"].ToString()),
                        BairroNome = dados["BairroNome"].ToString()
                    };

                    bairros.Add(bairro);
                }
                dados.Close();
            }
            return bairros;
        }
    }
}
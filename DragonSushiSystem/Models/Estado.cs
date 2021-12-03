using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DragonSushiSystem.Models
{
    public class Estado
    {
        public int EstadoID { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O estado é obrigatório.")]
        public string UF { get; set; }

        public List<Estado> mostrarTodosEstados()
        {
            Conexao conexao = new Conexao();
            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM tbEstado", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();
            return dadosReaderParaList(dados);
        }

        public Estado mostrarEstadoPorID(int code)
        {
            var conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM tbEstado WHERE EstadoID = {0}", code);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<Estado> dadosReaderParaList(MySqlDataReader dados)
        {
            var estados = new List<Estado>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var estado = new Estado()
                    {
                        EstadoID = Conexao.ConverteInt32(dados["EstadoID"]),
                        UF = Conexao.ConverteString(dados["UF"])
                    };

                    estados.Add(estado);
                }
                dados.Close();
            }
            return estados;
        }
    }
}
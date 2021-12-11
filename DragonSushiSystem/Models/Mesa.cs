using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DragonSushiSystem.Models
{
    public class Mesa
    {
        public int? MesaID { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public bool MesaStatus { get; set; }

        [Required(ErrorMessage = "A capacidade é obrigatória.")]
        public int MesaCapacidade { get; set; }

        public List<Mesa> listarTodasMesas()
        {
            Conexao conexao = new Conexao();

            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM tbMesa", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();

            return dadosReaderParaList(dados);
        }

        public Mesa listarMesaPorID(int id)
        {
            Conexao conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM tbMesa WHERE MesaID = {0}", id);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<Mesa> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaMesas = new List<Mesa>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var mesa = new Mesa()
                    {
                        MesaID = Conexao.ConverteInt32(dados["MesaID"]),
                        MesaCapacidade = Conexao.ConverteInt32(dados["MesaCapacidade"]),
                        MesaStatus = Conexao.ConverteBoolean(dados["MesaStatus"])
                    };
                    listaMesas.Add(mesa);
                }
            }
            dados.Close();
            return listaMesas;
        }
    }
}
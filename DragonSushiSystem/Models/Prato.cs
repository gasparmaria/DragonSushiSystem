using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DragonSushiSystem.Models
{
    public class Prato
    {
        [Key]
        public int PratoID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string PratoNome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string PratoDescricao { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O preço é obrigatório.")]
        public decimal PratoPreco { get; set; }

        

        public void cadastrarPrato(Prato prato)
        {
            Conexao conexao = new Conexao();
            MySqlCommand command = new MySqlCommand("INSERT INTO tbPrato VALUES(DEFAULT,@NomePrato,@DescricaoPrato,@PrecoPrato)", conexao.ConectarBD());
            command.Parameters.Add("@NomePrato", MySqlDbType.VarChar).Value = prato.PratoNome;
            command.Parameters.Add("@DescricaoPrato", MySqlDbType.VarChar).Value = prato.PratoDescricao;
            command.Parameters.Add("@PrecoPrato", MySqlDbType.Decimal).Value = prato.PratoPreco;

            command.ExecuteNonQuery();
            conexao.DesconectarBD();
        }

        public void editarPrato(Prato prato)
        {
            Conexao conexao = new Conexao();
            string updateQuery = "UPDATE tbPrato SET PratoNome = @NomePrato, PratoDescricao = @DescricaoPrato, PratoPreco = @PrecoPrato WHERE PratoID = @PratoID";
            MySqlCommand command = new MySqlCommand(updateQuery, conexao.ConectarBD());
            command.Parameters.Add("@NomePrato", MySqlDbType.VarChar).Value = prato.PratoNome;
            command.Parameters.Add("@DescricaoPrato", MySqlDbType.VarChar).Value = prato.PratoDescricao;
            command.Parameters.Add("@PrecoPrato", MySqlDbType.Decimal).Value = prato.PratoPreco;
            command.Parameters.Add("@PratoID", MySqlDbType.Int32).Value = prato.PratoID;

            command.ExecuteNonQuery();
            conexao.DesconectarBD();
        }

        public void deletarPrato(Prato prato)
        {
            Conexao conexao = new Conexao();
            string deleteQuery = "DELETE FROM tbPrato WHERE PratoID = @PratoID";

            MySqlCommand command = new MySqlCommand(deleteQuery, conexao.ConectarBD());
            command.Parameters.Add("@PratoID", MySqlDbType.Int32).Value = prato.PratoID;

            command.ExecuteNonQuery();
            conexao.DesconectarBD();
        }


        public List<Prato> listarTodosPratos()
        {
            Conexao conexao = new Conexao();

            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM tbPrato", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();

            return dadosReaderParaList(dados);
        }

        public Prato listarPratoPorID(int id)
        {
            Conexao conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM tbPrato WHERE PratoID = {0}", id);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<Prato> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaPratos = new List<Prato>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {

                    var Prato = new Prato()
                    {
                        PratoID = Conexao.ConverteInt32(dados["PratoID"]),
                        PratoNome = Conexao.ConverteString(dados["PratoNome"]),
                        PratoDescricao = Conexao.ConverteString(dados["PratoDescricao"]),
                        PratoPreco = Conexao.ConverteDecimal(dados["PratoPreco"])
                    };
                    listaPratos.Add(Prato);
                }
            }
            dados.Close();
            return listaPratos;
        }

    }
}
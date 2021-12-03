using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public decimal PratoPreco { get; set; }

        Conexao conexao = new Conexao();

        public void CadastrarPrato(Prato prato)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO tbPrato VALUES(DEFAULT,@NomePrato,@DescricaoPrato,@PrecoPrato)", conexao.ConectarBD());
            command.Parameters.Add("@NomePrato", MySqlDbType.VarChar).Value = prato.PratoNome;
            command.Parameters.Add("@DescricaoPrato", MySqlDbType.VarChar).Value = prato.PratoDescricao;
            command.Parameters.Add("@PrecoPrato", MySqlDbType.Decimal).Value = prato.PratoPreco;
        }

        public void EditarPrato(Prato prato)
        {
            string updateQuery = String.Format("UPDATE tbPrato SET PratoNome = @NomePrato, PratoDescricao = @DescricaoPrato, PratoPreco = @PrecoPrato WHERE PratoID = @PratoID");
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
    }
}
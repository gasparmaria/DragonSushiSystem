using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DragonSushiSystem.Models
{
    public class Cliente
    {
        public int? ClienteID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string ClienteNome { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O número de endereço é obrigatório.")]
        public int? ClienteNumeroEndereco { get; set; }

        [Display(Name = "Complemento")]
        public string ClienteComplemento { get; set; }

        [Display(Name = "Telefone")]
        [RegularExpression(@"^\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$", ErrorMessage = "Informe um número válido.")]
        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string ClienteTelefone { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string FK_Cep { get; set; }


        public List<Cliente> listarTodosClientes()
        {
            Conexao conexao = new Conexao();

            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM tbCliente", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();

            return dadosReaderParaList(dados);
        }

        public Cliente listarClientePorID(int id)
        {
            Conexao conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM tbCliente WHERE ClienteID = {0}", id);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<Cliente> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaClientes = new List<Cliente>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var cliente = new Cliente()
                    {
                        ClienteID = Conexao.ConverteInt32(dados["ClienteID"]),
                        ClienteNome = Conexao.ConverteString(dados["ClienteNome"]),
                        ClienteNumeroEndereco = Conexao.ConverteInt32(dados["ClienteNumeroEndereco"]),
                        ClienteComplemento = Conexao.ConverteString(dados["ClienteComplemento"]),
                        ClienteTelefone = Conexao.ConverteString(dados["ClienteTelefone"]),
                        FK_Cep = Conexao.ConverteString(dados["Fk_Cep"])
                    };
                    listaClientes.Add(cliente);
                }
            }
            dados.Close();
            return listaClientes;
        }
    }
}
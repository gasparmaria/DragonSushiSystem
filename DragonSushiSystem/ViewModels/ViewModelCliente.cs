using DragonSushiSystem.Banco;
using DragonSushiSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonSushiSystem.ViewModels
{
    public class ViewModelCliente
    {
        public Cliente Cliente { get; set; }

        public Endereco Endereco { get; set; }

        public Estado Estado { get; set; }

        public Cidade Cidade { get; set; }

        public Bairro Bairro { get; set; }

        public void cadastrarCliente(ViewModelCliente vmCliente)
        {
            Conexao conexao = new Conexao();

            string insertQuery = String.Format("CALL spInsertCliente(@NomeBairro,@NomeCidade,@UF,@Cep,@Logradouro,@NumeroEndereco,@Complemento,@Telefone,@Nome)");
            MySqlCommand command = new MySqlCommand(insertQuery, conexao.ConectarBD());
            command.Parameters.Add("@NomeBairro", MySqlDbType.VarChar).Value = vmCliente.Bairro.BairroNome;
            command.Parameters.Add("@NomeCidade", MySqlDbType.VarChar).Value = vmCliente.Cidade.CidadeNome;
            command.Parameters.Add("@UF", MySqlDbType.VarChar).Value = vmCliente.Estado.UF;
            command.Parameters.Add("@Cep", MySqlDbType.VarChar).Value = vmCliente.Endereco.Cep;
            command.Parameters.Add("@Logradouro", MySqlDbType.VarChar).Value = vmCliente.Endereco.Logradouro;
            command.Parameters.Add("@NumeroEndereco", MySqlDbType.Int32).Value = vmCliente.Cliente.ClienteNumeroEndereco;
            command.Parameters.Add("@Complemento", MySqlDbType.VarChar).Value = vmCliente.Cliente.ClienteComplemento;
            command.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = vmCliente.Cliente.ClienteTelefone;
            command.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = vmCliente.Cliente.ClienteNome;

            command.ExecuteNonQuery();
            conexao.DesconectarBD();
        }

        public void editarCliente(ViewModelCliente vmCliente)
        {
            Conexao conexao = new Conexao();
            string updateQuery = String.Format("CALL spUpdateCliente(@NomeBairro,@NomeCidade,@UF,@Cep,@Logradouro,@NumeroEndereco,@Complemento,@Telefone,@Nome,@ClienteID)");

            MySqlCommand command = new MySqlCommand(updateQuery, conexao.ConectarBD());
            command.Parameters.Add("@NomeBairro", MySqlDbType.VarChar).Value = vmCliente.Bairro.BairroNome;
            command.Parameters.Add("@NomeCidade", MySqlDbType.VarChar).Value = vmCliente.Cidade.CidadeNome;
            command.Parameters.Add("@UF", MySqlDbType.VarChar).Value = vmCliente.Estado.UF;
            command.Parameters.Add("@Cep", MySqlDbType.VarChar).Value = vmCliente.Endereco.Cep;
            command.Parameters.Add("@Logradouro", MySqlDbType.VarChar).Value = vmCliente.Endereco.Logradouro;
            command.Parameters.Add("@NumeroEndereco", MySqlDbType.Int32).Value = vmCliente.Cliente.ClienteNumeroEndereco;
            command.Parameters.Add("@Complemento", MySqlDbType.VarChar).Value = vmCliente.Cliente.ClienteComplemento;
            command.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = vmCliente.Cliente.ClienteTelefone;
            command.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = vmCliente.Cliente.ClienteNome;
            command.Parameters.Add("@ClienteID", MySqlDbType.VarChar).Value = vmCliente.Cliente.ClienteID;

            try
            {
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conexao.DesconectarBD();

            }
        }

        public List<ViewModelCliente> listarTodosClientes()
        {
            Conexao conexao = new Conexao();

            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM vwExibirCliente", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();

            return dadosReaderParaList(dados);
        }

        public ViewModelCliente listarClientePorID(int id)
        {
            Conexao conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM vwExibirCliente WHERE ClienteID = {0}", id);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<ViewModelCliente> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaClientes = new List<ViewModelCliente>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {

                    var VWCliente = new ViewModelCliente()
                    {
                        Cliente = new Cliente()
                        {
                            ClienteID = ushort.Parse(dados["ClienteID"].ToString()),
                            ClienteNome = dados["ClienteNome"].ToString(),
                            ClienteNumeroEndereco = ushort.Parse(dados["ClienteNumeroEndereco"].ToString()),
                            ClienteComplemento = dados["ClienteComplemento"].ToString(),
                            ClienteTelefone = dados["ClienteTelefone"].ToString()
                        },

                        Endereco = new Endereco()
                        {
                            Cep = dados["Cep"].ToString(),
                            Logradouro = dados["Logradouro"].ToString()
                        },

                        Bairro = new Bairro()
                        {
                            BairroNome = dados["BairroNome"].ToString()
                        },

                        Cidade = new Cidade()
                        {
                            CidadeNome = dados["CidadeNome"].ToString()
                        },

                        Estado = new Estado()
                        {
                            UF = dados["UF"].ToString()
                        }
                    };
                    listaClientes.Add(VWCliente);
                }
            }
            dados.Close();
            return listaClientes;
        }
    }
}
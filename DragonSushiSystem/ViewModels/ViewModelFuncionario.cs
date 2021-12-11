using DragonSushiSystem.Banco;
using DragonSushiSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonSushiSystem.ViewModels
{
    public class ViewModelFuncionario
    {
        public Funcionario Funcionario { get; set; }

        public Endereco Endereco { get; set; }

        public Estado Estado { get; set; }

        public Cidade Cidade { get; set; }

        public Bairro Bairro { get; set; }

        public Cargo Cargo { get; set; }

        public List<Estado> ListaEstado { get; set; }

        public void cadastrarFuncionario(ViewModelFuncionario vmFuncionario)
        {
            Conexao conexao = new Conexao();
            string insertQuery = "CALL spInsertFuncionario(@BairroNome,@CidadeNome,@EstadoUF,@Cep,@Logradouro,@NumeroEndereco,@ComplementoEndereco,@FuncionarioFotoPerfil,@FuncionarioTelefone,@FuncionarioCPF,@FuncionarioNome,@CargoNome,@FuncionarioSenha)";

            MySqlCommand command = new MySqlCommand(insertQuery, conexao.ConectarBD());
            command.Parameters.Add("@BairroNome", MySqlDbType.VarChar).Value = vmFuncionario.Bairro.BairroNome;
            command.Parameters.Add("@CidadeNome", MySqlDbType.VarChar).Value = vmFuncionario.Cidade.CidadeNome;
            command.Parameters.Add("@EstadoUF", MySqlDbType.VarChar).Value = vmFuncionario.Estado.UF;
            command.Parameters.Add("@Cep", MySqlDbType.VarChar).Value = vmFuncionario.Endereco.Cep;
            command.Parameters.Add("@Logradouro", MySqlDbType.VarChar).Value = vmFuncionario.Endereco.Logradouro;
            command.Parameters.Add("@NumeroEndereco", MySqlDbType.Int32).Value = vmFuncionario.Funcionario.FuncionarioNumeroEndereco;
            command.Parameters.Add("@ComplementoEndereco", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioComplemento;
            command.Parameters.Add("@FuncionarioFotoPerfil", MySqlDbType.Binary).Value = vmFuncionario.Funcionario.FuncionarioFotoPerfil;
            command.Parameters.Add("@FuncionarioTelefone", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioTelefone;
            command.Parameters.Add("@FuncionarioCPF", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioCPF;
            command.Parameters.Add("@FuncionarioNome", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioNome;
            command.Parameters.Add("@CargoNome", MySqlDbType.VarChar).Value = vmFuncionario.Cargo.CargoNome;
            command.Parameters.Add("@FuncionarioSenha", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioSenha;

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

        public void editarFuncionario(ViewModelFuncionario vmFuncionario)
        {
            Conexao conexao = new Conexao();
            string updateQuery = String.Format("CALL spUpdateFuncionario(" +
                                                                          "@NomeBairro," +
                                                                          "@NomeCidade," +
                                                                          "@UF," +
                                                                          "@Cep," +
                                                                          "@Logradouro," +
                                                                          "@NumeroEndereco," +
                                                                          "@Complemento," +
                                                                          "@FotoPerfil," +
                                                                          "@Telefone," +
                                                                          "@Cpf," +
                                                                          "@Nome," +
                                                                          "@Cargo," +
                                                                          "@Senha," +
                                                                          "@FuncionarioID" + ")");

            MySqlCommand command = new MySqlCommand(updateQuery, conexao.ConectarBD());
            command.Parameters.Add("@NomeBairro", MySqlDbType.VarChar).Value = vmFuncionario.Bairro.BairroNome;
            command.Parameters.Add("@NomeCidade", MySqlDbType.VarChar).Value = vmFuncionario.Cidade.CidadeNome;
            command.Parameters.Add("@UF", MySqlDbType.VarChar).Value = vmFuncionario.Estado.UF;
            command.Parameters.Add("@Cep", MySqlDbType.VarChar).Value = vmFuncionario.Endereco.Cep;
            command.Parameters.Add("@Logradouro", MySqlDbType.VarChar).Value = vmFuncionario.Endereco.Logradouro;
            command.Parameters.Add("@NumeroEndereco", MySqlDbType.Int32).Value = vmFuncionario.Funcionario.FuncionarioNumeroEndereco;
            command.Parameters.Add("@Complemento", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioComplemento;
            command.Parameters.Add("@FotoPerfil", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioFotoPerfil;
            command.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioTelefone;
            command.Parameters.Add("@Cpf", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioCPF;
            command.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioNome;
            command.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = vmFuncionario.Cargo.CargoNome;
            command.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = vmFuncionario.Funcionario.FuncionarioSenha;
            command.Parameters.Add("@FuncionarioID", MySqlDbType.Int32).Value = vmFuncionario.Funcionario.FuncionarioID;

            command.ExecuteNonQuery();
            conexao.DesconectarBD();
        }

        public void deletarFuncionario(int id)
        {
            Conexao conexao = new Conexao();
            string deleteQuery = String.Format("CALL spDeleteFuncionario({0});", id);

            MySqlCommand command = new MySqlCommand(deleteQuery, conexao.ConectarBD());

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

        public List<ViewModelFuncionario> listarTodosFuncionarios()
        {
            Conexao conexao = new Conexao();

            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM vwExibirFuncionarios", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();

            return dadosReaderParaList(dados);
        }

        public ViewModelFuncionario listarFuncionarioPorID(int id)
        {
            Conexao conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM vwExibirFuncionarios WHERE FuncionarioID = {0}", id);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<ViewModelFuncionario> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaFuncionarios = new List<ViewModelFuncionario>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {

                    var VWFuncionario = new ViewModelFuncionario()
                    {
                        Funcionario = new Funcionario()
                        {
                            FuncionarioID = ushort.Parse(dados["FuncionarioID"].ToString()),
                            FuncionarioNome = dados["FuncionarioNome"].ToString(),
                            FuncionarioSenha = dados["FuncionarioSenha"].ToString(),
                            FuncionarioCPF = dados["FuncionarioCPF"].ToString(),
                            FuncionarioNumeroEndereco = ushort.Parse(dados["FuncionarioNumeroEndereco"].ToString()),
                            FuncionarioComplemento = dados["FuncionarioComplemento"].ToString(),
                            FuncionarioFotoPerfil = Convert.FromBase64String(dados["FuncionarioFotoPerfil"].ToString()),
                            FuncionarioTelefone = dados["FuncionarioTelefone"].ToString()
                        },

                        Cargo = new Cargo()
                        {
                            CargoNome = dados["CargoNome"].ToString()
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
                    listaFuncionarios.Add(VWFuncionario);
                }
            }
            dados.Close();
            return listaFuncionarios;
        }
    }
}
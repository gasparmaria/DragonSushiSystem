using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DragonSushiSystem.Models
{
    public class Funcionario
    {
        public int FuncionarioID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string FuncionarioNome { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string FuncionarioSenha { get; set; }

        [Display(Name = "CPF")]
        [RegularExpression(@"([0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2})|([0-9]{11})", ErrorMessage = "Informe um CPF válido.")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string FuncionarioCPF { get; set; }

        [Display(Name = "Número")]
        [Range(1, int.MaxValue, ErrorMessage = "Insira um número de endereço válido.")]
        [Required(ErrorMessage = "O número de endereço é obrigatório.")]
        public int FuncionarioNumeroEndereco { get; set; }

        [Display(Name = "Complemento")]
        public string FuncionarioComplemento { get; set; }

        [Display(Name = "Foto de perfil")]
        public byte[] FuncionarioFotoPerfil { get; set; }

        [Display(Name = "Telefone")]
        [RegularExpression(@"^\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$", ErrorMessage = "Informe um número válido.")]
        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string FuncionarioTelefone { get; set; }

        [Display(Name = "Cargo")]
        public int FK_CargoID { get; set; }

        [Display(Name = "CEP")]
        public string FK_Cep { get; set; }

        public Funcionario TestarUsuario(Funcionario funcionario)
        {
            Conexao conexao = new Conexao();

            string loginQuery = string.Format("SELECT * FROM tbFuncionario WHERE FuncionarioCPF = @CPF AND FuncionarioSenha = @Senha");
            MySqlCommand command = new MySqlCommand(loginQuery, conexao.ConectarBD());
            command.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = funcionario.FuncionarioCPF;
            command.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = funcionario.FuncionarioSenha;

            MySqlDataReader dados = command.ExecuteReader();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    funcionario.FuncionarioCPF = Conexao.ConverteString(dados["FuncionarioCPF"]);
                    funcionario.FuncionarioSenha = Conexao.ConverteString(dados["FuncionarioSenha"]);
                    funcionario.FuncionarioID = Conexao.ConverteInt32(dados["FuncionarioID"]);
                }
            }
            else
            {
                funcionario.FuncionarioCPF = null;
                funcionario.FuncionarioSenha = null;
            }
            return funcionario;
        }

        public Funcionario mudarSenha(Funcionario funcionario, String novaSenha)
        {
            Conexao conexao = new Conexao();

            MySqlCommand updateQuery = new MySqlCommand("UPDATE tbFuncionario SET FuncionarioSenha = @NovaSenha WHERE FuncionarioID = @ID", conexao.ConectarBD());
            updateQuery.Parameters.Add("@ID", MySqlDbType.VarChar).Value = funcionario.FuncionarioID;
            updateQuery.Parameters.Add("@NovaSenha", MySqlDbType.VarChar).Value = novaSenha;

            updateQuery.ExecuteNonQuery();
            conexao.DesconectarBD();

            return funcionario;
        }


        public Funcionario listarFuncionarioPorID(int id)
        {
            Conexao conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM vwExibirFuncionarios WHERE FuncionarioID = {0}", id);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<Funcionario> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaFuncionarios = new List<Funcionario>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var funcionario = new Funcionario()
                    {
                            FuncionarioID = ushort.Parse(dados["FuncionarioID"].ToString()),
                            FuncionarioNome = dados["FuncionarioNome"].ToString(),
                            FuncionarioSenha = dados["FuncionarioSenha"].ToString(),
                            FuncionarioCPF = dados["FuncionarioCPF"].ToString(),
                            FuncionarioNumeroEndereco = ushort.Parse(dados["FuncionarioNumeroEndereco"].ToString()),
                            FuncionarioComplemento = dados["FuncionarioComplemento"].ToString(),
                            FuncionarioFotoPerfil = Convert.FromBase64String(dados["FuncionarioFotoPerfil"].ToString()),
                            FuncionarioTelefone = dados["FuncionarioTelefone"].ToString()
                    };
                    listaFuncionarios.Add(funcionario);
                }
            }
            dados.Close();
            return listaFuncionarios;
        }
    }
}
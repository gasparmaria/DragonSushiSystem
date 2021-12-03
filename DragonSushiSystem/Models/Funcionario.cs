using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;

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

        public string NovaSenha { get; set; }

        public string ConfirmacaoNovaSenha { get; set; }

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
                    Funcionario dto = new Funcionario();
                    {
                        dto.FuncionarioCPF = Conexao.ConverteString(dados["FuncionarioCPF"]);
                        dto.FuncionarioSenha = Conexao.ConverteString(dados["FuncionarioSenha"]);
                    }
                }
            }
            else
            {
                funcionario.FuncionarioCPF = null;
                funcionario.FuncionarioSenha = null;
            }
            return funcionario;
        }

        public Funcionario MudarSenha(Funcionario funcionario)
        {
            Conexao conexao = new Conexao();

            MySqlCommand verificacaoQuery = new MySqlCommand("SELECT * FROM tbFuncionario WHERE FuncionarioCPF = @CPF AND FuncionarioSenha = @Senha", conexao.ConectarBD());       
            MySqlDataReader dados = verificacaoQuery.ExecuteReader();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    Funcionario dto = new Funcionario();
                    {
                        dto.FuncionarioCPF = Conexao.ConverteString(dados["FuncionarioCPF"]);
                        dto.FuncionarioSenha = Conexao.ConverteString(dados["FuncionarioSenha"]);
                    }
                }
            }
            else
            {
                funcionario.FuncionarioCPF = null;
                funcionario.FuncionarioSenha = null;
            }

            if(NovaSenha == ConfirmacaoNovaSenha)
            {
                MySqlCommand updateQuery = new MySqlCommand("UPDATE FROM tbFuncionario SET FuncionarioSenha = @NovaSenha WHERE FuncionarioCPF = @CPF AND FuncionarioSenha = @Senha");
                updateQuery.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = funcionario.FuncionarioCPF;
                updateQuery.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = funcionario.FuncionarioSenha;
                updateQuery.Parameters.Add("NovaSenha", MySqlDbType.VarChar).Value = funcionario.NovaSenha;
            }       
            return funcionario;
        }
    }
}
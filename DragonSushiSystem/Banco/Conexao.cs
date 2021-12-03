using MySql.Data.MySqlClient;
using System;

namespace DragonSushiSystem.Banco
{
    public class Conexao
    {
        MySqlConnection conexao = new MySqlConnection("Server=localhost;DataBase=dbDragonSushi;user=westen;pwd=westen;SslMode=none;");
        public static string mensagem;

        public MySqlConnection ConectarBD()
        {
            try
            {
                conexao.Open();
            }
            catch (Exception erro)
            {
                mensagem = "Erro ao conectar-se." + erro.Message;
            }
            return conexao;
        }

        public MySqlConnection DesconectarBD()
        {
            try
            {
                conexao.Close();
            }
            catch (Exception erro)
            {
                mensagem = "Erro ao desconectar-se." + erro.Message;
            }
            return conexao;
        }

        public static string ConverteString(object Valor)
        {
            string newValor;
            try
            {
                newValor = (string)Valor;
            }
            catch (Exception)
            {
                return "";
            }

            return newValor;
        }

        public static Int32 ConverteInt32(object Valor)
        {
            Int32 newValor;
            try
            {
                newValor = Convert.ToInt32(Valor);
            }
            catch (Exception)
            {
                return 0;
            }

            return newValor;
        }   

        public static Decimal ConverteDecimal(object Valor)
        {
            Decimal newValor;
            try
            {
                newValor = Convert.ToDecimal(Valor);
            }
            catch (Exception)
            {
                return 0;
            }

            return newValor;
        }

        public static Boolean ConverteBoolean(object Valor)
        {
            Boolean newValor;
            try
            {
                newValor = Convert.ToBoolean(Valor);
            }
            catch (Exception)
            {
                return false;
            }

            return newValor;
        }
    }
}

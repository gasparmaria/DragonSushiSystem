using DragonSushiSystem.Banco;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DragonSushiSystem.Models
{
    public class Produto
    {
        public int ProdutoID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string ProdutoNome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string ProdutoDescricao { get; set; }

        public string ProdutoDescricaoShortened { get { return ProdutoDescricao.ToString().SubStringTo(40); } }

        [Display(Name = "Preço")]
        public decimal? ProdutoPreco { get; set; }

        [Display(Name = "Ingrediente")]
        [Required(ErrorMessage = "O ingrediente é obrigatório.")]
        public bool ProdutoIngrediente { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        public int ProdutoQuantidade { get; set; }


        public void cadastrarProduto(Produto produto)
        {
            Conexao conexao = new Conexao();
            MySqlCommand command = new MySqlCommand("INSERT INTO tbProduto VALUES(DEFAULT,@NomeProduto,@DescricaoProduto,@PrecoProduto,@QuantidadeProduto,@IngredienteProduto)", conexao.ConectarBD());
            command.Parameters.Add("@NomeProduto", MySqlDbType.VarChar).Value = produto.ProdutoNome;
            command.Parameters.Add("@DescricaoProduto", MySqlDbType.VarChar).Value = produto.ProdutoDescricao;
            command.Parameters.Add("@PrecoProduto", MySqlDbType.Decimal).Value = produto.ProdutoPreco;
            command.Parameters.Add("@QuantidadeProduto", MySqlDbType.Int32).Value = produto.ProdutoQuantidade;
            command.Parameters.Add("@IngredienteProduto", MySqlDbType.Int32).Value = produto.ProdutoIngrediente;

            command.ExecuteNonQuery();
            conexao.DesconectarBD();
        }


        public void editarProduto(Produto produto)
        {
            Conexao conexao = new Conexao();
            string updateQuery = "UPDATE tbProduto SET ProdutoNome = @NomeProduto, ProdutoDescricao = @DescricaoProduto, ProdutoPreco = @PrecoProduto, ProdutoQuantidade = @QuantidadeProduto, IngredienteProduto = @IngredienteProduto WHERE ProdutoID = @ProdutoID";

            MySqlCommand command = new MySqlCommand(updateQuery, conexao.ConectarBD());
            command.Parameters.Add("@NomeProduto", MySqlDbType.VarChar).Value = produto.ProdutoNome;
            command.Parameters.Add("@DescricaoProduto", MySqlDbType.VarChar).Value = produto.ProdutoDescricao;
            command.Parameters.Add("@PrecoProduto", MySqlDbType.Decimal).Value = produto.ProdutoPreco;
            command.Parameters.Add("@QuantidadeProduto", MySqlDbType.Int32).Value = produto.ProdutoQuantidade;
            command.Parameters.Add("@IngredienteProduto", MySqlDbType.Int32).Value = produto.ProdutoIngrediente;
            command.Parameters.Add("@ProdutoID", MySqlDbType.Int32).Value = produto.ProdutoID;

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

        public void deletarProduto(Produto produto)
        {
            Conexao conexao = new Conexao();
            string deleteQuery = "DELETE FROM tbproduto WHERE tbproduto.produtoID = @ProdutoID";

            MySqlCommand command = new MySqlCommand(deleteQuery, conexao.ConectarBD());
            command.Parameters.Add("@ProdutoID", MySqlDbType.Int32).Value = produto.ProdutoID;

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

        public List<Produto> listarTodosProdutos()
        {
            Conexao conexao = new Conexao();

            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM tbProduto", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();

            return dadosReaderParaList(dados);
        }

        public Produto listarProdutoPorID(int id)
        {
            Conexao conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM tbProduto WHERE ProdutoID = {0}", id);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<Produto> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaProdutos = new List<Produto>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {

                    var produto = new Produto()
                    {
                        ProdutoID = Conexao.ConverteInt32(dados["ProdutoID"]),
                        ProdutoNome = Conexao.ConverteString(dados["ProdutoNome"]),
                        ProdutoDescricao = Conexao.ConverteString(dados["ProdutoDescricao"]),
                        ProdutoPreco = Conexao.ConverteDecimal(dados["ProdutoPreco"]),
                        ProdutoQuantidade = Conexao.ConverteInt32(dados["ProdutoQuantidade"]),
                        ProdutoIngrediente = Conexao.ConverteBoolean(dados["IngredienteProduto"])
                    };
                    listaProdutos.Add(produto);
                }
            }
            dados.Close();
            return listaProdutos;
        }
    }
}

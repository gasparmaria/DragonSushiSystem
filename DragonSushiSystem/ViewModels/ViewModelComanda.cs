using DragonSushiSystem.Banco;
using DragonSushiSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonSushiSystem.ViewModels
{
    public class ViewModelComanda
    {
        public Comanda Comanda { get; set; }

        public Mesa Mesa { get; set; }

        public Cliente Cliente { get; set; }

        public List<Cliente> listaClientes { get ;  set; }

        public List<Mesa> listaMesas { get; set; }

        public void cadastrarComanda(ViewModelComanda viewModelComanda)
        {
            Conexao conexao = new Conexao();
            MySqlCommand command = new MySqlCommand("INSERT INTO tbComanda VALUES (DEFAULT, @MesaID, @ClienteID, DEFAULT)", conexao.ConectarBD());

                command.Parameters.Add("@MesaID", MySqlDbType.Int32).Value = viewModelComanda.Comanda.FK_Mesa;
                command.Parameters.Add("@ClienteID", MySqlDbType.Int32).Value = viewModelComanda.Comanda.FK_Cliente;

            command.ExecuteNonQuery();
            conexao.DesconectarBD();
        }

        public List<ViewModelComanda> listarTodosComandasAbertas()
        {
            Conexao conexao = new Conexao();

            MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM vwExibirComanda", conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();

            return dadosReaderParaList(dados);
        }

        public ViewModelComanda listarComandaPorID(int id)
        {
            Conexao conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM vwExibirComandas WHERE ComandaID = {0}", id);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaList(dados).FirstOrDefault();
        }

        public List<ViewModelComanda> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaComandas = new List<ViewModelComanda>();
            Cliente cliente = new Cliente();
            Mesa mesa = new Mesa();

            ViewModelComanda vmcomanda = new ViewModelComanda()
            {
                Cliente = new Cliente(),
                Mesa = new Mesa(),
                Comanda = new Comanda(),
                listaClientes = cliente.listarTodosClientes(),
                listaMesas = mesa.listarTodasMesas()
            };

            listaComandas.Add(vmcomanda);

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var vwComanda = new ViewModelComanda()
                    {

                        Comanda = new Comanda()
                        {
                            ComandaID = Conexao.ConverteInt32(dados["ComandaID"]),
                            ComandaStatus = Conexao.ConverteBoolean(dados["ComandaStatus"])
                        },

                        Mesa = new Mesa()
                        {
                            MesaID = Conexao.ConverteInt32(dados["MesaID"])
                        },

                        Cliente = new Cliente()
                        {
                            ClienteID = Conexao.ConverteInt32(dados["ClienteID"]),
                            ClienteNome = Conexao.ConverteString(dados["ClienteNome"])
                        },

                        listaClientes =  new List<Cliente>(),

                        listaMesas = new List<Mesa>()
                    };
                    listaComandas.Add(vwComanda);
                }
            }
            dados.Close();            
            return listaComandas;
        }
    }
}
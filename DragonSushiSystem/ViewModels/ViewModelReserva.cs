using DragonSushiSystem.Banco;
using DragonSushiSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushiSystem.ViewModels
{
    public class ViewModelReserva
    {
        public Reserva Reserva { get; set; }

        public Mesa Mesa { get; set; }

        public Cliente Cliente { get; set; }

        public List<Reserva> listaReservas { get; set; }

        public List<Mesa> listaMesas { get; set; }

        public List<Cliente> listaClientes { get; set; }

        public void cadastrarReserva(ViewModelReserva vmReserva)
        {
            Conexao conexao = new Conexao();

            MySqlCommand cmd = new MySqlCommand("CALL spInsertReserva(@ClienteID, @MesaID, @NumeroPessoas, @DataReserva, @Periodo);", conexao.ConectarBD());
            cmd.Parameters.Add("@ClienteID", MySqlDbType.Int32).Value = vmReserva.Reserva.FK_ClienteID;
            cmd.Parameters.Add("@MesaID", MySqlDbType.Int32).Value = vmReserva.Reserva.FK_MesaID;
            cmd.Parameters.Add("@NumeroPessoas", MySqlDbType.Int32).Value = vmReserva.Reserva.ReservaNumeroPessoas;
            cmd.Parameters.Add("@DataReserva", MySqlDbType.DateTime).Value = vmReserva.Reserva.ReservaData;
            cmd.Parameters.Add("@Periodo", MySqlDbType.VarChar).Value = vmReserva.Reserva.ReservaPeriodo;

            cmd.ExecuteNonQuery();
            conexao.DesconectarBD();
        }

        public void editarReserva(ViewModelReserva vmReserva)
        {
            Conexao conexao = new Conexao();
            MySqlCommand cmd = new MySqlCommand("CALL spUpdateReserva(@ClienteID,@MesaID,@NumeroPessoas,@DataReserva,@Periodo,@ReservaID)", conexao.ConectarBD());
            cmd.Parameters.Add("@ClienteID", MySqlDbType.Int32).Value = vmReserva.Reserva.FK_ClienteID;
            cmd.Parameters.Add("@MesaID", MySqlDbType.Int32).Value = vmReserva.Reserva.FK_MesaID;
            cmd.Parameters.Add("@NumeroPessoas", MySqlDbType.Int32).Value = vmReserva.Reserva.ReservaNumeroPessoas;
            cmd.Parameters.Add("@DataReserva", MySqlDbType.DateTime).Value = vmReserva.Reserva.ReservaData;
            cmd.Parameters.Add("@Periodo", MySqlDbType.VarChar).Value = vmReserva.Reserva.ReservaPeriodo;
            cmd.Parameters.Add("@ReservaID", MySqlDbType.Int32).Value = vmReserva.Reserva.ReservaID;

            cmd.ExecuteNonQuery();
            conexao.DesconectarBD();
        }

        public void deletarReserva(int id)
        {

            Conexao conexao = new Conexao();
            String deleteQuery = String.Format("DELETE FROM tbReserva WHERE ReservaID = {0}", id);
            MySqlCommand cmd = new MySqlCommand(deleteQuery, conexao.ConectarBD());
            cmd.ExecuteNonQuery();
            conexao.DesconectarBD();
        }
        
        public List<ViewModelReserva> listarTodasReservas(string dataReserva)
        {
            Conexao conexao = new Conexao();
            String selectQuery = String.Format("SELECT * FROM vwExibirReserva WHERE DataReserva = '{0}'", dataReserva);       
            MySqlCommand selectCommand = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = selectCommand.ExecuteReader();

            return dadosReaderParaListCadastrar(dados);
        }

        public ViewModelReserva listarReservaPorID(int id)
        {
            Conexao conexao = new Conexao();
            var selectQuery = String.Format("SELECT * FROM vwExibirReserva WHERE ReservaID = {0}", id);
            MySqlCommand comando = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = comando.ExecuteReader();
            return dadosReaderParaListEditar(dados).FirstOrDefault();
        }

        public List<ViewModelReserva> dadosReaderParaListCadastrar(MySqlDataReader dados)
        {
            var listaModelReservas = new List<ViewModelReserva>();
            Cliente cliente = new Cliente();
            Mesa mesa = new Mesa();
            Reserva reserva = new Reserva();
            string data = DateTime.Now.ToString("yyyy'-'MM'-'dd");

            ViewModelReserva vmreserva = new ViewModelReserva()
            {
                Cliente = new Cliente(),
                Mesa = new Mesa(),
                Reserva = new Reserva(),
                listaClientes = cliente.listarTodosClientes(),
                listaMesas = mesa.listarTodasMesas(),
                listaReservas = reserva.listarTodasReservas(data)
            };
            listaModelReservas.Add(vmreserva);

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var vmReserva = new ViewModelReserva()
                    {
                        Reserva = new Reserva()
                        {
                            ReservaID = Conexao.ConverteInt32(dados["ReservaID"]),
                            ReservaData = DateTime.Parse(dados["DataReserva"].ToString()),
                            ReservaNumeroPessoas = Conexao.ConverteInt32(dados["NumeroPessoas"]),
                            FK_MesaID = Conexao.ConverteInt32(dados["FK_MesaID"]),
                            ReservaPeriodo = Conexao.ConverteString(dados["Periodo"])
                        },

                        Mesa = new Mesa()
                        {
                            MesaID = Conexao.ConverteInt32(dados["MesaID"]),
                            MesaStatus = Conexao.ConverteBoolean(dados["MesaStatus"])
                        },

                        Cliente = new Cliente()
                        {
                            ClienteID = Conexao.ConverteInt32(dados["ClienteID"]),
                            ClienteNome = Conexao.ConverteString(dados["ClienteNome"]),
                            ClienteTelefone = Conexao.ConverteString(dados["ClienteTelefone"])
                        },

                        listaClientes = new List<Cliente>(),
                        listaMesas = new List<Mesa>(),
                        listaReservas = new List<Reserva>()
                    };
                    listaModelReservas.Add(vmReserva);
                }
            }
            dados.Close();
            return listaModelReservas;
        }
        public List<ViewModelReserva> dadosReaderParaListEditar(MySqlDataReader dados)
        {
            var listaModelReservas = new List<ViewModelReserva>();

            string data = DateTime.Now.ToString("yyyy'-'MM'-'dd");

            var cliente = new Cliente();
            var mesa = new Mesa();
            var reserva = new Reserva();
            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var vmReserva = new ViewModelReserva()
                    {
                        Reserva = new Reserva()
                        {
                            ReservaID = Conexao.ConverteInt32(dados["ReservaID"]),
                            ReservaData = DateTime.Parse(dados["DataReserva"].ToString()),
                            ReservaNumeroPessoas = Conexao.ConverteInt32(dados["NumeroPessoas"]),
                            FK_MesaID = Conexao.ConverteInt32(dados["FK_MesaID"]),
                            ReservaPeriodo = Conexao.ConverteString(dados["Periodo"])
                        },

                        Mesa = new Mesa()
                        {
                            MesaID = Conexao.ConverteInt32(dados["MesaID"]),
                            MesaStatus = Conexao.ConverteBoolean(dados["MesaStatus"])
                        },

                        Cliente = new Cliente()
                        {
                            ClienteID = Conexao.ConverteInt32(dados["ClienteID"]),
                            ClienteNome = Conexao.ConverteString(dados["ClienteNome"]),
                            ClienteTelefone = Conexao.ConverteString(dados["ClienteTelefone"])
                        },

                        listaClientes = cliente.listarTodosClientes(),
                        listaMesas = mesa.listarTodasMesas(),
                        listaReservas = reserva.listarTodasReservas(data)
                    };
                    listaModelReservas.Add(vmReserva);
                }
            }
            dados.Close();
            return listaModelReservas;
        }
    }
}
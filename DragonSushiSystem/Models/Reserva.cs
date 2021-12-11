using DragonSushiSystem.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

namespace DragonSushiSystem.Models
{
    public class Reserva
    {
        public int ReservaID { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório.")]
        public int FK_ClienteID { get; set; }

        [Required(ErrorMessage = "A mesa é obrigatória.")]
        public int FK_MesaID { get; set; }

        [Display(Name = "Número de pessoas")]
        [Required(ErrorMessage = "O número de pessoas é obrigatório.")]
        public int ReservaNumeroPessoas { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "A data da reserva é obrigatória.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReservaData { get; set; }

        [Display(Name = "Período")]
        [Required(ErrorMessage = "O período é obrigatório.")]
        public string ReservaPeriodo { get; set; }

        public List<Reserva> listarTodasReservas(string data)
        {
            Conexao conexao = new Conexao();
            
            string selectQuery = String.Format("SELECT * FROM vwExibirReserva WHERE DataReserva = '{0}'", data);
            MySqlCommand command = new MySqlCommand(selectQuery, conexao.ConectarBD());
            var dados = command.ExecuteReader();

            return dadosReaderParaList(dados);
        }

        public List<Reserva> dadosReaderParaList(MySqlDataReader dados)
        {
            var listaReservas = new List<Reserva>();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    var vmReserva = new Reserva()
                    {
                        ReservaID = Conexao.ConverteInt32(dados["ReservaID"]),
                        ReservaData = DateTime.Parse(dados["DataReserva"].ToString()),
                        ReservaNumeroPessoas = Conexao.ConverteInt32(dados["NumeroPessoas"]),
                        FK_MesaID = Conexao.ConverteInt32(dados["FK_MesaID"]),
                        ReservaPeriodo = Conexao.ConverteString(dados["Periodo"])
                    };
                    listaReservas.Add(vmReserva);
                }
            }
            dados.Close();
            return listaReservas;
        }

        public void cadastrarReserva(Reserva reserva)
        {
            Conexao conexao = new Conexao();

            MySqlCommand cmd = new MySqlCommand("CALL spInsertReserva(@ClienteID,@MesaID,@NumeroPessoas,@DataReserva,@Periodo)", conexao.ConectarBD());
            cmd.Parameters.Add("@ClienteID", MySqlDbType.VarChar).Value = reserva.FK_ClienteID;
            cmd.Parameters.Add("@MesaID", MySqlDbType.VarChar).Value = reserva.FK_MesaID;
            cmd.Parameters.Add("@NumeroPessoas", MySqlDbType.VarChar).Value = reserva.ReservaNumeroPessoas;
            cmd.Parameters.Add("@DataReserva", MySqlDbType.DateTime).Value = reserva.ReservaData;
            cmd.Parameters.Add("@Periodo", MySqlDbType.VarChar).Value = reserva.ReservaPeriodo;
            cmd.ExecuteNonQuery();
            conexao.DesconectarBD();
        }
    }
}
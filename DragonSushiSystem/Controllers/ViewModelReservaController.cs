using DragonSushiSystem.Models;
using DragonSushiSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushiSystem.Controllers
{
    public class ViewModelReservaController : Controller
    {
        // GET: ViewModelReserva
        public ActionResult Reserva()
        {
            ViewModelReserva vmReserva = new ViewModelReserva();
            string data = DateTime.Now.ToString("yyyy'-'MM'-'dd");
            var listaReservas = vmReserva.listarTodasReservas(data);
            return View(listaReservas);
        }                      

        [HttpPost]
        public ActionResult Reserva(FormCollection formCollection)
        {
            ViewModelReserva vmReserva = new ViewModelReserva();
            string data = formCollection[1];
            var listaReservas = vmReserva.listarTodasReservas(data);
            return View(listaReservas);
        }

        public ActionResult CadastrarReserva()
        {
            ViewModelReserva vmReserva = new ViewModelReserva();
            string data = DateTime.Now.ToString("yyyy'-'MM'-'dd");
            var reservaListas = vmReserva.listarTodasReservas(data).FirstOrDefault();
            return View(reservaListas);
        }

        [HttpPost]
        public ActionResult CadastrarReserva(ViewModelReserva vmReserva)
        {

            try
            {
                vmReserva.cadastrarReserva(vmReserva);
                string data = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                var listaReservas = vmReserva.listarTodasReservas(data);
                return View("Reserva", listaReservas);
                
            }
            catch
            {
                return View();
            }
            
        }

        public ActionResult EditarReserva(int id)
        {
            ViewModelReserva vmReserva = new ViewModelReserva();
            var reservaSelecionada = vmReserva.listarReservaPorID(id);
            return View(reservaSelecionada);
        }

        [HttpPost]
        public ActionResult EditarReserva(ViewModelReserva vmReserva)
        {
            try
            {
                vmReserva.editarReserva(vmReserva);
                string data = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                var listaReservas = vmReserva.listarTodasReservas(data);
                return View("Reserva", listaReservas);
            }
            catch
            {               
                return View(vmReserva);
            }
        }

        public ActionResult DeletarReserva(int id)
        { 
            ViewModelReserva vmReserva = new ViewModelReserva();
            try
            {
                vmReserva.deletarReserva(id);
                string data = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                var listaReservas = vmReserva.listarTodasReservas(data);
                return View("Reserva", listaReservas);    
            }
            catch
            {
                return EditarReserva(id);
            }
        }
    }
}
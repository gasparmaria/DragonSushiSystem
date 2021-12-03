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

        public ActionResult EditarReserva()
        {
            return PartialView();
        }
    }
}
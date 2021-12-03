using DragonSushiSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushiSystem.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            Cliente cliente = new Cliente();
            var listaClientes = cliente.listarTodosClientes();
            return View(listaClientes);
        }
    }
}
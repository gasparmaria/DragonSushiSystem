using DragonSushiSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushiSystem.Controllers
{
    public class ViewModelPedidoController : Controller
    {
        // GET: ViewModelPedido
        public ActionResult ComandaDetalhes()
        {
            ViewModelPedido vmPedido = new ViewModelPedido();
            var listaPedidos = vmPedido.listarTodosPedidos(1);

            return View(listaPedidos);
        }
    }
}
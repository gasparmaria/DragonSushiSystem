using DragonSushiSystem.ViewModels;
using System.Web.Mvc;

namespace DragonSushiSystem.Controllers
{
    public class ViewModelClienteController : Controller
    {
        // GET: ViewModelCliente
        public ActionResult ListarCliente()
        {
            ViewModelCliente vwFuncinario = new ViewModelCliente();
            var listaClientes = vwFuncinario.listarTodosClientes();

            return View(listaClientes);
        }

        // GET: ViewModelCliente/Create
        public ActionResult CadastrarCliente()
        {
            return View();
        }

        // POST: ViewModelCliente/Create
        [HttpPost]
        public ActionResult CadastrarCliente(ViewModelCliente vmCliente)
        {
            try
            {
                vmCliente.cadastrarCliente(vmCliente);
                var Clientes = vmCliente.listarTodosClientes();
                return RedirectToAction("ListarCliente", Clientes);
            }
            catch
            {
                return View();
            }
        }

        // GET: ViewModelCliente/Edit/5
        public ActionResult EditarCliente(int id)
        {
            ViewModelCliente vmCliente = new ViewModelCliente();
            var ClienteSelecionado = vmCliente.listarClientePorID(id);
            return View(ClienteSelecionado);
        }

        // POST: ViewModelCliente/Edit/5
        [HttpPost]
        public ActionResult EditarCliente(ViewModelCliente vmCliente)
        {
            try
            {
                vmCliente.editarCliente(vmCliente);
                var Clientes = vmCliente.listarTodosClientes();
                return RedirectToAction("ListarCliente", Clientes);
            }
            catch
            {
                return View();
            }
        }
    }
}

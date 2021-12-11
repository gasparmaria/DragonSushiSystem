using DragonSushiSystem.Models;
using DragonSushiSystem.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace DragonSushiSystem.Controllers
{
    public class ViewModelComandaController : Controller
    {
        // GET: ViewModelComanda
        public ActionResult ListarComanda()
        {
            ViewModelComanda vmComanda = new ViewModelComanda();
            var listaComandasAbertas = vmComanda.listarTodosComandasAbertas();
           
            return View(listaComandasAbertas);
        }

        public ActionResult CadastrarComanda()
        {
            ViewModelComanda vmComanda = new ViewModelComanda();
            var listaComandasAbertas = vmComanda.listarTodosComandasAbertas().FirstOrDefault();

            return View(listaComandasAbertas);
        }

        [HttpPost]
        public ActionResult CadastrarComanda(ViewModelComanda vmComanda)
        {

            try
            {
                if (vmComanda.Mesa == null)
                {
                    vmComanda.Mesa = new Mesa();
                    vmComanda.cadastrarComanda(vmComanda);
                }
                else if(vmComanda.Cliente == null)
                {
                    vmComanda.Cliente = new Cliente();
                    vmComanda.cadastrarComanda(vmComanda);
                }
                var listaComandas = vmComanda.listarTodosComandasAbertas();
                return View("ListarComanda", listaComandas);

            }
            catch
            {
                return View();
            }

        }

        public ActionResult EditarComanda(int id)
        {
            ViewModelComanda vmComanda = new ViewModelComanda();
            var ComandaSelecionada = vmComanda.listarComandaPorID(id);
            return View(ComandaSelecionada);
        }

        [HttpPost]
        public ActionResult EditarComanda(ViewModelComanda vmComanda)
        {
            try
            {
                //vmComanda.editarComanda(vmComanda);
                var listaComandas = vmComanda.listarTodosComandasAbertas();
                return View("ListarComanda", listaComandas);
            }
            catch
            {
                return View(vmComanda);
            }
        }
    }
}

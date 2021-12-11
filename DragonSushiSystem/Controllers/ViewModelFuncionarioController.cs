using DragonSushiSystem.ViewModels;
using System.Web.Mvc;
using System.Web;
using System.IO;
using DragonSushiSystem.Models;

namespace DragonSushiSystem.Controllers
{
    public class ViewModelFuncionarioController : Controller
    {
        // GET: ViewModelFuncionario
        public ActionResult ListarFuncionario()
        {
            ViewModelFuncionario vwFuncinario = new ViewModelFuncionario();
            var listaFuncionarios = vwFuncinario.listarTodosFuncionarios();

            return View(listaFuncionarios);
        }

        // GET: ViewModelFuncionario/Create
        public ActionResult CadastrarFuncionario()
        {
            return View();
        }

        public FileContentResult getImagem(int id)
        {
            ViewModelFuncionario vmFuncionario = new ViewModelFuncionario();
            byte[] byteArray = vmFuncionario.Funcionario.FuncionarioFotoPerfil;
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }

        // POST: ViewModelFuncionario/Create
        [HttpPost]
        public ActionResult CadastrarFuncionario(ViewModelFuncionario vmFuncionario, HttpPostedFileBase file)
        {
            try 
            {
                if(file.FileName != null)
                {
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    byte[] data = target.ToArray();
                    vmFuncionario.Funcionario.FuncionarioFotoPerfil = data;
                }
                vmFuncionario.cadastrarFuncionario(vmFuncionario);
                var funcionarios = vmFuncionario.listarTodosFuncionarios();
                return RedirectToAction("ListarFuncionario", funcionarios);
            }
            catch
            {
                return View();
            }
        }

        // GET: ViewModelFuncionario/Edit/5
        public ActionResult EditarFuncionario(int id)
        {
            ViewModelFuncionario vmFuncionario = new ViewModelFuncionario();
            var funcionarioSelecionado = vmFuncionario.listarFuncionarioPorID(id);
            return View(funcionarioSelecionado);
        }

        // POST: ViewModelFuncionario/Edit/5
        [HttpPost]
        public ActionResult EditarFuncionario(ViewModelFuncionario vmFuncionario)
        {
            try
            {
                vmFuncionario.editarFuncionario(vmFuncionario);
                var funcionarios = vmFuncionario.listarTodosFuncionarios();
                return RedirectToAction("ListarFuncionario", funcionarios);
            }
            catch
            {
                return View();
            }
        }

        // GET: ViewModelFuncionario/Delete/5

        public ActionResult DeletarFuncionario(int id)
        {
            ViewModelFuncionario vmFuncionario = new ViewModelFuncionario();
            try
            {
                vmFuncionario.deletarFuncionario(id);
                var funcionarios = vmFuncionario.listarTodosFuncionarios();
                return RedirectToAction("ListarFuncionario", funcionarios);
            }
            catch
            {
                return View("ListarFuncionario");
            }
        }

        // // POST: ViewModelFuncionario/Delete/5
        // [HttpPost]
        // public ActionResult DeletarFuncionario(Funcionario funcionario)
        // {
        //     ViewModelFuncionario vmFuncionario = new ViewModelFuncionario();
        //     try
        //     {
        //         vmFuncionario.deletarFuncionario(funcionario);
        //         var funcionarios = vmFuncionario.listarTodosFuncionarios();
        //         return RedirectToAction("ListarFuncionario", funcionarios);
        //     }
        //     catch
        //     {
        //         return View("NotaFiscal");
        //     }
        // }
    }
}

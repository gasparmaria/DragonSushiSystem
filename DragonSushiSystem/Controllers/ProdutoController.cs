using DragonSushiSystem.Models;
using System.Web.Mvc;

namespace DragonSushiSystem.Controllers
{
    public class ProdutoController : Controller
    {
        // Produto/ListarProduto
        public ActionResult ListarProduto()
        {
            Produto produto = new Produto();
            var listaprodutos = produto.listarTodosProdutos();

            return View(listaprodutos);
        }
        
        // Produto/ListarProduto/NomeDoProduto
        [HttpPost]
        public ActionResult ListarProduto(string id)
        {
            Produto produto = new Produto();
            var produtoprocurado = produto.procurarProduto(id);
            return View(produtoprocurado);
        }

        // GET: Produto/Create
        public ActionResult CadastrarProduto()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult CadastrarProduto(Produto produto)
        {
            try
            {
                // TODO: Add insert logic here
                
                produto.cadastrarProduto(produto);
                var produtos = produto.listarTodosProdutos();
                return RedirectToAction("ListarProduto", produtos);
            }
            catch
            {
                return View();
            }
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult EditarProduto(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produto.editarProduto(produto);
                    var produtos = produto.listarTodosProdutos();
                    return RedirectToAction("ListarProduto", produtos);
                }
                return View(produto);
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Delete/5

        public ActionResult DeletarProduto(int id)
        {
            Produto produto = new Produto();
            var produtoselecionado = produto.listarProdutoPorID(id);
            return View(produtoselecionado);
        }

        // POST: Produto/Delete/5
        [HttpPost]
        public ActionResult DeletarProduto(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produto.deletarProduto(produto);
                    var produtos = produto.listarTodosProdutos();
                    return RedirectToAction("ListarProduto", produtos);
                }
                return View(produto);
            }
            catch
            {
                return View();
            }
        }

        
    }

}
using ProjetoBudget.Models;
using System.Linq;
using System.Web.Mvc;

namespace Projeto_Budget.Controllers
{
    public class ItemOrcamentarioController : Controller
    {
        BDBudgetEntities db = new BDBudgetEntities();
        // GET: ItemOrcamentoController
        public ActionResult Cadastro()
        {
            return View(db.naturezaGasto.ToList());
        }


    }
}
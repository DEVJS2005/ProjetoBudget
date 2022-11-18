using ProjetoBudget.Models;
using System.Linq;
using System.Web.Mvc;

namespace Projeto_Budget.Controllers
{
    public class ItemOrcamentarioController : Controller
    {
        BDBudgetEntities db = new BDBudgetEntities();
        // GET: ItemOrcamentoController
        public ActionResult Index() 
        { 
            return View(db.itensOrcamentarios.ToList());
        }
        
        [HttpGet]
        public ActionResult Cadastro()
        {
            return View(db.TipoItem.ToList());
        }

        [HttpPost]
        public ActionResult Cadastro(string descricaoItem, string tipoGasto, int TipoItem, float? valorUnitario)
        {
            TipoItem tpI = db.TipoItem.Find(TipoItem);
            
            itensOrcamentarios Io = new itensOrcamentarios();
            Io.descricaoItem = descricaoItem;       
            Io.valorUnitario = valorUnitario;
            Io.tipoGasto = tipoGasto;
            Io.idTipoItem = TipoItem;
            Io.TipoItem = tpI;
            if(valorUnitario != null)
            {
                Io.valorUnitario = valorUnitario;   
            }
            db.itensOrcamentarios.Add(Io);
            db.SaveChanges();
            return View("index");
        }
    }
}
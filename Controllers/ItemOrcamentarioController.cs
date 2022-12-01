using ProjetoBudget.Models;
using System.Linq;
using System.Web.Mvc;


namespace Projeto_Budget.Controllers
{
    public class ItemOrcamentarioController : Controller
    {
        public bool VerificarLogin()
        {
<<<<<<< Updated upstream
            if (Session["LoginGF"] != null)
            {
                return true;
            }
            else if (Session["LoginGS"] != null) { return true; }
            return false;
=======
            if (Session["Login"] != null)
            {
                return true;
            }
                return false;
>>>>>>> Stashed changes
        }
        BDBudgetEntities db = new BDBudgetEntities();
        // GET: ItemOrcamentoController
        
        public ActionResult ListaItens() 
        {
<<<<<<< Updated upstream
            
            if (VerificarLogin()) 
            {
               return View(db.itensOrcamentarios.ToList());
=======
            if (VerificarLogin()) 
            {
                return View(db.itensOrcamentarios.ToList());
>>>>>>> Stashed changes
            }
            else
            {
                return RedirectToAction("TelaLogin","Login");
            }

            
        }
        
        [HttpGet]
        public ActionResult Cadastro()
        {
            if (VerificarLogin())
            {
                return View(db.TipoItem.ToList());
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }
            
        }

        [HttpPost]
        public ActionResult Cadastro(string descricaoItem, string tipoGasto, int TipoItem, float? valorUnitario)
        {
            TipoItem tpI = db.TipoItem.ToList().Find(x => x.idTipoItem == TipoItem);
            
            itensOrcamentarios Io = new itensOrcamentarios();
            Io.descricaoItem = descricaoItem;       
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
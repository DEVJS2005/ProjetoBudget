using ProjetoBudget.Models;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace Projeto_Budget.Controllers
{
    public class ItemOrcamentarioController : Controller
    {
        public bool VerificarLogin()
        {

            if (Session["LoginGF"] != null)
            {
                return true;
            }
            else if (Session["LoginGS"] != null) { return true; }
            return false;

        }

        BDBudgetEntities db = new BDBudgetEntities();
        // GET: ItemOrcamentoController

        public ActionResult ListaItens()
        {
            if (VerificarLogin())
            {
                return View(db.itensOrcamentarios.ToList());

            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
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
            if (valorUnitario != null)
            {
                Io.valorUnitario = valorUnitario;
            }
            db.itensOrcamentarios.Add(Io);
            db.SaveChanges();
            return RedirectToAction("ListaItens");
        }

        public ActionResult Editar(int idI)
        {
            return View(db.itensOrcamentarios.ToList().Find(x => x.idItemOrcamentario == idI));
        }

        [HttpPost]
        public ActionResult Editar(int idTipoItem,string DescricaoItem, string tipoGasto,double? valorUnitario,int idI)
        {
            itensOrcamentarios iO = new itensOrcamentarios();
            iO.idItemOrcamentario = idI;
            iO.descricaoItem = DescricaoItem;
            iO.tipoGasto=tipoGasto;
            iO.idTipoItem = idTipoItem;
            iO.valorUnitario = valorUnitario;

            db.itensOrcamentarios.AddOrUpdate(iO);
            db.SaveChanges();

            return RedirectToAction("ListaItens","ItemOrcamentario");

        }

        [HttpGet]
        public ActionResult Excluir(int idI)
        {
            itensOrcamentarios io = db.itensOrcamentarios.Find(idI);   
            db.itensOrcamentarios.Remove(io);
            db.SaveChanges();
            return RedirectToAction("ListaItens","itemOrcamentario");
        }
    }
}
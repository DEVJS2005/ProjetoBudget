using ProjetoBudget.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoBudget.Controllers
{
    public class TipoItemController : Controller
    {
        BDBudgetEntities db = new BDBudgetEntities();
        // GET: TipoItem
        [HttpGet]
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(string nomeTipoItem)
        {
            TipoItem TPI = new TipoItem();  
            TPI.nomeTipoItem= nomeTipoItem;
            db.TipoItem.Add(TPI);
            db.SaveChanges();

            return RedirectToAction("listaItens");
        }
        
        public ActionResult ListaItens()
        {
            return View(db.TipoItem.ToList());
        }

        public ActionResult Editar(int idTPI)
        {
            return View(db.TipoItem.Find(idTPI)) ;
        }

        [HttpPost]
        public ActionResult Editar(int idTPI,string nomeTipoItem)
        {
            TipoItem TPI = db.TipoItem.Find(idTPI);
            TPI.idTipoItem = idTPI;
            TPI.nomeTipoItem = nomeTipoItem;
            db.TipoItem.AddOrUpdate(TPI);
            db.SaveChanges();
            return RedirectToAction("ListaItens");
        }

        public ActionResult Excluir(int idTPI)
        {
            TipoItem TPI = db.TipoItem.Find(idTPI);
            db.TipoItem.Remove(TPI);
            return RedirectToAction("ListaItens");
        }
    }
}
using ProjetoBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoBudget.Controllers
{
    public class CentroGastoController : Controller
    {
        public int VerificarLogin()
        {
            if (Session["LoginGF"] != null)
            {
                Funcionario F = (Funcionario)Session["LoginGF"];
                return F.idFuncionario;
            }
            else if (Session["LoginGS"] != null) 
            {
                Funcionario F = (Funcionario)Session["LoginGS"];
                return F.idFuncionario;
            }
            return 0;
        }
        BDBudgetEntities db = new BDBudgetEntities();
        // GET: CentroGasto
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(string nomeCentroGasto)
        {
            Funcionario F = db.Funcionario.ToList().Find(x => x.idFuncionario == VerificarLogin());
            if (F == null)
            {
                return RedirectToAction("TelaLogin","Login");
            }
                CentroGasto CG = new CentroGasto();
                CG.nomeCentroGasto = nomeCentroGasto;
                CG.idFuncionario = F.idFuncionario;
                db.CentroGasto.Add(CG);
                db.SaveChanges();
                return RedirectToAction("ListaItens");
        }
        
        public ActionResult ListaItens()
        {
            return View(db.CentroGasto.ToList());
        }

        public ActionResult OrcaVinculados(int idCG)
        {
            return View(db.Orcamento.SqlQuery($"SELECT * FROM Orcamento where idCentroGasto = {idCG}"));
        }
    }
}
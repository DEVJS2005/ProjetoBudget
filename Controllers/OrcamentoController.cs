using ProjetoBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_Budget.Controllers
{
    public class OrcamentoController : Controller
    {
        BDBudgetEntities db = new BDBudgetEntities();
        // GET: Orcamento
        public ActionResult cadastro()
        {
          return View(db.CentroGasto.ToList());
        }

        [HttpPost]
        public ActionResult cadastro(string nomeOrcamento,int idCentroGasto,float valorAlocacao,DateTime dataInicio,DateTime dataFim)
        {
            Orcamento orcamento = new Orcamento();

            CentroGasto cg  = db.CentroGasto.Find(idCentroGasto);

            orcamento.nomeOrcamento = nomeOrcamento;
            orcamento.idcentrogasto = idCentroGasto;
            orcamento.valorAplicado = valorAlocacao;
            orcamento.dataInicio = dataInicio;
            orcamento.dataFim = dataFim;
            orcamento.CentroGasto = cg;
            db.Orcamento.Add(orcamento);
            db.SaveChanges();

            Session["orcamento"] = orcamento;

            return RedirectToAction("Cadastro","Orcaitem");
        }
        public ActionResult Lista()
        {
            return View();
        }
    }
}
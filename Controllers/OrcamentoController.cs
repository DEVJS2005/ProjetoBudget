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
        public ActionResult cadastro(string nomeOrcamento,int idCentroGasto, float valorAlocacao,DateTime dataInicio,DateTime dataFim)
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
            Session["orcamento"] = orcamento.idOrcamento;

            return RedirectToAction("Index","ItemOrcamentario");
        }

        public ActionResult ListaGF()
        {
            return View(db.Orcamento.ToList());
        }

        public ActionResult Acessar(int id)
        {
            Session["idOrca"] = id;
            return RedirectToAction("ListaItens","OrcaItem");
        }

        public ActionResult aprovado(int id)
        {
            Orcamento orca = db.Orcamento.Find(id);
            orca.idOrcamento = id;
            orca.situacao = "A";
            db.Orcamento.Add(orca);
            db.SaveChanges();

            return RedirectToAction("ListaGF");
        }
    }
}
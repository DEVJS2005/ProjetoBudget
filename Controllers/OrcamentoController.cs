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
        public bool VerificarLogin()
        {
            if (Session["Login"] != null)
            {
                return true;
            }
            return false;
        }
        BDBudgetEntities db = new BDBudgetEntities();
        // GET: Orcamento
        public ActionResult cadastro()
        {
            if (VerificarLogin())
            {
                return View(db.CentroGasto.ToList());
            }
            else{
                return RedirectToAction("TelaLogin","Login");
            }
          
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
            if (Session["LoginGF"] != null)
            {
                return View(db.Orcamento.ToList());
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }
        }
        public ActionResult ListaGS()
        {
            if (Session["LoginGS"] != null)
            {
                Funcionario func = (Funcionario)Session["LoginGS"];
                return View(db.Orcamento.ToList().Find(x => x.CentroGasto.idFuncionario == func.idFuncionario));
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }
        }
        public ActionResult Acessar(int id)
        {
            Session["idOrca"] = id;
            return RedirectToAction("ListaItens", "ItemOrcamentario");
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
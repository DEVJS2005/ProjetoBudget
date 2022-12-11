using ProjetoBudget.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_Budget.Controllers
{
    public class OrcamentoController : Controller
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
        // GET: Orcamento
        public ActionResult cadastro()
        {
            if (VerificarLogin())
            {
                return View(db.CentroGasto.ToList());
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }

        }

        [HttpPost]
        public ActionResult cadastro(string nomeOrcamento, int idCentroGasto, float valorAlocacao, DateTime dataInicio, DateTime dataFim)
        {
            Orcamento orcamento = new Orcamento();

            CentroGasto cg = db.CentroGasto.Find(idCentroGasto);

            orcamento.nomeOrcamento = nomeOrcamento;
            orcamento.idcentrogasto = idCentroGasto;
            orcamento.valorAplicado = valorAlocacao;
            orcamento.dataInicio = dataInicio;
            orcamento.dataFim = dataFim;
            orcamento.CentroGasto = cg;
            db.Orcamento.Add(orcamento);
            db.SaveChanges();
            Session["orcamento"] = orcamento.idOrcamento;

            return RedirectToAction("ListaItens", "ItemOrcamentario");
        }

        public ActionResult ListaGF()
        {
            if (Session["LoginGF"] != null)
            {
                return View(db.Orcamento.ToList());
            }
            else if (Session["LoginGS"] != null)
            {
                return RedirectToAction("ListaGS", "Orcamento");
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
                CentroGasto cGasto = db.CentroGasto.ToList().Find(x => x.idFuncionario == func.idFuncionario);
                return View(db.Orcamento.SqlQuery($"SELECT * FROM dbo.Orcamento WHERE idcentrogasto = {cGasto.idCentroGasto}").ToList());


            }
            else if (Session["LoginGF"] != null)
            {
                return RedirectToAction("ListaGF", "Orcamento");
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }
        }

        public ActionResult Acessar(int id)
        {
            if (Session["LoginGF"] != null)
            {
                Session["idOrca"] = id;
                return RedirectToAction("ListaItens", "ItemOrcamentario");
            }
            else if (Session["LoginGS"] != null)
            {
                Session["idOrca"] = id;
                return RedirectToAction("ListaItens", "ItemOrcamentario");
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }

        }

        [HttpGet]
        public ActionResult aprovado(int id)
        {
            if (Session["LoginGF"] != null)
            {
                Orcamento orca = db.Orcamento.Find(id);
                orca.idOrcamento = id;
                orca.situacao = "A";
                db.Orcamento.AddOrUpdate(orca);
                db.SaveChanges();

                return RedirectToAction("ListaGF");
            }
            else if(Session["LoginGS"] != null)
            {
                return RedirectToAction("ListaGS", "orcamento");
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }
        }

        [HttpGet]
        public ActionResult AprovaParcial(int id)
        {
            if (Session["LoginGF"] != null)
            {
                return View(db.Orcamento.Find(id));
            }
            else if (Session["LoginGS"] != null)
            {
                return RedirectToAction("ListaGS", "orcamento");
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }
            
        }

        [HttpPost]
        public ActionResult AprovaParcial(string observacao, int idOrcamento)
        {
            if (Session["LoginGF"] != null)
            {
                Orcamento orc = db.Orcamento.Find(idOrcamento);
                orc.idOrcamento = idOrcamento;
                orc.situacao = "AP";
                orc.observacao = observacao;
                db.Orcamento.AddOrUpdate(orc);
                db.SaveChanges();

                return RedirectToAction("ListaGF");
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }

        }

        [HttpGet]
        public ActionResult Reprova(int id)
        {

            if (Session["LoginGF"] != null)
            {
                return View(db.Orcamento.Find(id));
            }
            else if (Session["LoginGS"] != null)
            {
                return RedirectToAction("ListaGS", "orcamento");
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }
        }

        [HttpPost]
        public ActionResult Reprova(string observacao,int idOrcamento)
        {
            if (Session["LoginGF"] != null)
            {
                Orcamento orc = db.Orcamento.Find(idOrcamento);
                orc.idOrcamento = idOrcamento;
                orc.situacao = "R";
                orc.observacao = observacao;
                db.Orcamento.AddOrUpdate(orc);
                db.SaveChanges();

                return RedirectToAction("ListaGF");
            }
            else{
                return RedirectToAction("TelaLogin", "Login");
            }
            
        }
    }
}
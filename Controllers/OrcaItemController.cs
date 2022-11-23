using ProjetoBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoBudget.Controllers
{
    public class OrcaItemController : Controller
    {
        BDBudgetEntities bd = new BDBudgetEntities();
        // GET: OrcaItem
        
        public ActionResult AddItem(OrcaItem orcItem)
        {
            bool objProduto = false;
            int? idOrcamento;

            itensOrcamentarios itensO = Session["carrinho"] != null ? (itensOrcamentarios)Session["carrinho"] : new itensOrcamentarios();
            if (Session["orcamento"] == null)
            {
                idOrcamento = (int)Session["idOrca"];
            }
            else
            {
                idOrcamento = (int)Session["orcamento"];
            }

            Orcamento orcamento = bd.Orcamento.Find(idOrcamento);


            itensOrcamentarios item = bd.itensOrcamentarios.Find(orcItem.idItemorcamentario);

            if (item != null)
            {
                OrcaItem pedOI = new OrcaItem();
                pedOI.itensOrcamentarios = item;
                pedOI.quantItem = 1;
                pedOI.idorcamento = orcamento.idOrcamento;
                pedOI.Orcamento = orcamento;
                pedOI.itensOrcamentarios = item;
                pedOI.observacao = orcItem.observacao;
                pedOI.prioridade = orcItem.prioridade;

                foreach (var obj in item.OrcaItem)
                {
                    if (obj.itensOrcamentarios.idItemOrcamentario == item.idItemOrcamentario)
                    {
                        obj.quantItem += 1;
                        objProduto = true;
                        break;
                    }

                }

                if (objProduto == false)
                {
                    itensO.OrcaItem.Add(pedOI);
                }



                Session["carrinho"] = itensO;

            }

            return RedirectToAction("Listaitens");
        }

        [HttpGet]
        public ActionResult ListaItens() 
        {
            itensOrcamentarios itensO = Session["carrinho"] != null ? (itensOrcamentarios)Session["carrinho"] : new itensOrcamentarios();
            return View(itensO);

        }
        [HttpGet]
        public ActionResult alterPrioridade(int idI)
        {
            int idOrcamento;
            if (Session["orcamento"] == null)
            {
               idOrcamento = (int)Session["idOrca"];
            }
            else
            {
               idOrcamento = (int)Session["orcamento"];
            }
            OrcaItem orcItem = new OrcaItem();
            orcItem.idItemorcamentario = idI;
            orcItem.idorcamento = idOrcamento;
            return View(orcItem);
        }
        [HttpPost]
        public ActionResult alterPrioridade(string prioridade, string observacao,int idItemOrc)
        {
            int idOrcamento;
            if (Session["orcamento"] == null)
            {
                idOrcamento = (int)Session["idOrca"];
            }
            else
            {
                idOrcamento = (int)Session["orcamento"];
            }
            OrcaItem orcItem = new OrcaItem();
            orcItem.prioridade = prioridade;
            orcItem.observacao = observacao;
            orcItem.idorcamento = idOrcamento;
            orcItem.idItemorcamentario = idItemOrc;
            return RedirectToAction("AddItem","OrcaItem",orcItem);
        }
    }
}
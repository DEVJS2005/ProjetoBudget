﻿using ProjetoBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoBudget.Controllers
{
    public class OrcaItemController : Controller
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
                pedOI.idItemorcamentario = item.idItemOrcamentario;
                pedOI.quantItem = 1;
                pedOI.idorcamento = orcamento.idOrcamento;
                pedOI.Orcamento = orcamento;
                pedOI.itensOrcamentarios = item;
                pedOI.observacao = orcItem.observacao;
                pedOI.prioridade = orcItem.prioridade;
                if(item.tipoGasto == "I")
                {
                    pedOI.total = pedOI.quantItem * item.valorUnitario;
                }
                else
                {
                    pedOI.total = orcItem.total;
                }
                

                foreach (var obj in itensO.OrcaItem)
                {
                    if (obj.itensOrcamentarios.idItemOrcamentario == item.idItemOrcamentario)
                    {
                        obj.quantItem += 1;
                        if(obj.itensOrcamentarios.tipoGasto == "I")
                        {
                            obj.total = obj.quantItem * item.valorUnitario;
                        }
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
            if (VerificarLogin())
            {
                itensOrcamentarios itensO = Session["carrinho"] != null ? (itensOrcamentarios)Session["carrinho"] : new itensOrcamentarios();
                if (itensO != null)
                {
                    return View(itensO);
                }
                return RedirectToAction("ListaItens", "itemOrcamentario");
            }
            else
            {
                return RedirectToAction("TelaLogin","Login");
            }


        }

        [HttpGet]
        public ActionResult alterPrioridade(int idI)
        {
            int idOrcamento;
            if (Session["orcamento"] == null)
            {
                idOrcamento = (int)Session["idOrca"];
            }
            else if(Session["idOrca"] == null)
            {
                idOrcamento = (int)Session["orcamento"];
            }
            else
            {
                return RedirectToAction("ListaGF","Orcamento");
            }
            itensOrcamentarios ItemOrca = bd.itensOrcamentarios.ToList().Find(x => x.idItemOrcamentario == idI);
            OrcaItem orcItem = new OrcaItem();
            orcItem.idItemorcamentario = idI;
            orcItem.itensOrcamentarios = ItemOrca;
            orcItem.idorcamento = idOrcamento;
            return View(orcItem);
        }

        [HttpPost]
        public ActionResult alterPrioridade(string prioridade, string observacao, int idItemOrc, string total)
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
            itensOrcamentarios iOrca = bd.itensOrcamentarios.Find(idItemOrc);
            OrcaItem orcItem = new OrcaItem();
            orcItem.prioridade = prioridade;
            orcItem.observacao = observacao;
            orcItem.idorcamento = idOrcamento;
            orcItem.idItemorcamentario = idItemOrc;
            orcItem.itensOrcamentarios = iOrca;
            orcItem.total = Convert.ToDouble(total);
            return RedirectToAction("AddItem", "OrcaItem", orcItem);
        }
        public ActionResult alterOrcaItem(int idOrcamento, int idItemOrc)
        {
            OrcaItem orcaItem = bd.OrcaItem.ToList().Find(x => x.idorcamento == idOrcamento && x.idItemorcamentario == idItemOrc);
            return View(orcaItem);
        }
    }
}
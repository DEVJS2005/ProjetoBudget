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
        public ActionResult AddItem(int idI)
        {
            bool objProduto = false;

            OrcaItem orcaItem = Session["carrinho"] != null ? (OrcaItem)Session["carrinho"] : new OrcaItem();

            itensOrcamentarios item = bd.itensOrcamentarios.Find(idI);

            if (item != null)
            {
                OrcaItem pedOI = new OrcaItem();
                pedOI.itensOrcamentarios = item;
                pedOI.quantItem = 1;

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
                    item.OrcaItem.Add(pedOI);
                }



                Session["carrinho"] = orcaItem;

            }

            return RedirectToAction("ListaItens");
        }
    }
}
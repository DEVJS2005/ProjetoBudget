using ProjetoBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_Budget.Controllers
{
    public class ItemOrcamentarioController : Controller
    {
        BDBudgetEntities db = new BDBudgetEntities();
        // GET: ItemOrcamentoController
        public ActionResult Cadastro()
        {
            return View(db.itensOrcamentarios.ToList());
        }


    }
}
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
        public ActionResult Cadastro()
        {
            return View(bd.itensOrcamentarios.ToList());
        }
    }
}
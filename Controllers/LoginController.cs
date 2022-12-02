
﻿using Projeto_Budget.Controllers;
using ProjetoBudget.Models;
﻿using ProjetoBudget.Models;
using System.Linq;
using System.Web.Mvc;

namespace ProjetoBudget.Controllers
{
    public class LoginController : Controller
    {
        BDBudgetEntities db = new BDBudgetEntities();
        // GET: Login
        public ActionResult TelaLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string matricula, string senha)
        {

            Funcionario funcionario = db.Funcionario.ToList().Find(x => x.matricula == matricula);

            if (funcionario.matricula.Equals(matricula) && funcionario.senha.Equals(senha))
            {

                if (funcionario.cargo == "GF")
                {
                    Session["LoginGF"] = funcionario;
                    return RedirectToAction("TelaGF", "Home");
                }
                else
                {
                    Session["LoginGS"] = funcionario;
                    return RedirectToAction("TelaGS", "Home");
                }
            }
            else
            {
                return View("TelaLogin");
            }
        }
    }
}
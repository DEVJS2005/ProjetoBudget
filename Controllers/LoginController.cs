<<<<<<< Updated upstream
﻿using Projeto_Budget.Controllers;
using ProjetoBudget.Models;
=======
﻿using ProjetoBudget.Models;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

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
=======
                if(funcionario.cargo == "GF")
                {
                    Session.Add("LoginGF", funcionario);
                }else if(funcionario.cargo == "GS")
                {
                    Session.Add("LoginGS", funcionario);
                }
                
                return RedirectToAction("Cadastro", "Orcamento");
            }
            return View("TelaLogin");
>>>>>>> Stashed changes
        }
    }
}
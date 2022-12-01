using ProjetoBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_Budget.Controllers
{
    public class HomeController : Controller
    {
        public bool VerificarLogin()
        {
            if (Session["LoginGF"] != null)
            {
                return true;
            }else if (Session["LoginGS"] != null) { return true; }
            return false;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TelaGF()
        {
            if (VerificarLogin())
            {
                return View((Funcionario)Session["LoginGF"]);
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }

        }
        public ActionResult TelaGS()
        {
            
            if (VerificarLogin())
            {
                return View((Funcionario)Session["LoginGS"]);
            }
            else
            {
                return RedirectToAction("TelaLogin", "Login");
            }
        }

    }
}
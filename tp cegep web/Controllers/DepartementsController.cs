using GestionCegepWeb.Logics.Controleurs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace tp_cegep_web.Controllers
{
    public class DepartementController : Controller
    {
        [Route("Departement")]
        [Route("Departement/Index")]
        [HttpGet]
        public IActionResult Index([FromQuery] string nomCegep)
        {
            try
            {
                //nomCegep = "Cégep de Rivière-du-Loup";
                nomCegep = "Cégep de la Pocatière";
                //Préparation des données pour la vue...
                ViewBag.ListeDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            //Retour de la vue...
            return View();
        }
    }
}


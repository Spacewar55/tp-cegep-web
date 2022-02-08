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
                if(nomCegep == null)
                {
                    nomCegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                }
                //Préparation des données pour la vue...
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
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


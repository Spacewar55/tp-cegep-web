using GestionCegepWeb.Logics.Controleurs;
using Microsoft.AspNetCore.Mvc;
using System;

/// <summary>
/// Namespace pour les controleurs de vue.
/// </summary>
namespace tp_cegep_web.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de vue des Cours.
    /// </summary>
    public class CoursController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Cours.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("Cours")]
        [Route("Cours/Index")]
        [HttpGet]
        public IActionResult Index([FromQuery] string nomCegep, [FromQuery] string nomDepartement)
        {
            try
            {
                if (nomCegep == null || nomDepartement == null)
                {
                    nomCegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                    nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;
                }
                ViewBag.nomCegep = nomCegep;
                ViewBag.nomDepartement = nomDepartement;
                //Préparation des données pour la vue...
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(nomCegep).ToArray();
                ViewBag.ListeCours = CegepControleur.Instance.ObtenirListeCours(nomCegep, nomDepartement).ToArray();
            }
            catch (Exception e)
            {
                if (e.Message == "Erreur lors de l'obtention d'un département par son nom et son cégep...")
                {
                    nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;

                    ViewBag.nomCegep = nomCegep;
                    ViewBag.nomDepartement = nomDepartement;

                    ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                    ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(nomCegep).ToArray();
                    ViewBag.ListeCours = CegepControleur.Instance.ObtenirListeCours(nomCegep, nomDepartement).ToArray();
                }
                else
                {
                    ViewBag.MessageErreur = e.Message;
                }
            }
            //Retour de la vue...
            return View();
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;

/// <summary>
/// Namespace pour les controleurs de vue.
/// </summary>
namespace tp_cegep_web.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de vue des Cégeps.
    /// </summary>
    public class CegepController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Cégeps.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("")]
        [Route("Cegep")]
        [Route("Cegep/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                //Préparation des données pour la vue...
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }

            //Retour de la vue...
            return View();
        }

        [Route("/Cegep/AjouterCegep")]
        [HttpPost]
        public IActionResult AjouterCegep([FromForm] CegepDTO cegepDTO)
        {
            try
            {
                CegepControleur.Instance.AjouterCegep(cegepDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Cegep", cegepDTO);
        }

        /// <summary>
        /// Action FormulaireModifierCegep.
        /// Permet d'afficher le formulaire pour la modification d'un Cégep.
        /// </summary>
        /// <param name="nomCegep">Nom du Cégep.</param>
        /// <returns>IActionResult</returns>
        [Route("/Cegep/FormulaireModifierCegep")]
        [HttpGet]
        public IActionResult FormulaireModifierCegep([FromQuery] string nomCegep)
        {
            try
            {
                CegepDTO cegep = CegepControleur.Instance.ObtenirCegep(nomCegep);
                return View(cegep);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// Action ModifierCegep.
        /// Permet de modifier un Cégep.
        /// </summary>
        /// <param name="cegepDTO">Le Cégep a modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Cegep/ModifierCegep")]
        [HttpPost]
        public IActionResult ModifierCegep([FromForm] CegepDTO cegepDTO)
        {
            try
            {
                CegepControleur.Instance.ModifierCegep(cegepDTO);
            }
            catch (Exception e)
            {
                return RedirectToAction("FormulaireModifierCegep", "Cegep", new { nomCegep = cegepDTO.Nom });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }
    }
}

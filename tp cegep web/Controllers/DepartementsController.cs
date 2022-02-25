using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;

/// <summary>
/// Namespace pour les controleurs de vue.
/// </summary>
namespace tp_cegep_web.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de vue des Départements.
    /// </summary>
    public class DepartementController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Départements.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
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
                ViewBag.nomCegep = nomCegep;
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

        [Route("/Departement/AjouterDepartement")]
        [HttpPost]
        public IActionResult AjouterDepartement([FromForm] string nomCegep ,[FromForm] DepartementDTO departementDTO)
        {
            try
            {
                CegepControleur.Instance.AjouterDepartement(nomCegep, departementDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Departement", new { nomCegep = nomCegep });
        }

        /// <summary>
        /// Action FormulaireModifierDepartement.
        /// Permet d'afficher le formulaire pour la modification d'un Département.
        /// </summary>
        /// <param name="nomCegep">Nom du Cégep.</param>
        /// <param name="nomDepartement">Nom du Département.</param>
        /// <returns>IActionResult</returns>
        [Route("/Departement/FormulaireModifierDepartement")]
        [HttpGet]
        public IActionResult FormulaireModifierDepartement([FromQuery] string nomCegep, [FromQuery] string nomDepartement)
        {
            try
            {
                DepartementDTO departement = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
                ViewBag.nomCegep = nomCegep;
                return View(departement);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// Action ModifierDepartement.
        /// Permet de modifier un Département.
        /// </summary>
        /// <param name="departementDTO">Le Département a modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Departement/ModifierDepartement")]
        [HttpPost]
        public IActionResult ModifierCegep([FromForm] string nomCegep, [FromForm] DepartementDTO departementDTO)
        {
            try
            {
                CegepControleur.Instance.ModifierDepartement(nomCegep, departementDTO);
            }
            catch (Exception e)
            {
                return RedirectToAction("FormulaireModifierDepartement", "Departement", new { nomCegep = nomCegep, nomDepartement = departementDTO.Nom });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index", new { nomCegep = nomCegep });
        }
    }
}


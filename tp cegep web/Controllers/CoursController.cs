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

        [Route("/Cours/AjouterCours")]
        [HttpPost]
        public IActionResult AjouterCours([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] CoursDTO coursDTO)
        {
            try
            {
                CegepControleur.Instance.AjouterCours(nomCegep, nomDepartement, coursDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Cours", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }

        /// <summary>
        /// Action FormulaireModifierDepartement.
        /// Permet d'afficher le formulaire pour la modification d'un Département.
        /// </summary>
        /// <param name="nomCegep">Nom du Cégep.</param>
        /// <param name="nomDepartement">Nom du Département.</param>
        /// <param name="noEnseignant">No de l'Enseignant.</param>
        /// <returns>IActionResult</returns>
        [Route("/Cours/FormulaireModifierCours")]
        [HttpGet]
        public IActionResult FormulaireModifierCours([FromQuery] string nomCegep, [FromQuery] string nomDepartement, [FromQuery] string nomCours)
        {
            try
            {
                CoursDTO cours = CegepControleur.Instance.ObtenirCours(nomCegep, nomDepartement, nomCours);
                ViewBag.nomCegep = nomCegep;
                ViewBag.nomDepartement = nomDepartement;
                return View(cours);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// Action ModifierEnseignant.
        /// Permet de modifier un Enseignant.
        /// </summary>
        /// <param name="coursDTO">L'Enseignant a modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Cours/ModifierCours")]
        [HttpPost]
        public IActionResult ModifierCours([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] CoursDTO coursDTO)
        {
            try
            {
                CegepControleur.Instance.ModifierCours(nomCegep, nomDepartement, coursDTO);
            }
            catch (Exception e)
            {
                return RedirectToAction("FormulaireModifierCours", "Cours", new { nomCegep = nomCegep, nomDepartement = nomDepartement, nomCours = coursDTO.Nom });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }
    }
}

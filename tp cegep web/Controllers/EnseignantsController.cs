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
    /// Classe représentant le controleur de vue des Enseignants.
    /// </summary>
    public class EnseignantController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Enseignants.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("Enseignant")]
        [Route("Enseignant/Index")]
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
                ViewBag.ListeEnseignants = CegepControleur.Instance.ObtenirListeEnseignant(nomCegep, nomDepartement).ToArray();
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
                    ViewBag.ListeEnseignants = CegepControleur.Instance.ObtenirListeEnseignant(nomCegep, nomDepartement).ToArray();
                }
                else
                {
                    ViewBag.MessageErreur = e.Message;
                }
            }
            //Retour de la vue...
            return View();
        }

        [Route("/Enseignant/AjouterEnseignant")]
        [HttpPost]
        public IActionResult AjouterEnseignant([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] EnseignantDTO enseignantDTO)
        {
            try
            {
                CegepControleur.Instance.AjouterEnseignant(nomCegep, nomDepartement, enseignantDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Enseignant", new {nomCegep = nomCegep, nomDepartement = nomDepartement});
        }

        /// <summary>
        /// Action FormulaireModifierDepartement.
        /// Permet d'afficher le formulaire pour la modification d'un Département.
        /// </summary>
        /// <param name="nomCegep">Nom du Cégep.</param>
        /// <param name="nomDepartement">Nom du Département.</param>
        /// <param name="noEnseignant">No de l'Enseignant.</param>
        /// <returns>IActionResult</returns>
        [Route("/Enseignant/FormulaireModifierEnseignant")]
        [HttpGet]
        public IActionResult FormulaireModifierEnseignant([FromQuery] string nomCegep, [FromQuery] string nomDepartement, [FromQuery] int noEnseignant)
        {
            try
            {
                EnseignantDTO enseignant = CegepControleur.Instance.ObtenirEnseignant(nomCegep, nomDepartement, noEnseignant);
                ViewBag.nomCegep = nomCegep;
                ViewBag.nomDepartement = nomDepartement;
                return View(enseignant);
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
        /// <param name="enseignantDTO">L'Enseignant a modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Enseignant/ModifierEnseignant")]
        [HttpPost]
        public IActionResult ModifierEnseignant([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] EnseignantDTO enseignantDTO)
        {
            try
            {
                CegepControleur.Instance.ModifierEnseignant(nomCegep, nomDepartement, enseignantDTO);
            }
            catch (Exception e)
            {
                return RedirectToAction("FormulaireModifierEnseignant", "Enseignant", new { nomCegep = nomCegep, nomDepartement = nomDepartement, noEnseignant = enseignantDTO.NoEmploye });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Algorithmes.Realisations;
using TeamsMaker_METIER.Algorithmes.Realisations.Niv2;
using TeamsMaker_METIER.Algorithmes.Realisations.Niv1_Base;
using TeamsMaker_METIER.Problemes;

namespace TeamsMaker_METIER.Algorithmes
{
    /// <summary>
    /// Fabrique des algorithmes
    /// </summary>
    public class FabriqueAlgorithme
    {
        #region --- Propriétés ---
        /// <summary>
        /// Liste des noms des algorithmes
        /// </summary>
        public string[] ListeAlgorithmes => Enum.GetValues(typeof(NomAlgorithme)).Cast<NomAlgorithme>().ToList().Select(nom => nom.Affichage()).ToArray();
        #endregion

        #region --- Méthodes ---
        /// <summary>
        /// Fabrique d'algorithme en fonction du nom de l'algorithme
        /// </summary>
        /// <param name="nomAlgorithme">Nom de l'algorithme</param>
        /// <returns></returns>
        public Algorithme? Creer(NomAlgorithme nomAlgorithme)
        {
            Algorithme res = null;
            switch (nomAlgorithme)
            {
                case NomAlgorithme.ALGOTEST: res = new AlgorithmeTest(); break;
                // question 17 
                case NomAlgorithme.GLOUTONCROISSANT: res = new AlgorithmeGloutonCroissant(); break;
                // Ajout l'aglorithme glouton décroissant
                case NomAlgorithme.GLOUTONDECROISSANT: res = new AlgorithmeGloutonDécroissant(); break;

                // Ajout l'aglorithme glouton équilibre progressif
                case NomAlgorithme.EQUILIBREPROGRESSIF: res = new EquilibreProgressif(); break;
                // Ajout l'aglorithme glouton Extremes En Premier
                case NomAlgorithme.EXTREMESENPREMIER: res = new ExtremesEnPremier(); break;

                // Ajout l'aglorithme local NSwap
                case NomAlgorithme.NSWAP: res = new AlgorithmeNSwap(); break;
                //Ajout l'aglorithme local NOpt
                case NomAlgorithme.NOPT: res = new AlgorithmeNOpt(); break;

                //Ajout l'algorithme génétique simple
                case NomAlgorithme.GENETIQUE: res = new AlgorithmeGenetique(); break;


                //niv 2
                case NomAlgorithme.Niv2ENPRE: res = new Niv2EnPre(); break; 
                case NomAlgorithme.Niv2OPT: res = new Niv2Opt(); break;
            }
            return res;
        }
        #endregion
    }
}

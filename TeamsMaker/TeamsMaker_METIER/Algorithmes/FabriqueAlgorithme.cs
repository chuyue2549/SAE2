using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Algorithmes.Realisations;
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
            switch(nomAlgorithme)
            {
                case NomAlgorithme.ALGOTEST: res = new AlgorithmeTest(); break;
                // question 17 
                case NomAlgorithme.GLOUTONCROISSANT: res = new AlgorithmeGloutonCroissant(); break;
                // Ajout l'aglorithme glouton décroissant
                case NomAlgorithme.GLOUTONDECROISSANT: res = new GloutonDécroissant(); break;
                // Ajout l'aglorithme glouton équilibre progressif
                case NomAlgorithme.EQUILIBREPROGRESSIF: res = new EquilibreProgressif(); break;
                // Ajout l'aglorithme local NSwap
                case NomAlgorithme.NSWAP: res = new AlgorithmeNSwap(); break;
                //Ajout l'aglorithme local NOpt
                case NomAlgorithme.NOPT: res = new AlgorithmeNOpt(); break;
            }
            return res;
        }
        #endregion
    }
}

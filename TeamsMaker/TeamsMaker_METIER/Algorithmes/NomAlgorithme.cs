using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsMaker_METIER.Algorithmes
{
    /// <summary>
    /// Liste des noms d'algorithmes
    /// </summary>
    public enum NomAlgorithme
    {
        ALGOTEST,
        GLOUTONCROISSANT,
        GLOUTONDECROISSANT,
        EQUILIBREPROGRESSIF,
        NSWAP,
        NOPT

    }

    //Question 16
    public static class NomAlgorithmeExt
    {
        /// <summary>
        /// Affichage du nom de l'algorithme
        /// </summary>
        /// <param name="algo">NomAlgorithme</param>
        /// <returns>La chaine de caractères à afficher</returns>
        public static string Affichage(this NomAlgorithme algo)
        {
            string res = "Algorithme non nommé :(";
            switch(algo)
            {
                case NomAlgorithme.ALGOTEST: res = "Algorithme de test (à supprimer)"; break;
                case NomAlgorithme.GLOUTONCROISSANT: res = "Algorithme Glouton Croissant"; break;
                case NomAlgorithme.GLOUTONDECROISSANT: res = "Algorithme Glouton Décroissant"; break;
                case NomAlgorithme.EQUILIBREPROGRESSIF: res = "Algorithme Equilibre progressif"; break;
                case NomAlgorithme.NSWAP: res = "Algorithme Locale NSWAP"; break;
                case NomAlgorithme.NOPT: res = "Algorithme Locale NOPT"; break;
            }
        
            return res;
        }
    }
}

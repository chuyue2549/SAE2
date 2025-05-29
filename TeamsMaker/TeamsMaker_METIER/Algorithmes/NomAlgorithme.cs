using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Algorithmes.Realisations;

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
        EXTREMESENPREMIER,
        EQUILIBREPROGRESSIF,
        NSWAP,
        NOPT,
        GENETIQUE,


        Niv2ENPRE,
        Niv2OPT


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
            switch (algo)
            {
                case NomAlgorithme.ALGOTEST: res = "Algorithme de test (à supprimer)"; break;
                case NomAlgorithme.GLOUTONCROISSANT: res = "Algorithme Glouton Croissant"; break;
                case NomAlgorithme.GLOUTONDECROISSANT: res = "Algorithme Glouton Décroissant"; break;
                case NomAlgorithme.EQUILIBREPROGRESSIF: res = "Algorithme Equilibre progressif"; break;
                case NomAlgorithme.EXTREMESENPREMIER: res = "Algorithme En Premier"; break;
                case NomAlgorithme.NSWAP: res = "Algorithme Locale NSWAP"; break;
                case NomAlgorithme.NOPT: res = "Algorithme Locale NOPT"; break;
                case NomAlgorithme.GENETIQUE: res = "Algorithme génétique"; break;
                case NomAlgorithme.Niv2ENPRE: res = "Algorithme En Premier pour niv 2"; break;
                case NomAlgorithme.Niv2OPT: res = "Algorithme NOpt pour niv 2"; break;

            }

            return res; 
        }
    }
}

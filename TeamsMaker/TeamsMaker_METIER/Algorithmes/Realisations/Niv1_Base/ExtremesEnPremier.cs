using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Algorithmes.Outils;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Realisations.Niv1_Base
{
    /// <summary>
    /// Heuristique extrêmes en premier : on trie les personnages par ordre croissant de niveau 
    ///et puis on choisit deux premiers meilleurs et deux moins bon restant
    /// </summary>
    internal class ExtremesEnPremier : Algorithme
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            //démarrer stopwatch pour mesurer le temps d'exécution
            Stopwatch stw = new Stopwatch();
            stw.Start();
            // créer la répartition
            Repartition repartition = new Repartition(jeuTest);
            // TRIER la liste de personnages par niveau principal (ordre croissant)
            List<Personnage> personnagesDispo = new List<Personnage>(jeuTest.Personnages);
            personnagesDispo.Sort((a, b) => a.LvlPrincipal.CompareTo(b.LvlPrincipal));

            while (personnagesDispo.Count >= 4)
            {
                //creation une equipe vide
                Equipe equipe = new Equipe();

                //ajouter deux premier faibles 
                equipe.AjouterMembre(personnagesDispo[0]);
                equipe.AjouterMembre(personnagesDispo[1]);
                // retirer les deux 
                personnagesDispo.RemoveAt(0);
                personnagesDispo.RemoveAt(0);

                // Prendre les 2 plus forts (à la fin)
                int n = personnagesDispo.Count;
                equipe.AjouterMembre(personnagesDispo[n - 1]);
                equipe.AjouterMembre(personnagesDispo[n - 2]);

                personnagesDispo.RemoveAt(n - 1); // dernier
                personnagesDispo.RemoveAt(n - 2); // avant-dernier (MAIS maintenant n-2 est bien le bon car n-1 a été supprimé juste avant)

                // Ajouter à la répartition
                repartition.AjouterEquipe(equipe);

            }
            stw.Stop();
            TempsExecution = stw.ElapsedMilliseconds;
            //Retourner la répartition
            return repartition;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Realisations.Niv1_Base
{
    /// <summary>
    /// Former des équipes de 4 joueurs, en ajoutant un joueur à la fois,
    /// de façon à ce que la moyenne de l’équipe se rapproche le plus possible de 50 à chaque ajout.
    /// </summary>
    public class EquilibreProgressif : Algorithme
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            //Démarrer le stopwatch
            Stopwatch stw = new Stopwatch();
            stw.Start();
            //créer une répartition vide
            Repartition repartition = new Repartition(jeuTest);

            //Mettre tous les personnages dans une List<Personnage> mutable
            List<Personnage?> personnagesDispo = new List<Personnage?>(jeuTest.Personnages);
            //Tant qu’il reste au moins 4 personnages:
            while (personnagesDispo.Count >= 4)
            {
                //Créer une équipe vide
                Equipe equipe = new Equipe();
                //Choisir un joueur de départ(par exemple le premier)
                Personnage permierPersonnage = personnagesDispo[0];
                equipe.AjouterMembre(permierPersonnage);
                personnagesDispo.Remove(permierPersonnage);

                //Pour chaque joueur restant, ajouter celui qui rapproche le plus la moyenne de 50
                for (int i = 0; i < 3; i++)
                {
                    Personnage bestPerso = null;
                    double bestEcart = double.MaxValue;

                    foreach (Personnage p in personnagesDispo)
                    {
                        double moyenneTemp = (equipe.Membres.Sum(m => m.LvlPrincipal) + p.LvlPrincipal )/ (equipe.Membres.Length + 1);
                        double ecart = Math.Abs(50 - moyenneTemp);

                        if (ecart < bestEcart)
                        {
                            bestEcart = ecart;
                            bestPerso = p;
                        }
                    }
                    equipe.AjouterMembre(bestPerso);
                    personnagesDispo.Remove(bestPerso);
                }
                repartition.AjouterEquipe(equipe);
            }
                //Arrêter le Stopwatch, enregistrer le temps
                stw.Stop();
                TempsExecution = stw.ElapsedMilliseconds;
                //Retourner la répartition
                return repartition;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Algorithmes.Outils;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Realisations
{
    /// <summary>
    /// Créer l'équipe par niveau principale de personnage trié décroissant
    /// </summary>
    internal class GloutonDécroissant : Algorithme
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            // Créez un objet Stopwatch et démarrez-le au début de la méthode Repartir.
            Stopwatch stw = new Stopwatch();
            stw.Start();

            Repartition repartition = new Repartition(jeuTest);
            //Récupérer le tableau des personnages de "jeuTest".

            Personnage[] tab_perso = jeuTest.Personnages;
            // Trier le tableau par le niveau decroissant
            Array.Sort(tab_perso, new ComparePersonnageParNiveauPrincipalDecroissant());

            //    Lire le tableau 4 personnages par 4 personnages en faisant à chaque fois :
            for (int i = 0; i < tab_perso.Length / 4; i++)
            {
                //   Créer une équipe vide
                Equipe equipe = new Equipe();

                //    Ajouter les 4 personnages 1 par 1
                for (int j = i * 4; j < (i + 1) * 4; j++)
                {
                    equipe.AjouterMembre(tab_perso[j]);
                }

                //Ajouter l'équipe à la répartition.
                repartition.AjouterEquipe(equipe);

            }

            // Arrêtez-le à la fin de la méthode.
            stw.Stop();
            // Sauvegardez le temps d’exécution
            this.TempsExecution = stw.ElapsedMilliseconds;

            //Renvoyez la répartition
            return repartition;
        }

    }
    
}

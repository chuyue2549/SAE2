using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Realisations.Niv1_Base
{
    internal class AlgorithmeGenetique : Algorithme
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            //Démarrer le stopwatch
            Stopwatch stw = new Stopwatch();
            stw.Start();
            //créer une répartition vide
            Repartition repartition = new Repartition(jeuTest);
            List<Personnage> disponibles = new List<Personnage>(jeuTest.Personnages);
            Random rnd = new Random();

            disponibles = disponibles.OrderBy(x => rnd.Next()).ToList();

            // Créer les équipes de 4
            while (disponibles.Count >= 4)
            {
                Equipe equipe = new Equipe();
                for (int i = 0; i < 4; i++)
                {
                    equipe.AjouterMembre(disponibles[0]);
                    disponibles.RemoveAt(0);
                }
                repartition.AjouterEquipe(equipe);
            }

            //Arrêter le Stopwatch, enregistrer le temps
            stw.Stop();
            TempsExecution = stw.ElapsedMilliseconds;
            //retourner la répatition 
            return repartition;
        }
    }
}

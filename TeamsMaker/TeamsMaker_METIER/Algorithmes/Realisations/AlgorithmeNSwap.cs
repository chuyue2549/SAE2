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
    public class AlgorithmeNSwap : Algorithme //2-Swap
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stw = new Stopwatch();
            stw.Start();

            // initialize ; 4 members in a equipe
            Repartition repartition = new Repartition(jeuTest);
            Personnage[] tab_perso = jeuTest.Personnages;

            int fullTeamCount = tab_perso.Length / 4;
            for (int i = 0; i < fullTeamCount; i++)
            {
                Equipe equipe = new Equipe();
                for (int j = i * 4; j < (i + 1) * 4; j++)
                    equipe.AjouterMembre(tab_perso[j]);
                repartition.AjouterEquipe(equipe);
            }

            // the member don't have equipe => PersonnagesSansEquipe

            repartition.LancerEvaluation(Problemes.Probleme.SIMPLE);

            int noImprovementCount = 0;
            const int maxNoImprovement = 10;

            while (noImprovementCount < maxNoImprovement)
            {
                bool improved = false;
                var equipes = repartition.Equipes;
                int equipeCount = equipes.Length;

                for (int i = 0; i < equipeCount; i++)
                {
                    for (int j = i + 1; j < equipeCount; j++)
                    {
                        Equipe eq1 = equipes[i];
                        Equipe eq2 = equipes[j];

                        foreach (var perso1 in eq1.Membres)
                        {
                            foreach (var perso2 in eq2.Membres)
                            {
                                // clone
                                Repartition tentative = repartition.Cloner();
                                Equipe t_eq1 = tentative.Equipes[i];
                                Equipe t_eq2 = tentative.Equipes[j];

                                // change member in equip
                                t_eq1.RemoveMembre(perso1);
                                t_eq2.RemoveMembre(perso2);
                                t_eq1.AjouterMembre(perso2);
                                t_eq2.AjouterMembre(perso1);

                                // test：after changages the equipe still has 4 membres
                                if (t_eq1.Membres.Length != 4 || t_eq2.Membres.Length != 4)
                                    continue;

                                tentative.LancerEvaluation(Problemes.Probleme.SIMPLE);

                                if (tentative.Score < repartition.Score)
                                {
                                    repartition = tentative;
                                    improved = true;
                                }
                            }
                        }
                    }
                }

                if (improved)
                    noImprovementCount = 0;
                else
                    noImprovementCount++;

                //Debug.WriteLine($"score : {repartition.Score}, times score no raise : {noImprovementCount}");
            }

            stw.Stop();
            this.TempsExecution = stw.ElapsedMilliseconds;
            return repartition;
        }
    }
}
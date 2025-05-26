using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Realisations
{
    public class AlgorithmeNOpt : Algorithme
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stw = new Stopwatch();
            stw.Start();

            //initialize ; 4 members in a equipe
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
                        // tous le memebres dans les deux equipe ajoute tous membres dans uniom
                        List<Personnage> union = new List<Personnage>();
                        union.AddRange(equipes[i].Membres);
                        union.AddRange(equipes[j].Membres);


                        foreach (var group1 in GetPersonnageCombinations(union, 4))
                        {
                            List<Personnage> group2 = union.Except(group1).ToList();

                            if (group1.Count != 4 || group2.Count != 4)
                                continue;

                            // coloner et changer
                            Repartition tentative = repartition.Cloner();
                            Equipe t_eq1 = tentative.Equipes[i];
                            Equipe t_eq2 = tentative.Equipes[j];

                            t_eq1.Vider();
                            t_eq2.Vider();
                            foreach (var p in group1) t_eq1.AjouterMembre(p);
                            foreach (var p in group2) t_eq2.AjouterMembre(p);

                            tentative.LancerEvaluation(Problemes.Probleme.SIMPLE);

                            if (tentative.Score < repartition.Score)
                            {
                                repartition = tentative;
                                improved = true;
                            }
                        }
                    }
                }

                List<Personnage> restants = repartition.PersonnagesSansEquipe.ToList();

                while (restants.Count >= 4)
                {
                    Equipe equipe = new Equipe();
                    for (int k = 0; k < 4; k++)
                    {
                        equipe.AjouterMembre(restants[0]);
                        restants.RemoveAt(0);
                    }
                    repartition.AjouterEquipe(equipe);
                }

                if (improved)
                    noImprovementCount = 0;
                else
                    noImprovementCount++;

                Debug.WriteLine($"[n-opt] score : {repartition.Score}, no raise count : {noImprovementCount}");
            }

            stw.Stop();
            this.TempsExecution = stw.ElapsedMilliseconds;
            return repartition;
        }

        private List<List<Personnage>> GetPersonnageCombinations(List<Personnage> list, int n)
        {
            List<List<Personnage>> result = new List<List<Personnage>>();
            Generate(new List<Personnage>(), 0);
            return result;

            void Generate(List<Personnage> current, int index)
            {
                if (current.Count == n)
                {
                    result.Add(new List<Personnage>(current));
                    return;
                }

                if (index >= list.Count)
                    return;

                current.Add(list[index]);
                Generate(current, index + 1);

                current.RemoveAt(current.Count - 1);
                Generate(current, index + 1);
            }
        }
    }
}
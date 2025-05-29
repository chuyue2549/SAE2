using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;
using TeamsMaker_METIER.Problemes;

namespace TeamsMaker_METIER.Algorithmes.Realisations.Niv1_Base
{
    public class AlgorithmeNOpt : Algorithme
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stw = new Stopwatch();
            stw.Start();

            // initialize ; 4 members in a equipe
            Repartition repartition = new Repartition(jeuTest);

            AlgorithmeTest algorithmeTest = new AlgorithmeTest();
            repartition = algorithmeTest.Repartir(jeuTest);

            repartition.LancerEvaluation(Probleme.SIMPLE);

            int noImprovementCount = 0;
            const int maxNoImprovement = 10;
            Random rand = new Random();

            while (noImprovementCount < maxNoImprovement)
            {
                bool improved = false;
                var equipes = repartition.Equipes;
                int equipeCount = equipes.Length;

                // Find possible i, j pairs from equipe list
                int iIdx = rand.Next(equipeCount);
                int jIdx;
                do
                {
                    jIdx = rand.Next(equipeCount);
                } while (jIdx == iIdx);


                List<Personnage> union = new List<Personnage>();
                union.AddRange(equipes[iIdx].Membres);
                union.AddRange(equipes[jIdx].Membres);

                double bestScore = double.MaxValue;
                List<Personnage> bestGroup1 = null;
                List<Personnage> bestGroup2 = null;

                // Try all combinations from the union of equipe i and j
                foreach (var group1 in GetPersonnageCombinations(union))
                {
                    List<Personnage> group2 = new List<Personnage>();
                    foreach (var p in union)
                    {
                        if (!group1.Contains(p))
                            group2.Add(p);
                    }
                    if (group2.Count != 4) continue;

                    // create two temporary equipe for finding the best combinations
                    Equipe temp1 = new Equipe();
                    Equipe temp2 = new Equipe();
                    foreach (var p in group1) temp1.AjouterMembre(p);
                    foreach (var p in group2) temp2.AjouterMembre(p);

                    // Evaluate the score of temporary equipe
                    double score = temp1.Score(Probleme.SIMPLE) + temp2.Score(Probleme.SIMPLE);
                    if (score < bestScore)
                    {
                        bestScore = score;
                        bestGroup1 = group1;
                        bestGroup2 = group2;
                    }
                }

                if (bestGroup1 != null && bestGroup2 != null)
                {
                    // Clone and remove
                    Repartition tentative = repartition.Cloner();
                    var eqs = tentative.Equipes.ToList();
                    int maxIdx = Math.Max(iIdx, jIdx);
                    int minIdx = Math.Min(iIdx, jIdx);
                    eqs.RemoveAt(maxIdx);
                    eqs.RemoveAt(minIdx);
                    tentative = new Repartition(jeuTest);
                    foreach (var eq in eqs)
                        tentative.AjouterEquipe(eq);

                    // Create new equipe with best combinations
                    Equipe t_eq1 = new Equipe();
                    Equipe t_eq2 = new Equipe();
                    foreach (var p in bestGroup1) t_eq1.AjouterMembre(p);
                    foreach (var p in bestGroup2) t_eq2.AjouterMembre(p);
                    tentative.AjouterEquipe(t_eq1);
                    tentative.AjouterEquipe(t_eq2);

                    tentative.LancerEvaluation(Probleme.SIMPLE);
                    if (tentative.Score < repartition.Score)
                    {
                        repartition = tentative;
                        improved = true;
                    }
                }


                if (improved)
                    noImprovementCount = 0;
                else
                    noImprovementCount++;

                //Debug.WriteLine($"[n-opt] score : {repartition.Score}, no raise count : {noImprovementCount}");
            }

            stw.Stop();
            TempsExecution = stw.ElapsedMilliseconds;
            return repartition;
        }

        // find all combinations of i and j equipe
        public static List<List<Personnage>> GetPersonnageCombinations(List<Personnage> list)
        {
            var result = new List<List<Personnage>>();
            GenerateCombinations(result, list, new List<Personnage>(), 0, 4);
            return result;
        }

        private static void GenerateCombinations(
        List<List<Personnage>> result,
        List<Personnage> list,
        List<Personnage> current,
        int index,
        int targetCount)
        {
            if (current.Count == targetCount)
            {
                result.Add(new List<Personnage>(current));
                return;
            }

            if (index >= list.Count) return;

            current.Add(list[index]);
            GenerateCombinations(result, list, current, index + 1, targetCount);

            current.RemoveAt(current.Count - 1);
            GenerateCombinations(result, list, current, index + 1, targetCount);
        }
    }
}
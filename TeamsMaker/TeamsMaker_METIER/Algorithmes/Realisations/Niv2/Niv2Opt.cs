using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;
using TeamsMaker_METIER.Problemes;

namespace TeamsMaker_METIER.Algorithmes.Realisations.Niv2
{
    class Niv2Opt : Algorithme
    {

        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stw = new Stopwatch();
            stw.Start();

            // Initialize: 4 members per equipe
            Repartition repartition = new Repartition(jeuTest);

            GrouperParRolePrincipal(jeuTest.Personnages.ToList(), out var personnagesTank, out var personnagesSupport, out var personnagesDPS);

           
            Random rand = new Random();

            while (personnagesDPS.Count >= 2 && personnagesTank.Count >= 1 && personnagesSupport.Count >= 1)
            {
                Equipe equipe = new Equipe();

                // choose 2 membre random from DPS
                int indexDPS1 = rand.Next(personnagesDPS.Count);
                Personnage dps1 = personnagesDPS[indexDPS1];
                personnagesDPS.RemoveAt(indexDPS1);

                int indexDPS2 = rand.Next(personnagesDPS.Count);
                Personnage dps2 = personnagesDPS[indexDPS2];
                personnagesDPS.RemoveAt(indexDPS2);

                // choose 2 membre random from tank
                int indexTank = rand.Next(personnagesTank.Count);
                Personnage tank = personnagesTank[indexTank];
                personnagesTank.RemoveAt(indexTank);

                // choose 2 membre random from support
                int indexSupport = rand.Next(personnagesSupport.Count);
                Personnage support = personnagesSupport[indexSupport];
                personnagesSupport.RemoveAt(indexSupport);

                // ajouter
                equipe.AjouterMembre(dps1);
                equipe.AjouterMembre(dps2);
                equipe.AjouterMembre(tank);
                equipe.AjouterMembre(support);


                repartition.AjouterEquipe(equipe);
            }

            //process

            int noImprovementCount = 0;
            const int maxNoImprovement = 50;
            int equipeCount = repartition.Equipes.Length;
            Probleme pro = Problemes.Probleme.ROLEPRINCIPAL;

            while (noImprovementCount < maxNoImprovement)
            {
                bool improved = false;
                var equipes = repartition.Equipes;

                // Find possible i, j pairs from equipe list
                int iIdx = rand.Next(equipeCount);
                int jIdx;
                do
                {
                    jIdx = rand.Next(equipeCount);
                }while (jIdx == iIdx);

                List<Personnage> union = new List<Personnage>();
                union.AddRange(equipes[iIdx].Membres);
                union.AddRange(equipes[jIdx].Membres);

                double bestScore = double.MaxValue;
                List<Personnage> bestGroup1 = null;
                List<Personnage> bestGroup2 = null;

                // Try all combinations from the union of equipe i and j
                List<List<Personnage>> AllC = GetValidPersonnageCombinations(union);
                foreach (var group1 in AllC)
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
                    double score = temp1.Score(pro) + temp2.Score(pro);
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

                    tentative.LancerEvaluation(pro);
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
            this.TempsExecution = stw.ElapsedMilliseconds;
            return repartition;
        }

        public static List<List<Personnage>> GetValidPersonnageCombinations(List<Personnage> list)
        {
            List<List<Personnage>> result = new List<List<Personnage>>();
            Generate(new List<Personnage>(), 0);
            return result;

            void Generate(List<Personnage> current, int index)
            {
                if (current.Count == 4)
                {
                    if (EstCompositionValide(current)) //1d, 1s, 2d
                        result.Add(new List<Personnage>(current));
                    return;
                }

                if (index >= list.Count) return;

                // Include current personnage
                current.Add(list[index]);
                Generate(current, index + 1);

                // Exclude current personnage
                current.RemoveAt(current.Count - 1);
                Generate(current, index + 1);
            }
        }
    }
}

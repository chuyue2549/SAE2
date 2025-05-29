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
    public class AlgorithmeNSwap : Algorithme // 2- Swap, n = 2
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stw = new Stopwatch();
            stw.Start();

            // Initialize: 4 members per equipe
            Repartition repartition = new Repartition(jeuTest);
            AlgorithmeTest algorithmeTest = new AlgorithmeTest();
            repartition = algorithmeTest.Repartir(jeuTest);

            repartition.LancerEvaluation(Problemes.Probleme.SIMPLE);


            int noImprovementCount = 0;
            const int maxNoImprovement = 50;
            Random rand = new Random();


            //choose n equipe
            int n = 2;
            while (noImprovementCount < maxNoImprovement)
            {
                bool improved = false;
                var equipes = repartition.Equipes;
                int equipeCount = equipes.Length;

                if (equipeCount < n) break; // Not enough equipe to swap

                // Randomly choose n equipe
                HashSet<int> selectedIndexes = new HashSet<int>();//selected n equipe
                while (selectedIndexes.Count < n)
                {
                    selectedIndexes.Add(rand.Next(equipeCount));
                }
                var selected = selectedIndexes.ToList();

                // Pick one random member from each team
                List<Personnage> selectedMembers = new List<Personnage>();
                foreach (int idx in selected)
                {
                    var eq = equipes[idx];
                    var mem = eq.Membres[rand.Next(eq.Membres.Length)];
                    selectedMembers.Add(mem);
                }

                // Try all cyclic swaps of these n members
                for (int shift = 1; shift < n; shift++)
                {
                    Repartition tentative = repartition.Cloner();
                    List<Equipe> clonedEquipes = selected.Select(idx => tentative.Equipes[idx]).ToList();

                    // Remove selected members
                    for (int i = 0; i < n; i++)
                        clonedEquipes[i].RemoveMembre(selectedMembers[i]);

                    // Add to new teams in shifted order
                    for (int i = 0; i < n; i++)
                    {
                        int newIndex = (i + shift) % n;
                        clonedEquipes[newIndex].AjouterMembre(selectedMembers[i]);
                    }

                    // Ensure all teams are still size 4
                    if (clonedEquipes.All(eq => eq.Membres.Length == 4))
                    {
                        tentative.LancerEvaluation(Problemes.Probleme.SIMPLE);
                        if (tentative.Score < repartition.Score)
                        {
                            repartition = tentative;
                            improved = true;
                            break;
                        }
                    }
                }

                if (improved)
                    noImprovementCount = 0;
                else
                    noImprovementCount++;
            }

            stw.Stop();
            TempsExecution = stw.ElapsedMilliseconds;
            return repartition;
        }
    }
}
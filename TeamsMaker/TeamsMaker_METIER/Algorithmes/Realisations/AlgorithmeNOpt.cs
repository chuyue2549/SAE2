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

            // 初始解：按顺序4人组队
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
                        // 提取两个队伍的所有成员
                        List<Personnage> union = new List<Personnage>();
                        union.AddRange(equipes[i].Membres);
                        union.AddRange(equipes[j].Membres); //ajoute tous membres dans uniom

                        // 遍历 C(8,4) 种组合方式（将8个成员分为两个队伍）
                        foreach (var group1 in GetCombinations(union, 4))
                        {
                            List<Personnage> group2 = union.Except(group1).ToList();

                            // 克隆并替换两个队伍
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

        // 返回从 list 中选择 n 个元素的所有组合
        private IEnumerable<List<T>> GetCombinations<T>(List<T> list, int n)
        {
            int count = list.Count;
            if (n == 0 || n > count)
                yield break;

            int[] indices = Enumerable.Range(0, n).ToArray();
            while (true)
            {
                yield return indices.Select(index => list[index]).ToList();

                int i = n - 1;
                while (i >= 0 && indices[i] == count - n + i)
                    i--;

                if (i < 0) yield break;
                indices[i]++;
                for (int j = i + 1; j < n; j++)
                    indices[j] = indices[j - 1] + 1;
            }
        }
    }
}
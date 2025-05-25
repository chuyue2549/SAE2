using System.Diagnostics;
using TeamsMaker_METIER.Algorithmes.Realisations;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.JeuxTest.Parseurs;
using TeamsMaker_METIER.Problemes;

namespace ModeConsoleTP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            JeuTest _jeuTest = new JeuTest();
            Parseur _parseur = new Parseur();
            _jeuTest = _parseur.Parser("Test.jt");
            AlgorithmeGloutonCroissant algo = new AlgorithmeGloutonCroissant();
            Repartition rp = new Repartition(_jeuTest);
            rp = algo.Repartir(_jeuTest);
            //Probleme pb = new Probleme();
            rp.LancerEvaluation(Probleme.SIMPLE);
            
            // Sauvegardez le temps d’exécution
            long temps = algo.TempsExecution;
            double score = rp.Score;
            Console.WriteLine($"le score de equipe est {score} et le temps de l''excecution est {temps}.");




        }
    }
}

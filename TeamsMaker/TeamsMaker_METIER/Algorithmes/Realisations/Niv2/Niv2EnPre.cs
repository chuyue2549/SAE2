using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Realisations.Niv2
{
    public class Niv2EnPre : Algorithme
    {
        
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stw = new Stopwatch();
            stw.Start();

            // Initialize: 4 members per equipe
            Repartition repartition = new Repartition(jeuTest);

            GrouperParRolePrincipal(jeuTest.Personnages.ToList(), out var personnagesTank, out var personnagesSupport, out var personnagesDPS);

            personnagesTank.Sort((a, b) => a.LvlPrincipal.CompareTo(b.LvlPrincipal));
            personnagesSupport.Sort((a, b) => a.LvlPrincipal.CompareTo(b.LvlPrincipal));
            personnagesDPS.Sort((a, b) => a.LvlPrincipal.CompareTo(b.LvlPrincipal));

            while (personnagesTank.Count > 0 && personnagesSupport.Count > 0 && personnagesDPS.Count > 2 ) 
            {
                //creation une equipe vide
                Equipe equipe = new Equipe();

                //ajouter deux premier faibles 
                equipe.AjouterMembre(personnagesTank[0]);
                equipe.AjouterMembre(personnagesSupport[0]);
                // retirer les deux 
                personnagesTank.RemoveAt(0);
                personnagesSupport.RemoveAt(0);

                // Prendre les 2 plus forts (à la fin)
                int n = personnagesDPS.Count;
                equipe.AjouterMembre(personnagesDPS[n - 1]);
                equipe.AjouterMembre(personnagesDPS[n - 2]);

                personnagesDPS.RemoveAt(n - 1); // dernier
                personnagesDPS.RemoveAt(n - 2); // avant-dernier (MAIS maintenant n-2 est bien le bon car n-1 a été supprimé juste avant)

                // Ajouter à la répartition
                repartition.AjouterEquipe(equipe);

            }

            stw.Stop();
            this.TempsExecution = stw.ElapsedMilliseconds;
            //Retourner la répartition
            return repartition;
        }
    }
}

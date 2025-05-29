using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages.Classes;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes
{
    /// <summary>
    /// Notion générique d'algorithme
    /// </summary>
    public abstract class Algorithme
    {
        #region --- Attributs ---
        private long tempsExecution = -1;    //Temps d'exécution de l'algorithme
        #endregion

        /// <summary>
        /// Lance la répartition des personnages d'un jeu de test donnée
        /// </summary>
        /// <param name="jeuTest">Jeu de test</param>
        /// <returns>La répartition</returns>
        public abstract Repartition Repartir(JeuTest jeuTest);

        /// <summary>
        /// Temps d'exécution de l'algorithme
        /// </summary>
        public long TempsExecution
        {
            get => this.tempsExecution;
            protected set => this.tempsExecution = value;
        }

        /// <summary>
        /// Sépare les personnages en trois listes selon leur rôle principal : Tank, Support, DPS
        /// </summary>
        /// <param name="personnages">Liste de tous les personnages</param>
        /// <param name="personnagesTank">Sortie : personnages ayant le rôle Tank</param>
        /// <param name="personnagesSupport">Sortie : personnages ayant le rôle Support</param>
        /// <param name="personnagesDPS">Sortie : personnages ayant le rôle DPS</param>
        public static void GrouperParRolePrincipal(
        List<Personnage> personnages,
        out List<Personnage> personnagesTank,
        out List<Personnage> personnagesSupport,
        out List<Personnage> personnagesDPS)
        {
            personnagesTank = new List<Personnage>();
            personnagesSupport = new List<Personnage>();
            personnagesDPS = new List<Personnage>();

            foreach (var p in personnages)
            {
                switch (p.RolePrincipal)
                {
                    case Role.TANK:
                        personnagesTank.Add(p);
                        break;
                    case Role.SUPPORT:
                        personnagesSupport.Add(p);
                        break;
                    case Role.DPS:
                        personnagesDPS.Add(p);
                        break;
                    default:
                        break;
                }
            }
        }

        public static bool EstCompositionValide(List<Personnage> membres)
        {
            int countTank = 0;
            int countSupport = 0;
            int countDPS = 0;

            foreach (var p in membres)
            {
                switch (p.RolePrincipal)
                {
                    case Role.TANK:
                        countTank++;
                        break;
                    case Role.SUPPORT:
                        countSupport++;
                        break;
                    case Role.DPS:
                        countDPS++;
                        break;
                }
            }

            return membres.Count == 4 && countTank == 1 && countSupport == 1 && countDPS == 2;
        }


    }
}

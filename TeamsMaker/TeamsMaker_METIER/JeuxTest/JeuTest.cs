using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Personnages;
using TeamsMaker_METIER.Personnages.Classes;

namespace TeamsMaker_METIER.JeuxTest
{
    public class JeuTest
    {
        #region --- Attributs ---
        private List<Personnage> personnages;   //Les personnages
        #endregion

        #region --- Propriétés ---
        /// <summary>
        /// Les personnages
        /// </summary>
        public Personnage[] Personnages => this.personnages.ToArray();
        #endregion

        #region --- Constructeurs ---
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public JeuTest()
        {
            this.personnages = new List<Personnage>();
            // Question 3 : Supprimer le code qui génère un jeu de test aléatoire 
           /* Random rand = new Random();
            for (int i = 0; i < 105; i++)
            {
                Array values = Enum.GetValues(typeof(Classe));
                Random random = new Random();
                Classe randomBar = (Classe)values.GetValue(random.Next(values.Length));
                this.personnages.Add(new Personnage(randomBar, random.Next(1,100), random.Next(1, 100)));
            }*/
        }
        #endregion

        #region --- Méthodes ---
        /// <summary>
        /// Ajout d'un personnage
        /// </summary>
        /// <param name="personnage">Le personnage à ajouter</param>
        public void AjouterPersonnage(Personnage personnage)
        {
            this.personnages.Add(personnage);
        }
        #endregion
    }
}

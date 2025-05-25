using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Personnages;
using TeamsMaker_METIER.Personnages.Classes;

namespace TeamsMaker_METIER.JeuxTest.Parseurs
{
    public class Parseur
    {
        // question 4 
        private Personnage ParserLigne(string ligne)
        {
            // question 8
            string[] tab_s = ligne.Split(' ');
            Classe classe = (Classe)Enum.Parse(typeof(Classe), tab_s[0]);

            int lvl_p = Int32.Parse(tab_s[1]); // level principale 
            int lvl_s = Int32.Parse(tab_s[2]);  // level secondaire

            Personnage personnage = new Personnage(classe, lvl_p, lvl_s);


            return personnage; 
        }
        public JeuTest Parser(string nomFichier)
        {
            JeuTest jeuTest = new JeuTest();
            string cheminFichier = Path.Combine(Directory.GetCurrentDirectory(),
            "JeuxTest/Fichiers/" + nomFichier);
            using (StreamReader stream = new StreamReader(cheminFichier))
            {
                string ligne;
                while ((ligne = stream.ReadLine()) != null)
                {
                    //question 9
                    //Traiter une ligne
                    Personnage perse = ParserLigne(ligne);
                    jeuTest.AjouterPersonnage(perse);
                }
            }
            return jeuTest;
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MOTS_GLISSES
{
    public class Joueur
    {
        private string nom;
        private static string motTrouves = " ";
        private static int score = 0;

        public Joueur(string nom)
        {
            this.nom = nom;
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }    
        }
        public int Score
        {
            get { return score; }
        }

        public string MotTrouves
        {
            get { return motTrouves;}
        }

        public void Add_Mot(string mot)
        {
            motTrouves += mot + " ";
        }

        public string toString()
        {
            return (this.nom + " a un score de " + score + " et a trouvé les mots suivant : " + motTrouves);
        }

        public void Add_Score(int val)
        {
            score += val;
        }

        public bool Contient(string mot)
        {
            string[] tab_mots = motTrouves.Split(' ');
            for(int i = 0; i <  tab_mots.Length; i++) 
            {
                if (tab_mots[i] == mot)
                    return true;
            }
            return false;
        }
    }
}

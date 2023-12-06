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
        private string motTrouves;
        private int score;

        public Joueur(string nom, string motTrouves, int score)
        {
            this.nom = nom;
            this.motTrouves = motTrouves;
            this.score = score;
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }    
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public string MotTrouves
        {
            get { return motTrouves;}
            set { motTrouves = value;}
        }

        public void Add_Mot(string mot)
        {
            this.motTrouves += mot + " ";
        }

        public string toString()
        {
            return (this.nom + " a un score de " + this.score + " et a trouvé les mots suivant : " + this.motTrouves);
        }

        public void Add_Score(int val)
        {
            this.score += val;
        }

        public bool Contient(string mot)
        {
            string[] tab_mots = this.motTrouves.Split(' ');
            for(int i = 0; i <  tab_mots.Length; i++) 
            {
                if (tab_mots[i] == mot)
                    return true;
            }
            return false;
        }
    }
}

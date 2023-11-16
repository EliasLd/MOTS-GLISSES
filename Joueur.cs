using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTS_GLISSES
{
    public class Joueur
    {
        private string nom;
        private static string[] motTrouves = null;
        private static int score = 0;

        public Joueur(string nom)
        {
            this.nom = nom;
        }

        public string Nom
        {
            get { return nom; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public string[] MotTrouves
        {
            get { return motTrouves;}
        }
    }
}

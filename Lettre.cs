using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTS_GLISSES
{
    public class Lettre
    {
        private char symbole;
        private int nombreApparitions;
        private int poids;
        private int nombreApparitionsActuel;
        private bool found = false;

        public Lettre(char symbole, int nombreApparitions, int poids, int nombreApparitionsActuel)
        {
            this.symbole = symbole;
            this.nombreApparitions = nombreApparitions;
            this.poids = poids;
            this.nombreApparitionsActuel = nombreApparitionsActuel;
        }   

        public char Symbole
        {
            get { return symbole;}
        }

        public int NombreApparitions
        {
            get { return nombreApparitions; }
        }

        public int Poids
        {
            get { return poids; }
        }

        public bool Found
        {
            get { return found; }
            set { found = value; }
        }

        public int NombreApparitionsActuel
        {
            get { return nombreApparitionsActuel; }
            set { nombreApparitionsActuel++;}
        }

        public string toString()
        {
            return ("la lettre " + this.symbole + " apparait au maximum " + this.nombreApparitions + " fois, a un poids de " + this.poids + " et est apparue " + this.nombreApparitionsActuel + " fois");
        }

    }
}

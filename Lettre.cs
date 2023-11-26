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

        public Lettre(char symbole, int nombreApparitions, int poids)
        {
            this.symbole = symbole;
            this.nombreApparitions = nombreApparitions;
            this.poids = poids;
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

        public string toString()
        {
            return ("la lettre " + this.symbole + " apparait au maximum " + this.nombreApparitions + " fois et a un poids de " + this.poids);
        }

    }
}

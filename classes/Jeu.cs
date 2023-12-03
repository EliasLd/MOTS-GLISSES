using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTS_GLISSES.classes
{
    public class Jeu
    {
        private Dictionnaire dico;
        private Plateau plateauCourant;
        private static Joueur[] joueurs = new Joueur[2];   //static car il n'y a que deux joueurs par définition dans la consigne mais on n'aurait pu en mettre plus

        public Jeu(Dictionnaire dico, Plateau plateauCourant)
        {
            this.dico = dico;
            this.plateauCourant = plateauCourant;
        }   

        public Dictionnaire Dico
        {
            get { return dico; }
        }

        public Plateau PlateauCourant
        {
            get { return plateauCourant;}
        }

        public Joueur[] Joueurs
        {
            get { return joueurs; }
        }
    }
}
